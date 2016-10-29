using System;
using System.Collections.Generic;

using BZFlag.IO.Elements;

namespace BZFlag.IO
{
	public class Map
	{
		public List<BasicObject> Objects = new List<BasicObject>();

		public World WorldInfo = new World();
		public Options WorldOptions = new Options();

		public void AddObject(BasicObject obj)
		{

			if(obj as World != null)
				WorldInfo = obj as World;
			else if(obj as Options != null)
				WorldOptions = obj as Options;
			else
				Objects.Add(obj);

		}

	}
}
