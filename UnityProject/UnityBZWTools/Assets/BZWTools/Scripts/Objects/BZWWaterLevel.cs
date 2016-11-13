using UnityEngine;
using System.Collections.Generic;
using BZFlag.Map.Elements;
using BZFlag.Map.Elements.Shapes;

public class BZWWaterLevel : BZWBasicObject
{
    public List<string> Attributes = new List<string>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

	public override BasicObject ToBZWObject()
	{
		var water = new WaterLevel();
		water.Attributes = Attributes;
		water.Height = transform.localPosition.y;

		return water;
	}

	public virtual void FromBZWObject(BZFlag.Map.Elements.WaterLevel obj)
    {
        Name = obj.Name;

        this.transform.localPosition = new Vector3(0, obj.Height, 0);

        Attributes = obj.Attributes;
        GUID = obj.GUID;
    }

    public override void Setup(BasicObject elementObject)
    {
        FromBZWObject(elementObject as WaterLevel);
        BuildGeometry();
    }

    public override void BuildGeometry()
    {
        GroundBuilder.BuildGroundPlane(gameObject, 500, "Assets/BZWTools/StandardAssets/Textures/water.mat");
    }
}
