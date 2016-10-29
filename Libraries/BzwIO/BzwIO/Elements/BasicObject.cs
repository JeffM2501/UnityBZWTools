﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Elements
{
	public class BasicObject
	{
		public string ObjectType = string.Empty;
		public List<string> Code = new List<string>();

		public string Name = string.Empty;

		public virtual bool AddCodeLine(string command, string line)
		{
			Code.Add(line);

			if(command == "NAME")
				Name = Reader.GetRestOfWords(line);
			else
				return false;

			return true;
		}

		public virtual void Finish()
		{

		}
	}
}