using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Elements
{
	public class Options : BasicObject
	{
		public List<string> Attributes = new List<string>();
		public Options()
		{
			ObjectType = "Options";
		}

		public override bool AddCodeLine(string command, string line)
		{
			if(!base.AddCodeLine(command, line))
			{
				Attributes.Add(line);
			}

			return true;
		}

		public override string BuildCode()
		{
			Code.Clear();

			foreach(var s in Attributes)
				AddCode(1, s);

			return ObjectType;
		}
	}
}
