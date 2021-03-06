﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System;

using BZFlag.IO.BZW;
using BZFlag.Map;
using BZFlag.Map.Elements;
using BZFlag.Map.Elements.Shapes;

using System.Collections.Generic;

public class FromBZW
{

    public static void SetupRootObject(GameObject worldObj, WorldMap map)
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

    public static GameObject NewMapObject<T>(BZFlag.Map.Elements.BasicObject obj) where T : BZWBasicObject
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

    public static GameObject AddMapObject<T>(GameObject gb, BZFlag.Map.Elements.BasicObject obj) where T : BZWBasicObject
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

	public static GameObject CreateNewBZWRoot(WorldMap map)
	{
		// build the root world
		GameObject worldObj = new GameObject("World_" + map.WorldInfo.Name);
		SetupRootObject(worldObj, map);
		return worldObj;
	}
    public static GameObject GetZonePrefab()
    {
		var obj = AssetDatabase.LoadAssetAtPath("Assets/BZWTools/Prefabs/BZWZone.prefab", typeof(GameObject));
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
			CreateNewBZWRoot(map);

			// add all the sub objects
			int count = 1;
			foreach(var m in map.Objects)
			{
				if(m as BZFlag.Map.Elements.Shapes.Base != null)
                    NewMapObject<BZWBase>(m);
                else if(m as BZFlag.Map.Elements.Shapes.Box != null)
                    NewMapObject<BZWBox>(m);
                else if(m as BZFlag.Map.Elements.Shapes.Pyramid != null)
                    NewMapObject<BZWPyramid>(m);
                else if(m as BZFlag.Map.Elements.Shapes.Teleporter != null)
                    NewMapObject<BZWTeleporter>(m);
                else if(m as BZFlag.Map.Elements.Link != null)
                    NewMapObject<BZWLink>(m);
                else if (m as BZFlag.Map.Elements.WaterLevel != null)
                    NewMapObject<BZWWaterLevel>(m);
				else if(m as BZFlag.Map.Elements.Physics != null)
					NewMapObject<BZWPhysics>(m);
				else if(m as BZFlag.Map.Elements.Shapes.Zone != null)
					AddMapObject<BZWZone>(GetZonePrefab(), m);
				else
                    NewMapObject<BZWUnknown>(m);
				count++;
			}

			Debug.Log("Map Loaded: " + map.Objects.Count.ToString() + " objects");
		}
	}
}
