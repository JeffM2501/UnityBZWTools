using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Elements.Shapes
{
	public class PhaseableObject : PositionableObject
    {
        public bool Passable = false;
        public bool ShootThrough = false;
        public bool DriveThrough = false;
        public bool Ricochet = false;

		public PhaseableObject()
		{
			ObjectType = "Unknown";
		}

		public override bool AddCodeLine(string command, string line)
		{
			if(!base.AddCodeLine(command, line))
			{
                if (command == "PASSABLE")
                    Passable = true;
                else if (command == "DRIVETHROUGH")
                    DriveThrough = true;
                else if (command == "SHOOTTHROUGH")
                    ShootThrough = true;
                else if (command == "RICOCHET")
                    Ricochet = true;
                else
                    Attributes.Add(line);
			}

			return true;
		}

        public override void Finish()
        {

        }

		public override string BuildCode()
		{
            string name = base.BuildCode();

            if (Passable)
                AddCode(1, "passable", string.Empty);
            else
            {
                if (DriveThrough)
                    AddCode(1, "drivethrough", string.Empty);

                if (ShootThrough)
                    AddCode(1, "shootthrough", string.Empty);
            }

            if (Ricochet)
                AddCode(1, "ricochet", string.Empty);

            return name;
		}
	}
}
