using UnityEngine;
using System.Collections.Generic;

public class BZWTeleporter : BZWBasicObject
{
	public int Index = 0;

	public float Border = 0.0f;
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
	}

	public override BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		var obj = new BZFlag.IO.Elements.Shapes.Teleporter();

		OutputToPoisitionalbe(obj);
		obj.Border = Border;
		obj.Attributes = Attributes;
		obj.GUID = GUID;
		obj.Index = Index;
		return obj;
	}
}
