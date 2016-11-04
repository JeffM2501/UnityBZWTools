using UnityEngine;
using UnityEditor;
using System.Collections;

using BZFlag.IO;
using BZFlag.IO.Elements;
using BZFlag.IO.Elements.Shapes;

public class BZWToolsWindow : EditorWindow
{
    public void OnGUI()
    {
        GUILayout.Label("Objects", EditorStyles.boldLabel);

        if (GUILayout.Button("Add Box"))
            AddBox();
        if (GUILayout.Button("Add Pyramid"))
            AddPyramid();
        if (GUILayout.Button("Add Teleporter"))
            AddTeleporter();
        if (GUILayout.Button("Add Link"))
            AddLink();
        if (GUILayout.Button("Add Base"))
            AddBase();
        if (GUILayout.Button("Add WaterLevel"))
            AddWaterLevel();
        if (GUILayout.Button("Add Zone"))
            AddZone();

        EditorGUILayout.BeginVertical();
        GUILayout.Label("Geometry", EditorStyles.boldLabel);
        if (GUILayout.Button("Rebuild Geometry"))
            RebuildGeo();

        EditorGUILayout.EndVertical();
    }

    public static GameObject GetRoot()
    {
        BZWWorld root = GameObject.FindObjectOfType<BZWWorld>();
        if (root != null)
            return root.gameObject;

        return FromBZW.CreateNewBZWRoot(new Map());
    }

    public void RebuildGeo()
    {
        RebuildGeoInChildren(GetRoot());
    }


    public BZWBasicObject GetMapObject(GameObject obj)
    {
        BZWBasicObject b = obj.GetComponent<BZWBasicObject>();
        if (b != null)
            return b;

        b = obj.GetComponent<BZWUnknown>();
        if (b != null)
            return b;

        b = obj.GetComponent<BZWWorld>();
        if (b != null)
            return b;

        b = obj.GetComponent<BZWOptions>();
        if (b != null)
            return b;

        b = obj.GetComponent<BZWPhaseableObject>();
        if (b != null)
            return b;

        b = obj.GetComponent<BZWPyramid>();
        if (b != null)
            return b;

        b = obj.GetComponent<BZWBox>();
        if (b != null)
            return b;

        b = obj.GetComponent<BZWBase>();
        if (b != null)
            return b;

        b = obj.GetComponent<BZWLink>();
        if (b != null)
            return b;

        b = obj.GetComponent<BZWTeleporter>();
        if (b != null)
            return b;

        b = obj.GetComponent<BZWZone>();
        if (b != null)
            return b;

        return null;
    }

    public void RebuildGeoInChildren(GameObject obj)
    {
        foreach(Transform child in obj.transform)
        {
            BZWBasicObject b = GetMapObject(child.gameObject);
            if (b != null)
                b.BuildGeometry();
            RebuildGeoInChildren(child.gameObject);
        }
    }

    public void AddBox()
    {
        Box box = new Box();
        box.Size = new BZFlag.IO.Types.Vector3F(1, 1, 1);
        box.Name = "NewBox_" + box.GUID;
        FromBZW.NewMapObject<BZWBox>(box);
    }

    public void AddPyramid()
    {
        Pyramid p = new Pyramid();
        p.Size = new BZFlag.IO.Types.Vector3F(1, 1, 1);
        p.Name = "NewPyramid_" + p.GUID;
        FromBZW.NewMapObject<BZWPyramid>(p);
    }

    public void AddTeleporter()
    {
        Teleporter t = new Teleporter();
        t.Size = new BZFlag.IO.Types.Vector3F(1, 1, 1);
        t.Border = 0.25f;
        t.Name = "NewTeleporter_" + t.GUID;
        FromBZW.NewMapObject<BZWTeleporter>(t);
    }

    public void AddLink()
    {
        Link t = new Link();
        t.Name = "NewLink_" + t.GUID;
        FromBZW.NewMapObject<BZWLink>(t);
    }

    public void AddBase()
    {
        Base t = new Base();
        t.Name = "NewBase_" + t.GUID;
        FromBZW.NewMapObject<BZWBase>(t);
    }

    public void AddWaterLevel()
    {
        WaterLevel t = new WaterLevel();
        t.Name = "WaterLevel";
        FromBZW.NewMapObject<BZWWaterLevel>(t);
    }

    public void AddZone()
    {
        Zone t = new Zone();
        t.Name = "NewZone_" + t.GUID;
        FromBZW.AddMapObject<BZWZone>(FromBZW.GetZonePrefab(), t);
    }
}
