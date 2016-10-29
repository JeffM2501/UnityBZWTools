﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Elements.Shapes
{
	public class PositionableObject : BasicObject
	{
		public float[] Position = new float[] { 0, 0, 0 };
		public float Rotation = 0;
		public float[] Size = new float[] { 0, 0, 0 };

		protected float[] ReadVector3(string line)
		{
			float[] v = new float[] { 0, 0, 0 };

			var vec = Reader.ParseFloatVector(line);
			for(int i = 0; i < 3 && i < vec.Count; i++)
				v[i] = vec[i];

			return v;
		}

		public override bool AddCodeLine(string command, string line)
		{
			if(!base.AddCodeLine(command, line))
			{
				if(command == "POSITION")
					Position = ReadVector3(Reader.GetRestOfWords(line));
				else if(command == "SIZE")
					Size = ReadVector3(Reader.GetRestOfWords(line));
				else if(command == "ROTATION")
					float.TryParse(Reader.GetRestOfWords(line), out Rotation);
				else
					return false;

				return true;
			}
			else
				return true;
		}

		public override void Finish()
		{

		}
	}
}