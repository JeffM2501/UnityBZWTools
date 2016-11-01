using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BZFlag.IO.Types;

namespace BZFlag.IO.Elements.Shapes
{
	public abstract class PositionableObject : BasicObject
	{
		public Vector3F Position = new Vector3F();
		public float Rotation = 0;
		public Vector3F Size = new Vector3F();

		public List<string> Attributes = new List<string>();

		public override bool AddCodeLine(string command, string line)
		{
			if(!base.AddCodeLine(command, line))
			{
				if(command == "POSITION")
					Position = Vector3F.Read(Reader.GetRestOfWords(line));
				else if(command == "SIZE")
					Size = Vector3F.Read(Reader.GetRestOfWords(line));
				else if(command == "ROTATION")
					float.TryParse(Reader.GetRestOfWords(line), out Rotation);
				else
					Attributes.Add(line);
			}

			return true;
		}

		public override void Finish()
		{

		}

		public override string BuildCode()
		{
			Code.Clear();
			if(Name != string.Empty)
				AddCode(1, "name", Name);

			AddCode(1, "position", Position);
			AddCode(1, "rotation", Rotation);
			AddCode(1, "size", Size);

            return ObjectType;
		}
	}
}
