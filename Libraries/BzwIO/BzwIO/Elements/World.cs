using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Elements
{
	public class World : BasicObject
	{
		public float Size = 0;
		public float FlagHeight = 0;

		public bool NoWalls = false;
		public bool FreeCTFSpawns = false;

		public List<string> Attributes = new List<string>();

		public override bool AddCodeLine(string command, string line)
		{
			if(!base.AddCodeLine(command, line))
			{
				if(command == "SIZE")
					float.TryParse(Reader.GetRestOfWords(line), out Size);
				else if(command == "FLAGHEIGHT")
					float.TryParse(Reader.GetRestOfWords(line), out FlagHeight);
				else if(command == "NOWALLS")
					NoWalls = true;
				else if(command == "FREECTFSPAWNS")
					FreeCTFSpawns = true;
				else
					Attributes.Add(line);

				return true;
			}
			else
				return true;
		}
	}
}
