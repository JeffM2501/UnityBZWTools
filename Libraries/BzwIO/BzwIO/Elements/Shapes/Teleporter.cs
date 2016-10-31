using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Elements.Shapes
{
	public class Teleporter : PositionableObject
	{
		public static int TeleporterCount = 0;

		public float Border = 0;
		public int Index = 0;

		public Teleporter()
		{
			ObjectType = "Teleporter";

			TeleporterCount++;
			Index = TeleporterCount;
		}

		public override bool AddCodeLine(string command, string line)
		{
			if(command == "BORDER")
				float.TryParse(Reader.GetRestOfWords(line), out Border);
			else if(!base.AddCodeLine(command, line))
				return false;

			return true;
		}

		public override void BuildCode()
		{
			base.BuildCode();

			AddCode(1, "border", Border);
		}
	}
}
