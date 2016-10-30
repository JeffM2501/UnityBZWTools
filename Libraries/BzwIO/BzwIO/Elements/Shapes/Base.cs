using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Elements.Shapes
{
	public class Base : Box
	{
		public enum TeamColors
		{
			Unknown = 0,
			Red = 1,
			Green = 2,
			Blue = 3,
			Purple = 4,
		}
		public TeamColors TeamColor = TeamColors.Unknown;

		public override bool AddCodeLine(string command, string line)
		{
			if(!base.AddCodeLine(command, line))
			{
				if(command == "COLOR")
				{
					int c = 0;
					int.TryParse(Reader.GetRestOfWords(line), out c);

					TeamColor = (TeamColors)c;
				}

				else
					return false;

				return true;
			}
			else
				return true;
		}

		public override void BuildCode()
		{
			base.BuildCode();

			AddCode(1, "color", (int)TeamColor);
		}
	}
}
