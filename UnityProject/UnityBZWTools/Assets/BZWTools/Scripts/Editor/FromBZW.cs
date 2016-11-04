using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System;

using BZFlag.IO;
using BZFlag.IO.Elements;
using BZFlag.IO.Elements.Shapes;

using System.Collections.Generic;

public class FromBZW
{

    public static void SetupRootObject(GameObject worldObj, Map map)
	{
        BZWWorld mapRoot = worldObj.AddComponent<BZWWorld>();
        mapRoot.Setup(map.WorldInfo);

        // add map options
        GameObject optionsObject = AddToRoot(worldObj ,new GameObject("Options"));
        optionsObject.AddComponent<BZWOptions>().Setup(map.WorldOptions);
	}

    public static GameObject AddToRoot(GameObject mapRoot, GameObject obj)
    {
        obj.transform.SetParent(mapRoot.transform, false);
        return obj;
    }

    public static GameObject NewMapObject(GameObject obj)
    {
        AddToRoot(BZWToolsWindow.GetRoot(), obj);
        return obj;
    }

    public static GameObject NewMapObject<T>(BZFlag.IO.Elements.BasicObject obj) where T : BZWBasicObject
    {
        string name = obj.Name;
        if (name == string.Empty)
            name = obj.ObjectType + "_" + obj.GUID;

        var gb = new GameObject(name);
        AddToRoot(BZWToolsWindow.GetRoot(), gb);
        T bzw = gb.AddComponent<T>();
        bzw.Setup(obj);

        return gb;
    }

    public static GameObject AddMapObject<T>(GameObject gb, BZFlag.IO.Elements.BasicObject obj) where T : BZWBasicObject
    {
        AddToRoot(BZWToolsWindow.GetRoot(), gb);
        T bzw = gb.AddComponent<T>();
		string name = obj.Name;
		if(name == string.Empty)
			name = obj.ObjectType + "_" + obj.GUID;
		gb.name = name;

		bzw.Setup(obj);

        return gb;
    }

	public static GameObject CreateNewBZWRoot(Map map)
	{
		// build the root world
		GameObject worldObj = new GameObject("World_" + map.WorldInfo.Name);
		SetupRootObject(worldObj, map);
		return worldObj;
	}
    public static GameObject GetZonePrefab()
    {
		var obj = AssetDatabase.LoadAssetAtPath("Assets/BZWTools/Prefabs/BZWZone.prefab", typeof(GameObject));
		//var obj = AssetDatabase.LoadAssetAtPath("Assets/BZWTools/Prefabs/Meshes/Zone.obj", typeof(GameObject));

		return (GameObject)GameObject.Instantiate(obj);
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
                    NewMapObject<BZWBase>(m);
                else if(m as BZFlag.IO.Elements.Shapes.Box != null)
                    NewMapObject<BZWBox>(m);
                else if(m as BZFlag.IO.Elements.Shapes.Pyramid != null)
                    NewMapObject<BZWPyramid>(m);
                else if(m as BZFlag.IO.Elements.Shapes.Teleporter != null)
                    NewMapObject<BZWTeleporter>(m);
                else if(m as BZFlag.IO.Elements.Link != null)
                    NewMapObject<BZWLink>(m);
                else if (m as BZFlag.IO.Elements.WaterLevel != null)
                    NewMapObject<BZWWaterLevel>(m);
				else if(m as BZFlag.IO.Elements.Physics != null)
					NewMapObject<BZWPhysics>(m);
				else if(m as BZFlag.IO.Elements.Shapes.Zone != null)
					AddMapObject<BZWZone>(GetZonePrefab(), m);
				else
                    NewMapObject<BZWUnknown>(m);
				count++;
			}

			Debug.Log("Map Loaded: " + map.Objects.Count.ToString() + " objects");
		}
	}
}
