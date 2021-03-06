﻿using UnityEngine;
using System.Collections.Generic;

public class BZWPhaseableObject : BZWBasicObject
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

	public virtual void FromBZWObject(BZFlag.Map.Elements.Shapes.PhaseableObject obj)
	{
		Name = obj.Name;
		SetupFromPoisitionalbe(obj);
		Attributes = obj.Attributes;
		Passable = obj.Passable;
		DriveThrough = obj.DriveThrough;
		ShootThrough = obj.ShootThrough;
		Ricochet = obj.Ricochet;
		GUID = obj.GUID;
	}

	protected virtual BZFlag.Map.Elements.Shapes.PhaseableObject OutputToPhaseable(BZFlag.Map.Elements.Shapes.PhaseableObject obj)
	{
		OutputToPoisitionalbe(obj);
		obj.Attributes = Attributes;
		obj.GUID = GUID;

		obj.Passable = Passable;
		obj.DriveThrough = DriveThrough;
		obj.ShootThrough = ShootThrough;
		obj.Ricochet = Ricochet;

		return obj;
	}

}
