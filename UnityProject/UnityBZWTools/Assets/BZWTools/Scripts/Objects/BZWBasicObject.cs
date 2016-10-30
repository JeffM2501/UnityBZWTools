using UnityEngine;
using System.Collections;

public class BZWBasicObject : MonoBehaviour
{
	public string Name = string.Empty;

	// Use this for initialization
	void Start ()
	{
	
	}

	// Update is called once per frame
	void Update()
	{

	}

	public virtual void FromBZWObject(BZFlag.IO.Elements.BasicObject obj)
	{
		Name = obj.Name;
	}

	public virtual BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		return new BZFlag.IO.Elements.BasicObject();
	}

	public void SetupFromPoisitionalbe(BZFlag.IO.Elements.Shapes.PositionableObject p)
	{
		this.transform.localPosition = new Vector3(p.Position[0], p.Position[2], p.Position[1]);
		this.transform.localRotation = Quaternion.AngleAxis(-p.Rotation, Vector3.up);
		this.transform.localScale = new Vector3(p.Size[0], p.Size[2], p.Size[1]);
	}

	public void OutputToPoisitionalbe(BZFlag.IO.Elements.Shapes.PositionableObject p)
	{
		p.Name = Name;

		p.Position[0] = this.transform.localPosition.x;
		p.Position[2] = this.transform.localPosition.y;
		p.Position[1] = this.transform.localPosition.z;

		p.Rotation = -this.transform.localRotation.eulerAngles.y;

		p.Size[0] = this.transform.localScale.x;
		p.Size[2] = this.transform.localScale.y;
		p.Size[1] = this.transform.localScale.z;
	}
}
