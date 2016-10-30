using UnityEngine;
using System.Collections;

public class BZWBase : BZWBox
{
	
	public enum BaseColors
	{
		Red = 1,
		Green = 2,
		Blue = 3,
		Purple = 4,
	}

	public BaseColors TeamColor = BaseColors.Red;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public override void FromBZWObject(BZFlag.IO.Elements.Shapes.Box b)
	{
		Name = b.Name;
		SetupFromPoisitionalbe(b);

		BZFlag.IO.Elements.Shapes.Base bs = b as BZFlag.IO.Elements.Shapes.Base;
		if(bs != null)
			TeamColor = (BaseColors)bs.TeamColor;
	}

	public override BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		var obj = new BZFlag.IO.Elements.Shapes.Base();

		OutputToPoisitionalbe(obj);
		obj.TeamColor = (BZFlag.IO.Elements.Shapes.Base.TeamColors)TeamColor;

		return obj;
	}
}
