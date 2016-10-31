using System;
using System.Collections.Generic;

using BZFlag.IO.Elements;
using BZFlag.IO.Elements.Shapes;

namespace BZFlag.IO
{
	public class Map
	{
		public List<BasicObject> Objects = new List<BasicObject>();

		public World WorldInfo = new World();
		public Options WorldOptions = new Options();

		public void IntForLoad()
		{
			Teleporter.TeleporterCount = 0;
		}

		public void AddObject(BasicObject obj)
		{

			if(obj as World != null)
				WorldInfo = obj as World;
			else if(obj as Options != null)
				WorldOptions = obj as Options;
			else
				Objects.Add(obj);

		}

		public void PrepForSave()
		{
			WorldInfo.BuildCode();
			WorldOptions.BuildCode();

			foreach(var o in Objects)
				o.BuildCode();
		}
	}
}
