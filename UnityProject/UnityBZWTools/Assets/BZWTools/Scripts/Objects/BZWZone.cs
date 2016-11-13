using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


using BZFlag.Map.Elements.Shapes;
using BZFlag.Data.Teams;

public class BZWZone : BZWBasicObject
{
    public List<string> Flags = new List<string>();
    public List<string> ZoneFlags = new List<string>();
    public List<TeamColors> Safe = new List<TeamColors>();
    public List<TeamColors> Team = new List<TeamColors>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void FromBZWObject(BZFlag.Map.Elements.Shapes.Zone zone)
    {
        base.FromBZWObject(zone as BZFlag.Map.Elements.Shapes.PositionableObject);

        Flags = zone.Flags;
        ZoneFlags = zone.Flags;
        Safe = zone.Safe;
        Team = zone.Team;
    }

    public override BZFlag.Map.Elements.BasicObject ToBZWObject()
    {
        var obj = new BZFlag.Map.Elements.Shapes.Zone();
        OutputToPoisitionalbe(obj);
        obj.Flags = Flags;
        obj.ZoneFlags = ZoneFlags;
        obj.Team = Team;
        obj.Safe = Safe;
        return obj;
    }
}
