using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System;

using BZFlag.IO.BZW;
using BZFlag.Map;

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
		WorldMap map = ToBZW.BuildBZW();

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
		FromBZW.CreateNewBZWRoot(new WorldMap());
	}

	[MenuItem("BZWTools/Windows/Show BZWTools")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(BZWToolsWindow));
	}

	[MenuItem("BZWTools/Tools/Strip For Preview")]
	public static void StripForPreview()
	{
		//Show existing window instance. If one doesn't exist, make one.
		GameObject root = BZWToolsWindow.GetRoot();

		DeleteBZWComponentsOnChildren(root);
	}

	private static void DeleteBZWComponentsOnChildren(GameObject obj)
	{
		DeleteBZWComponents(obj);
		foreach(Transform t in obj.transform)
			DeleteBZWComponentsOnChildren(t.gameObject);
	}

	private static void DeleteBZWComponents(GameObject obj)
	{
		BZWBasicObject b = obj.GetComponent<BZWBasicObject>();
		if(b != null)
			GameObject.DestroyImmediate(b);

		b = obj.GetComponent<BZWUnknown>();
		if(b != null)
			GameObject.DestroyImmediate(b);

		b = obj.GetComponent<BZWWorld>();
		if(b != null)
			GameObject.DestroyImmediate(b); ;

		b = obj.GetComponent<BZWOptions>();
		if(b != null)
			GameObject.DestroyImmediate(b);

		b = obj.GetComponent<BZWPhaseableObject>();
		if(b != null)
			GameObject.DestroyImmediate(b);

		b = obj.GetComponent<BZWPyramid>();
		if(b != null)
			GameObject.DestroyImmediate(b);

		b = obj.GetComponent<BZWBox>();
		if(b != null)
			GameObject.DestroyImmediate(b);

		b = obj.GetComponent<BZWBase>();
		if(b != null)
			GameObject.DestroyImmediate(b);

		b = obj.GetComponent<BZWLink>();
		if(b != null)
			GameObject.DestroyImmediate(b);

		b = obj.GetComponent<BZWTeleporter>();
		if(b != null)
			GameObject.DestroyImmediate(b);

		b = obj.GetComponent<BZWZone>();
		if(b != null)
			GameObject.DestroyImmediate(b);
	}
}
