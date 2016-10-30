using UnityEngine;
using System.Collections.Generic;
using System;

public class BZWUnknown : BZWBasicObject
{
	public string ObjectType = string.Empty;

	public List<string> CodeLines = new List<string>();


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public override void FromBZWObject(BZFlag.IO.Elements.BasicObject obj)
	{
		ObjectType = obj.ObjectType;
		Name = obj.Name;
		CodeLines = obj.Code;
	}

	public override BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		var obj = new BZFlag.IO.Elements.BasicObject();

		obj.ObjectType = ObjectType;
		obj.Code = CodeLines;

		return obj;
	}
}
