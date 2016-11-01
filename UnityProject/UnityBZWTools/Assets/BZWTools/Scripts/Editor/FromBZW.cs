using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System;

using BZFlag.IO;
using BZFlag.IO.Elements;
using BZFlag.IO.Elements.Shapes;

public class FromBZW
{

	static void SetupRootObject(GameObject worldObj, Map map)
	{
		worldObj.AddComponent<BZWWorld>();
		BZWWorld mapRoot = worldObj.GetComponent<BZWWorld>();
		mapRoot.FromBZWObject(map.WorldInfo);

		GameObject grass = new GameObject("_Grass");
		grass.transform.SetParent(worldObj.transform, false);
		GroundBuilder.BuildGrass(grass, mapRoot);

		if(!map.WorldInfo.NoWalls)
		{
			GameObject walls = new GameObject("_Walls");
			walls.transform.SetParent(grass.transform, false);
			GroundBuilder.BuildWalls(walls, mapRoot);
		}

		// add map options
		GameObject optionsObject = new GameObject("Options");
		optionsObject.AddComponent<BZWOptions>();
		BZWOptions opt = optionsObject.GetComponent<BZWOptions>();
		optionsObject.transform.SetParent(worldObj.transform, false);
		opt.FromBZWObject(map.WorldOptions);
	}
	static GameObject SetupLink(GameObject obj, Link link)
	{
		BZLink ptr = obj.AddComponent<BZLink>() as BZLink;
		ptr.FromBZWObject(link);
		return obj;
	}

	static GameObject SetupTeleporter(GameObject obj, Teleporter tp)
	{
		GameObject newObj = new GameObject("sides");
		newObj.transform.SetParent(obj.transform, false);

		BZWTeleporter ptr = obj.AddComponent<BZWTeleporter>() as BZWTeleporter;
		ptr.FromBZWObject(tp);
		TeleporterBuilder.BuildField(obj, ptr);
		TeleporterBuilder.BuildFrame(newObj, ptr);

		return obj;
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
		BoxBuilder.BuildWalls(newObj, bx);

		return obj;
	}

	static GameObject SetupBase(GameObject obj, Base b)
	{
		GameObject newObj = new GameObject("walls");
		newObj.transform.SetParent(obj.transform, false);

		BZWBase bx = obj.AddComponent<BZWBase>() as BZWBase;
		bx.FromBZWObject(b);

		BaseBuilder.BuildRoof(obj, bx);
		BaseBuilder.BuildWalls(newObj, bx);

		return obj;
	}

	public static GameObject CreateNewBZWRoot(Map map)
	{
		// build the root world
		GameObject worldObj = new GameObject("World_" + map.WorldInfo.Name);
		SetupRootObject(worldObj, map);
		return worldObj;
	}

	public static void ReadUserBZW(string path)
	{
		if(path != string.Empty)
		{
			FileInfo file = new FileInfo(path);
			StreamReader sr = file.OpenText();

			var map = Reader.ReadMap(sr);
			sr.Close();
			GameObject worldObj = CreateNewBZWRoot(map);

			// add all the sub objects
			int count = 1;
			foreach(var m in map.Objects)
			{
				string name = string.Empty;
				if(m.Name != string.Empty)
					name = m.Name;
				else
					name = m.ObjectType + "_" + count.ToString();

				if(m as BZFlag.IO.Elements.Shapes.Base != null)
				{
					GameObject newObj = new GameObject(name);
					newObj.transform.SetParent(worldObj.transform, false);
					SetupBase(newObj, m as BZFlag.IO.Elements.Shapes.Base);
				}
				else if(m as BZFlag.IO.Elements.Shapes.Box != null)
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
				else if(m as BZFlag.IO.Elements.Shapes.Teleporter != null)
				{
					GameObject newObj = new GameObject(name);
					newObj.transform.SetParent(worldObj.transform, false);
					SetupTeleporter(newObj, m as BZFlag.IO.Elements.Shapes.Teleporter);
				}
				else if(m as BZFlag.IO.Elements.Link != null)
				{
					GameObject newObj = new GameObject(name);
					newObj.transform.SetParent(worldObj.transform, false);
					SetupLink(newObj, m as BZFlag.IO.Elements.Link);
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
