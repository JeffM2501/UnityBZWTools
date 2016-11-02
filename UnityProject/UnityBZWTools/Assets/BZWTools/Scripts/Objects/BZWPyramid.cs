using UnityEngine;
using System.Collections.Generic;

public class BZWPyramid : BZWPhaseableObject
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
		base.FromBZWObject(py as BZFlag.IO.Elements.Shapes.PhaseableObject);
		FlipZ = py.FlipZ;
	}

	public override BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		var obj = OutputToPhaseable(new BZFlag.IO.Elements.Shapes.Pyramid()) as BZFlag.IO.Elements.Shapes.Pyramid;
		obj.FlipZ = FlipZ;
		return obj;
	}
}