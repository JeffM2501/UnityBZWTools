using UnityEngine;
using System.Collections.Generic;

public class BZWOptions : BZWBasicObject
{
	public List<string> Options = new List<string>();

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
		Options = obj.Code;
	}

	public override BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		var options = new BZFlag.IO.Elements.Options();

		options.Code = Options;
		return options;
	}
}
