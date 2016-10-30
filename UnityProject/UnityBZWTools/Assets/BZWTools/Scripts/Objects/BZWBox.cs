using UnityEngine;
using System.Collections.Generic;

public class BZWBox : BZWBasicObject
{
	public List<string> Attributes = new List<string>();

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public virtual void FromBZWObject(BZFlag.IO.Elements.Shapes.Box box)
	{
		Name = box.Name;
		SetupFromPoisitionalbe(box);
		Attributes = box.Attributes;
		GUID = box.GUID;
	}

	public override BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		var obj = new BZFlag.IO.Elements.Shapes.Box();

		OutputToPoisitionalbe(obj);
		obj.Attributes = Attributes;
		obj.GUID = GUID;

		return obj;
	}
}
