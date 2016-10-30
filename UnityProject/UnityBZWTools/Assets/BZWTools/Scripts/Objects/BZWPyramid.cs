using UnityEngine;
using System.Collections.Generic;

public class BZWPyramid : BZWBasicObject
{
	public bool FlipZ = false;
	public List<string> Attributes = new List<string>();

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public virtual void FromBZWObject(BZFlag.IO.Elements.Shapes.Pyramid py)
	{
		Name = py.Name;
		SetupFromPoisitionalbe(py);
		FlipZ = py.FlipZ;
		Attributes = py.Attributes;
		GUID = py.GUID;
	}

	public override BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		var obj = new BZFlag.IO.Elements.Shapes.Pyramid();

		OutputToPoisitionalbe(obj);
		obj.FlipZ = FlipZ;
		obj.Attributes = Attributes;
		obj.GUID = GUID;
		return obj;
	}


}