using UnityEngine;
using System.Collections.Generic;

using BZFlag.Data.Teams;
using BZFlag.Map.Elements.Shapes;
using BZFlag.Map.Elements;

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

	public override void FromBZWObject(Box b)
	{
		base.FromBZWObject(b);

		BZFlag.Map.Elements.Shapes.Base bs = b as Base;
		if(bs != null)
			TeamColor = (BaseColors)bs.TeamColor;
	}

	public override BasicObject ToBZWObject()
	{
		var obj = OutputToPhaseable(new Base()) as Base;
		obj.TeamColor = (TeamColors)TeamColor;
		return obj;
	}

    public override void Setup(BasicObject elementObject)
    {
        FromBZWObject(elementObject as Box);
        BuildGeometry();
    }

    public override void BuildGeometry()
    {
        List<GameObject> toKill = new List<GameObject>();
        foreach (Transform c in transform)
        {
            if (c.gameObject.name == "_Walls")
            {
                GameObject.Destroy(c.gameObject);
                break;
            }
        }

        GameObject newObj = new GameObject("_Walls");
        newObj.transform.SetParent(transform, false);

        BaseBuilder.BuildRoof(gameObject, this);
        BaseBuilder.BuildWalls(newObj, this);
    }
}
