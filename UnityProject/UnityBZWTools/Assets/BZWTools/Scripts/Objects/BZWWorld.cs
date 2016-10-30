using UnityEngine;
using System;
using System.Collections.Generic;

public class BZWWorld : BZWBasicObject
{
	public float Size = 0;
	public float FlagHeight = 0;

	public bool NoWalls = false;
	public bool FreeCTFSpawns = false;

	public List<string> Attributes = new List<string>();

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public virtual void FromBZWObject(BZFlag.IO.Elements.World world)
	{
		Name = world.Name;
		Size = world.Size;
		FlagHeight = world.FlagHeight;
		NoWalls = world.NoWalls;
		FreeCTFSpawns = world.FreeCTFSpawns;

		Attributes = world.Attributes;
	}

	public override BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		var world =  new BZFlag.IO.Elements.World();

		world.Name = Name;
		world.Size = Size;
		world.NoWalls = NoWalls;
		world.FreeCTFSpawns = FreeCTFSpawns;
		world.Attributes = Attributes;

		return world;
	}
}
