using UnityEngine;
using System.Collections;

public class BZWBox : BZWBasicObject
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public virtual void FromBZWObject(BZFlag.IO.Elements.Shapes.Box box)
	{
		Name = box.Name;
		SetupFromPoisitionalbe(box);
	}

	
}
