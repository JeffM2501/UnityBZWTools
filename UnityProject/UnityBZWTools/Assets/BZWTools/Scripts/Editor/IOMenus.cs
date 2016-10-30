using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System;

using BZFlag.IO;
using BZFlag.IO.Elements.Shapes;

public class IOMenus : MonoBehaviour
{
	static string[] GetBZWFilter()
	{
		return new string[] { "BZFlag World", "bzw", "BZFlag Map", "map", "All Files", "*" };
	}

	[MenuItem ("BZWTools/Open/Open BZW")]
	static void OpenBZW()
	{
		string path = EditorUtility.OpenFilePanelWithFilters("Select BZW file", string.Empty, GetBZWFilter());

		if(path == string.Empty)
			return;

		if (EditorUtility.DisplayDialog("Clear Scene Objects?", "Opening a BZW file will clear any existing scene objects, are you sure you wish to continue?", "Yes", "No, import instead"))
		{
			// flush out any old objects
			foreach(var obj in GameObject.FindObjectsOfType(typeof(GameObject)))
			{
				GameObject mb = obj as GameObject;
				if(mb.GetComponent<Camera>() != null || mb.GetComponent<Light>() != null)
					continue;

				UnityEngine.Object.DestroyImmediate(obj);
			}
		}

		FromBZW.ReadUserBZW(path);
	}

	[MenuItem("BZWTools/Open/Import BZW")]
	static void ImportBZW()
	{
		string path = EditorUtility.OpenFilePanelWithFilters("Select BZW file", string.Empty, GetBZWFilter());

		if (path != string.Empty)
			FromBZW.ReadUserBZW(path);
	}

	[MenuItem("BZWTools/Save/Save BZW")]
	static void SavetBZW()
	{
		Map map = ToBZW.BuildBZW();

		if (map == null)
		{
			EditorUtility.DisplayDialog("No BZW World object", "The current scene does not contain a BZWWorld object, create one using the new map menu", "Ok", string.Empty);
			return;
		}

		string path = EditorUtility.SaveFilePanel("Save BZW file", string.Empty, map.WorldInfo.Name, "bzw");

		if(path != string.Empty)
			ToBZW.SaveBZW(path,map);
	}

	[MenuItem("BZWTools/New Map")]
	static void NewBZW()
	{
		FromBZW.CreateNewBZWRoot(new Map());
	}
}
