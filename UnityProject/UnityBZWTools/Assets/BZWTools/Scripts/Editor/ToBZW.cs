using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System;

using BZFlag.IO;
using BZFlag.IO.Elements.Shapes;


public class ToBZW 
{

	public static Map BuildBZW()
	{
		Map map = new Map();

		BZWWorld root = GameObject.FindObjectOfType<BZWWorld>();
		if(root == null)
			return null;

		if(root.Name == string.Empty)
			map.WorldInfo.Name = "Untitled BZW";

		map.WorldInfo = root.ToBZWObject() as BZFlag.IO.Elements.World;

		for (int i = 0; i < root.gameObject.transform.childCount; i++)
		{
			var child = root.transform.GetChild(i);

			BZWBasicObject opt = child.GetComponent<BZWBasicObject>();
			if (opt != null)
				map.AddObject(opt.ToBZWObject());
		}

		map.PrepForSave();
		return map;
	}

	public static bool SaveBZW(string path, Map map)
	{
		FileInfo file = new FileInfo(path);
		if(file.Exists)
			file.Delete();

		FileStream fs = file.OpenWrite();
		StreamWriter sw = new StreamWriter(fs);
		Writer.WriteMap(sw, map);
		sw.Close();
		fs.Close();

		return false;
	}
}
