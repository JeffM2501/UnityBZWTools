using UnityEngine;
using System.Collections.Generic;

using BZFlag.IO.Elements;


public class BZWPhysics : BZWBasicObject
{
	public Vector3 Linear = Vector3.zero;
	public Vector3 Angular = Vector3.zero;
	public float Slide = 0;

	public string Death = string.Empty;
	public List<string> Attributes = new List<string>();

// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public virtual void FromBZWObject(BZFlag.IO.Elements.Physics obj)
	{
		GUID = obj.GUID;
		Name = obj.Name;
		Linear = new Vector3(obj.Linear.X, obj.Linear.Z, obj.Linear.Y);
		Angular = new Vector3(obj.Angular.X, obj.Angular.Y, obj.Angular.Z);
		Slide = obj.Slide;
		Death = obj.Death;
		Attributes = obj.Attributes;
	}

	public override BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		var phyz = new BZFlag.IO.Elements.Physics();
		phyz.Name = name;
		phyz.GUID = GUID;
		phyz.Linear = new BZFlag.IO.Types.Vector3F(Linear.x, Linear.z, Linear.y);
		phyz.Angular = new BZFlag.IO.Types.Vector3F(Angular.x, Linear.y, Angular.z);
		phyz.Slide = Slide;
		phyz.Death = Death.Trim();
		phyz.Attributes = Attributes;
		return phyz;
	}

	public override void Setup(BasicObject elementObject)
	{
		FromBZWObject(elementObject as BZFlag.IO.Elements.Physics);
		BuildGeometry();
	}

	public override void BuildGeometry()
	{
		base.BuildGeometry();
	}
}
