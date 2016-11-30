using UnityEngine;
using System;
using System.Collections.Generic;
using BZFlag.Map.Elements;

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

	public virtual void FromBZWObject(BZFlag.Map.Elements.World world)
	{
		Name = world.Name;
		Size = world.Size;
		FlagHeight = world.FlagHeight;
		NoWalls = world.NoWalls;
		FreeCTFSpawns = world.FreeCTFSpawns;

		Attributes = world.Attributes;
	}

	public override BZFlag.Map.Elements.BasicObject ToBZWObject()
	{
		var world =  new BZFlag.Map.Elements.World();

		world.Name = Name;
		world.Size = Size;
		world.NoWalls = NoWalls;
		world.FreeCTFSpawns = FreeCTFSpawns;
		world.Attributes = Attributes;

		return world;
	}

    public override void Setup(BasicObject elementObject)
    {
        World world = elementObject as World;

        FromBZWObject(world);
        BuildGeometry();
    }

    public override void BuildGeometry()
    {
        foreach(Transform c in transform)
        {
            if (c.gameObject.name == "_Grass")
            {
                GameObject.Destroy(c.gameObject);
                break;
            }
        }

        GameObject grass = new GameObject("_Grass");
        grass.transform.SetParent(transform, false);
        GroundBuilder.BuildGrass(grass, this);

        if (!NoWalls)
        {
            GameObject walls = new GameObject("_Walls");
            walls.transform.SetParent(grass.transform, false);
            GroundBuilder.BuildWalls(walls, this);
        }
    }
}
