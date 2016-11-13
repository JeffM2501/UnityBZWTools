using UnityEngine;
using UnityEditor;
using System.Collections;

using BZFlag.IO.BZW;
using BZFlag.Map;
using BZFlag.Map.Elements;
using BZFlag.Map.Elements.Shapes;

public class BZWToolsWindow : EditorWindow
{
	protected int LastSelectedOtherObject = 0;
	protected string[] OtherObjects = new string[]
	{
		"Physics",
		"Zone",
	};
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

		EditorGUILayout.BeginHorizontal();
		LastSelectedOtherObject = EditorGUILayout.Popup("", LastSelectedOtherObject, OtherObjects);
		if(GUILayout.Button("Add"))
			AddOther();
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginVertical();
        GUILayout.Label("Geometry", EditorStyles.boldLabel);
        if (GUILayout.Button("Rebuild Geometry"))
            RebuildGeo();

        EditorGUILayout.EndVertical();
    }

	void AddOther()
	{
		switch(LastSelectedOtherObject)
		{
			case 0:
				AddPhysics();
			
				break;
			case 1:
				AddZone();
				break; 

			default:
				break;
		}
	}

    public static GameObject GetRoot()
    {
        BZWWorld root = GameObject.FindObjectOfType<BZWWorld>();
        if (root != null)
            return root.gameObject;

        return FromBZW.CreateNewBZWRoot(new WorldMap());
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

		b = obj.GetComponent<BZWPhysics>();
		if(b != null)
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
        box.Size = new BZFlag.Data.Types.Vector3F(1, 1, 1);
        box.Name = "NewBox_" + box.GUID;
        FromBZW.NewMapObject<BZWBox>(box);
    }

    public void AddPyramid()
    {
        Pyramid p = new Pyramid();
        p.Size = new BZFlag.Data.Types.Vector3F(1, 1, 1);
        p.Name = "NewPyramid_" + p.GUID;
        FromBZW.NewMapObject<BZWPyramid>(p);
    }

    public void AddTeleporter()
    {
        Teleporter t = new Teleporter();
        t.Size = new BZFlag.Data.Types.Vector3F(0.25f, 3, 10);
        t.Border = 0.5f;
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
		t.Height = 1;
        FromBZW.NewMapObject<BZWWaterLevel>(t);
    }

    public void AddZone()
    {
        Zone t = new Zone();
        t.Name = "NewZone_" + t.GUID;
        FromBZW.AddMapObject<BZWZone>(FromBZW.GetZonePrefab(), t);
	}

	public void AddPhysics()
	{
		BZFlag.Map.Elements.Physics t = new BZFlag.Map.Elements.Physics();
		t.Name = "NewPhysics_" + t.GUID;
		FromBZW.NewMapObject<BZWPhysics>(t);
	}
}
