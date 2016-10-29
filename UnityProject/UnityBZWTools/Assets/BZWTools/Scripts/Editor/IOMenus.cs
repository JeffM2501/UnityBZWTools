using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System;

using BZFlag.IO;


public class IOMenus : MonoBehaviour
{
	[MenuItem ("BZWTools/Open/Open BZW")]
	static void OpenBZW()
	{
		string path = EditorUtility.OpenFilePanelWithFilters("Select BZW file", string.Empty, new string[] { "BZFlag World", "bzw", "BZFlag Map", "map", "All Files", "*" });

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

		ReadUserBZW(path);
	}

	[MenuItem("BZWTools/Open/Import BZW")]
	static void ImportBZW()
	{
		string path = EditorUtility.OpenFilePanelWithFilters("Select BZW file", string.Empty, new string[] { "BZFlag World", "bzw", "BZFlag Map", "map", "All Files", "*" });

		ReadUserBZW(path);
	}

	static void ReadUserBZW(string path)
	{
		if(path != string.Empty)
		{
			Debug.Log("Path was " + path);

			FileInfo file = new FileInfo(path);
			StreamReader sr = file.OpenText();

			var map = Reader.ReadMap(sr);
			sr.Close();

			Debug.Log("Read map has " + map.Objects.Count.ToString() + " objects");
		}
	}
}
