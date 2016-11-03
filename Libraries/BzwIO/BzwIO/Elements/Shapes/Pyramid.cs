using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Elements.Shapes
{
	public class Pyramid : PhaseableObject
    {
		public bool FlipZ = false;

		public Pyramid()
		{
			ObjectType = "Pyramid";
		}

		public override bool AddCodeLine(string command, string line)
		{
			if(command == "FLIPZ")
				FlipZ = true;
			else if(!base.AddCodeLine(command, line))
				return false;

			return true;
		}

		public override void Finish()
		{
			base.Finish();

			if ((Size[0] < 0 |  Size[1] < 0 || Size[2] < 0) && ! FlipZ)
			{
				FlipZ = true;
				Size[0] = (float)Math.Abs(Size[0]);
				Size[1] = (float)Math.Abs(Size[1]);
				Size[2] = (float)Math.Abs(Size[2]);
			}
		}

		public override string BuildCode()
		{
			string name = base.BuildCode();

			if (FlipZ)
				AddCode(1, "flipz");

            return name;
		}
	}
}
