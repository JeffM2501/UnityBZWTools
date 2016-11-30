using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

using BZFlag.Map.Elements;
using BZFlag.Map.Elements.Shapes;

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

	public virtual void FromBZWObject(BZFlag.Map.Elements.Shapes.Teleporter tp)
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

	public override BZFlag.Map.Elements.BasicObject ToBZWObject()
	{
		var obj = new BZFlag.Map.Elements.Shapes.Teleporter();

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

		var front = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/BZWTools/Prefabs/Glyphs/TeleporterFront.prefab", typeof(GameObject)));
		front.transform.SetParent(transform, false);
	//	front.transform.localScale = new Vector3(1, transform.localScale.x / transform.localScale.y, 1);
		front.transform.localPosition = new Vector3(1.1f, 0.5f, 0);
	//	front.transform.Rotate(Vector3.up, -90);

		var back = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/BZWTools/Prefabs/Glyphs/TeleporterBack.prefab", typeof(GameObject)));
		back.transform.SetParent(transform, false);
	//	back.transform.localScale = new Vector3(1, transform.localScale.x / transform.localScale.y, 1);
		back.transform.localPosition = new Vector3(-1.1f, 0.5f, 0);
	//	back.transform.Rotate(Vector3.up, 90);

	}
}
