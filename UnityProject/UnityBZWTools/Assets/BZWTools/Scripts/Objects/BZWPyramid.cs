using UnityEngine;
using System.Collections.Generic;
using BZFlag.IO.Elements;
using BZFlag.IO.Elements.Shapes;

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

	public virtual void FromBZWObject(Pyramid py)
	{
		base.FromBZWObject(py as PhaseableObject);
		FlipZ = py.FlipZ;
	}

	public override BasicObject ToBZWObject()
	{
		var obj = OutputToPhaseable(new Pyramid()) as BZFlag.IO.Elements.Shapes.Pyramid;
		obj.FlipZ = FlipZ;
		return obj;
	}

    public override void Setup(BasicObject elementObject)
    {
        FromBZWObject(elementObject as Pyramid);
        BuildGeometry();
    }

    public override void BuildGeometry()
    {
        PyramidBuilder.Build(gameObject, this);
    }
}