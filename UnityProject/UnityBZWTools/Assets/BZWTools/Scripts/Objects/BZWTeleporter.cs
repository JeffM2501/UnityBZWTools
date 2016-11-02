﻿using UnityEngine;
using System;
using System.Collections.Generic;

using BZFlag.IO.Elements;
using BZFlag.IO.Elements.Shapes;

public class BZWTeleporter : BZWBasicObject
{
	public int Index = 0;

	public float Border = 0.0f;
	public bool Horizontal = false;
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

	public virtual void FromBZWObject(BZFlag.IO.Elements.Shapes.Teleporter tp)
	{
		Name = tp.Name;
		SetupFromPoisitionalbe(tp);
		Border = tp.Border;
		Attributes = tp.Attributes;
		GUID = tp.GUID;
		Index = tp.Index;
		Ricochet = tp.Ricochet;
		Horizontal = tp.Horizontal;
	}

	public override BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		var obj = new BZFlag.IO.Elements.Shapes.Teleporter();

		if(name == string.Empty)
			name = Index.ToString();

		OutputToPoisitionalbe(obj);
		obj.Border = Border;
		obj.Attributes = Attributes;
		obj.GUID = GUID;
		obj.Index = Index;
		obj.Ricochet = Ricochet;
		obj.Horizontal = Horizontal;
		return obj;
	}

    public override void Setup(BasicObject elementObject)
    {
        Teleporter tp = elementObject as Teleporter;

        FromBZWObject(tp);
        BuildGeometry();
    }

    public override void BuildGeometry()
    {
        List<GameObject> toKill = new List<GameObject>();
        foreach (Transform c in transform)
        {
            if (c.gameObject.name == "_Sides")
            {
                GameObject.Destroy(c.gameObject);
                break;
            }
        }

        GameObject newObj = new GameObject("_Sides");
        newObj.transform.SetParent(transform, false);
        TeleporterBuilder.BuildField(gameObject, this);
        TeleporterBuilder.BuildFrame(newObj, this);
    }
}
