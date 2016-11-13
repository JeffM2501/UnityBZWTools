using UnityEngine;
using System.Collections.Generic;
using System;
using BZFlag.Map.Elements;

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

	public override void FromBZWObject(BasicObject obj)
	{
		ObjectType = obj.ObjectType;
		Name = obj.Name;
		CodeLines = obj.Attributes;
		GUID = obj.GUID;
	}

	public override BZFlag.Map.Elements.BasicObject ToBZWObject()
	{
		var obj = new BZFlag.Map.Elements.BasicObject();

		obj.ObjectType = ObjectType;
		obj.Attributes = CodeLines;
		obj.GUID = GUID;

		return obj;
	}

    public override void Setup(BasicObject elementObject)
    {
        FromBZWObject(elementObject);
        BuildGeometry();
    }
}
