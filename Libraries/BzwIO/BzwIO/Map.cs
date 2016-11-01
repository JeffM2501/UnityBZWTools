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

        public void FinishLoad()
        {
            // check to see if all the teleporters have names and if not fix any links

            foreach(BasicObject obj in Objects)
            {
                Teleporter tp = obj as Teleporter;
                if (tp == null)
                    continue;

                if (tp.Name == string.Empty)
                    tp.Name = tp.Index.ToString();
            }
        }

        private BasicObject FindObjectByName(string name)
        {
            return Objects.Find((x) => x.Name.ToUpperInvariant() == name.ToUpperInvariant());
        }

        private Teleporter FindTeleporterByName(string name)
        {
            return Objects.Find((x) => x as Teleporter != null && x.Name.ToUpperInvariant() == name.ToUpperInvariant()) as Teleporter;
        }

        private Teleporter FindTeleporterFaceIndex(int index)
        {
            int porterID = (index / 2) + 1;

            return Objects.Find((x) => x as Teleporter != null && (x as Teleporter).Index == porterID) as Teleporter;
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
	}
}
