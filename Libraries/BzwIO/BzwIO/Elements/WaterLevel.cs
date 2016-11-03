using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Elements
{
	public class WaterLevel : BasicObject
	{
		public float Height = 0;
		public List<string> Attributes = new List<string>();

		public WaterLevel()
		{
			ObjectType = "WaterLevel";
		}

		public override bool AddCodeLine(string command, string line)
		{
			if (!base.AddCodeLine(command, line))
			{
				if(command == "HEIGHT")
					float.TryParse(Reader.GetRestOfWords(line), out Height);
				else
					Attributes.Add(line);
			}

			return true;
		}

		public override string BuildCode()
		{
			Code.Clear();

			AddCode(1, "height", Height);
			foreach(var s in Attributes)
				AddCode(2, s, string.Empty);

            return ObjectType;
		}
	}
}
