using UnityEngine;
using System.Collections.Generic;

using BZFlag.IO.Elements;

public class BZWOptions : BZWBasicObject
{
	public List<string> Options = new List<string>();

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public virtual void FromBZWObject(Options obj)
	{
		Name = obj.Name;
		Options = obj.Attributes;
	}

	public override BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		var options = new BZFlag.IO.Elements.Options();

		options.Attributes = Options;
		return options;
	}

    public override void Setup(BasicObject elementObject)
    {
        FromBZWObject(elementObject as Options);
        BuildGeometry();
    }

    public override void BuildGeometry()
    {
        base.BuildGeometry();
    }
}
