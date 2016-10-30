using UnityEngine;
using System.Collections;

public class BZWPyramid : BZWBasicObject
{
	public bool FlipZ = false;

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
	}

	public override BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		var obj = new BZFlag.IO.Elements.Shapes.Pyramid();

		OutputToPoisitionalbe(obj);
		obj.FlipZ = FlipZ;

		return obj;
	}


}