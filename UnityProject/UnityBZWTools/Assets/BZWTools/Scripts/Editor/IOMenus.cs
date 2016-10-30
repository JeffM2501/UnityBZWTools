using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System;

using BZFlag.IO;
using BZFlag.IO.Elements.Shapes;

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

	static void SetupRootObject(GameObject worldObj, Map map)
	{
		worldObj.AddComponent<BZWWorld>();
		BZWWorld mapRoot = worldObj.GetComponent<BZWWorld>();
		mapRoot.FromBZWObject(map.WorldInfo);

		GameObject grass = new GameObject("_Grass");
		grass.transform.SetParent(worldObj.transform, false);
		GroundBuilder.BuildGrass(grass, mapRoot);

		if (!map.WorldInfo.NoWalls)
		{
			GameObject walls = new GameObject("_Walls");
			walls.transform.SetParent(grass.transform, false);
			GroundBuilder.BuildWalls(walls, mapRoot);
		}

		// add map options
		GameObject optionsObject = new GameObject("Options");
		worldObj.AddComponent<BZWOptions>();
		BZWOptions opt = worldObj.GetComponent<BZWOptions>();
		optionsObject.transform.SetParent(worldObj.transform, false);
		opt.FromBZWObject(map.WorldOptions);
	}

	static GameObject SetupPyramid(GameObject obj, Pyramid pyr)
	{
		BZWPyramid py = obj.AddComponent<BZWPyramid>() as BZWPyramid;
		py.FromBZWObject(pyr);
		PyramidBuilder.Build(obj, py);

		return obj;
	}

	static GameObject SetupBox(GameObject obj, Box box)
	{
		GameObject newObj = new GameObject("walls");
		newObj.transform.SetParent(obj.transform, false);

		BZWBox bx = obj.AddComponent<BZWBox>() as BZWBox;
		bx.FromBZWObject(box);

		BoxBuilder.BuildRoof(obj, bx);
		BoxBuilder.BuildWalls(obj, bx);

		return obj;
	}

	static void ReadUserBZW(string path)
	{
		if(path != string.Empty)
		{
			FileInfo file = new FileInfo(path);
			StreamReader sr = file.OpenText();

			var map = Reader.ReadMap(sr);
			sr.Close();

			// build the root world
			GameObject worldObj = new GameObject("World_" + map.WorldInfo.Name);
			SetupRootObject(worldObj, map);

			// add all the sub objects
			int count = 1;
			foreach(var m in map.Objects)
			{
				string name = string.Empty;
				if(m.Name != string.Empty)
					name =  m.Name;
				else
					name = m.ObjectType + "_" + count.ToString();

				if (m as BZFlag.IO.Elements.Shapes.Box != null)
				{
					GameObject newObj = new GameObject(name);
					newObj.transform.SetParent(worldObj.transform, false);
					SetupBox(newObj, m as BZFlag.IO.Elements.Shapes.Box);
				}
				else if(m as BZFlag.IO.Elements.Shapes.Pyramid != null)
				{
					GameObject newObj = new GameObject(name);
					newObj.transform.SetParent(worldObj.transform, false);
					SetupPyramid(newObj, m as BZFlag.IO.Elements.Shapes.Pyramid);
				}
				else
				{
					GameObject newObj = new GameObject(name);
					newObj.AddComponent<BZWUnknown>();
					BZWUnknown unk = newObj.GetComponent<BZWUnknown>();
					newObj.transform.SetParent(worldObj.transform, false);
					unk.FromBZWObject(m);
				}
				count++;
			}

			Debug.Log("Map Loaded: " + map.Objects.Count.ToString() + " objects");
		}
	}
}
