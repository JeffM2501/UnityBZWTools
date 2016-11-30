using UnityEngine;
using System.Collections.Generic;
using BZFlag.Map.Elements;
using BZFlag.Map.Elements.Shapes;

public class BZWBox : BZWPhaseableObject
{
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public virtual void FromBZWObject(Box box)
	{
		base.FromBZWObject(box as PhaseableObject);
	}

	public override BasicObject ToBZWObject()
	{
		var obj =  OutputToPhaseable(new Box());
		return obj;
	}

    public override void Setup(BasicObject elementObject)
    {
        FromBZWObject(elementObject as Box);
        BuildGeometry();
    }

    public override void BuildGeometry()
    {
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

        BoxBuilder.BuildRoof(gameObject, this);
        BoxBuilder.BuildWalls(newObj, this);
    }
}
