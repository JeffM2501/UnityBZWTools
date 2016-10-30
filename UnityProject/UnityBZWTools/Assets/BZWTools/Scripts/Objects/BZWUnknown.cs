using UnityEngine;
using System.Collections.Generic;
using System;

public class BZWUnknown : BZWBasicObject
{
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
		Name = obj.Name;
		CodeLines = obj.Code;
	}
}
