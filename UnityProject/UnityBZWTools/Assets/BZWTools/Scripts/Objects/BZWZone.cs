using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


using BZFlag.IO.Elements.Shapes;

public class BZWZone : BZWBasicObject
{
    public List<string> Flags = new List<string>();
    public List<string> ZoneFlags = new List<string>();
    public List<Base.TeamColors> Safe = new List<Base.TeamColors>();
    public List<Base.TeamColors> Team = new List<Base.TeamColors>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void FromBZWObject(BZFlag.IO.Elements.Shapes.Zone zone)
    {
        base.FromBZWObject(zone as BZFlag.IO.Elements.Shapes.PositionableObject);

        Flags = zone.Flags;
        ZoneFlags = zone.Flags;
        Safe = zone.Safe;
        Team = zone.Team;
    }

    public override BZFlag.IO.Elements.BasicObject ToBZWObject()
    {
        var obj = new BZFlag.IO.Elements.Shapes.Zone();
        OutputToPoisitionalbe(obj);
        obj.Flags = Flags;
        obj.ZoneFlags = ZoneFlags;
        obj.Team = Team;
        obj.Safe = Safe;
        return obj;
    }
}
