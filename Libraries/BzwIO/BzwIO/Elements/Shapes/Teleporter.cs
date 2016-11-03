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

        public bool Horizontal = false;
        public bool Ricochet = false;

		public Teleporter()
		{
			ObjectType = "Teleporter";

			TeleporterCount++;
			Index = TeleporterCount;
		}

        public override void Init(string objectType, string typeParams)
        {
            ObjectType = objectType;

            if (typeParams != string.Empty)
                Name = typeParams;
        }

        public override bool AddCodeLine(string command, string line)
		{
            if (command == "BORDER")
                float.TryParse(Reader.GetRestOfWords(line), out Border);
            else if (command == "NAME" && Name != string.Empty)
                Name = Reader.GetRestOfWords(line);
            else if (command == "HORIZONTAL")
                Horizontal = true;
            else if (command == "RICOCHET")
                Ricochet = true;
            else if (!base.AddCodeLine(command, line))
                return false;

			return true;
		}

		public override string BuildCode()
		{
            Code.Clear();
    
            AddCode(1, "position", Position);
            AddCode(1, "rotation", Rotation);
            AddCode(1, "size", Size);

            AddCode(1, "border", Border);
            if (Horizontal)
                AddCode(1, "horizontal");
            if (Ricochet)
                AddCode(1, "ricochet");

            string t = "teleporter";
            if (Name != string.Empty)
                t += " " + Name;

			return t;
		}
	}
}
