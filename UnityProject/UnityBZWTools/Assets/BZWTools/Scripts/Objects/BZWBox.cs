using UnityEngine;
using System.Collections.Generic;

public class BZWBox : BZPhaseableObject
{
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
		base.FromBZWObject(box as BZFlag.IO.Elements.Shapes.PhaseableObject);
	}

	public override BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		var obj =  OutputToPhaseable(new BZFlag.IO.Elements.Shapes.Box());
		return obj;
	}
}
