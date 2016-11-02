using UnityEngine;
using System;
using System.Collections.Generic;
using BZFlag.IO.Elements;

public class BZWLink : BZWBasicObject
{
	[Serializable]
	public class LinkDestination
	{
		public string Group = string.Empty;
		public string Target = string.Empty;
	
		[Serializable]
		public enum TargetSides
		{
			Front,
			Back,
		}
		public TargetSides TargetSide = TargetSides.Front;
		public bool Wildcard = false;

		public void FromBZW(BZFlag.IO.Elements.Link.PorterLink link)
		{
			Group = link.TargetGroup;
			Target = link.TargetName;
			Wildcard = link.Wildcard;
			TargetSide = link.Front ? TargetSides.Front : TargetSides.Back;
		}

		public BZFlag.IO.Elements.Link.PorterLink ToBZW( )
		{
			BZFlag.IO.Elements.Link.PorterLink link = new BZFlag.IO.Elements.Link.PorterLink();

			link.TargetGroup = Group ;
			link.TargetName = Target;
			link.Wildcard = Wildcard;
			link.Front = TargetSide == TargetSides.Front;

			return link;
		}
	}
	
	public LinkDestination From = new LinkDestination();
	public LinkDestination To = new LinkDestination();

	public List<string> Attributes = new List<string>();

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public virtual void FromBZWObject(BZFlag.IO.Elements.Link link)
	{
		Name = link.Name;

		From.FromBZW(link.From);
		To.FromBZW(link.To);

		Attributes = link.Attributes;
	}

	public override BZFlag.IO.Elements.BasicObject ToBZWObject()
	{
		var link = new BZFlag.IO.Elements.Link();

		link.Name = Name;
		link.From = From.ToBZW();
		link.To = To.ToBZW();

		link.Attributes = Attributes;

		return link;
	}

    public override void Setup(BasicObject elementObject)
    {
        FromBZWObject(elementObject as Link);
    }
}
