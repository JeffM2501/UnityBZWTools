using UnityEngine;
using System.Collections.Generic;

public class BZPhaseableObject : BZWBasicObject
{
	public bool Passable = false;
	public bool DriveThrough = false;
	public bool ShootThrough = false;
	public bool Ricochet = false;

	public List<string> Attributes = new List<string>();

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public virtual void FromBZWObject(BZFlag.IO.Elements.Shapes.PhaseableObject obj)
	{
		Name = obj.Name;
		SetupFromPoisitionalbe(obj);
		Attributes = obj.Attributes;
		GUID = obj.GUID;
	}

	protected virtual BZFlag.IO.Elements.Shapes.PhaseableObject OutputToPhaseable(BZFlag.IO.Elements.Shapes.PhaseableObject obj)
	{
		OutputToPoisitionalbe(obj);
		obj.Attributes = Attributes;
		obj.GUID = GUID;
		return obj;
	}

	public override BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		return OutputToPhaseable(new BZFlag.IO.Elements.Shapes.PhaseableObject());
	}
}
