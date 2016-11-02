using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Elements.Shapes
{
    public class Zone : PositionableObject
    {
        public List<string> Flags = new List<string>();
        public List<string> ZoneFlags = new List<string>();
        public List<Base.TeamColors> Safe = new List<Base.TeamColors>();
        public List<Base.TeamColors> Team = new List<Base.TeamColors>();

        public Zone()
        {
            ObjectType = "Zone";
        }

        protected IEnumerable<Base.TeamColors> IntListToBaseColors(IEnumerable<int> bases)
        {
            List<Base.TeamColors> teams = new List<Base.TeamColors>();
            foreach (var b in bases)
                teams.Add((Base.TeamColors)b);
            return teams;
        }

        protected IEnumerable<int> BaseColorListToInts(IEnumerable<Base.TeamColors> bases)
        {
            List<int> teams = new List<int>();
            foreach (var b in bases)
                teams.Add((int)b);
            return teams;
        }

        public override bool AddCodeLine(string command, string line)
        {
            string nubs = Reader.GetRestOfWords(line);
            if (!base.AddCodeLine(command, line))
            {
                if (command == "FLAG")
                    Flags.Add(nubs);
                else if (command == "ZONEFLAG")
                    ZoneFlags.Add(nubs);
                else if (command == "SAFE")
                    Safe.AddRange(IntListToBaseColors(Reader.ParseIntVector(nubs)));
                else if (command == "TEAM")
                    Team.AddRange(IntListToBaseColors(Reader.ParseIntVector(nubs)));
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

            foreach( var f in ZoneFlags)
                AddCode(1, "zoneflag", f);

            foreach (var f in Flags)
                AddCode(1, "flag", f);

            if (Safe.Count > 0)
                AddCode(1, "safe", string.Join(" ", BZFlag.IO.Types.Utilities.GetStringList<int>(BaseColorListToInts(Safe))));
            if (Team.Count > 0)
                AddCode(1, "team", string.Join(" ", BZFlag.IO.Types.Utilities.GetStringList<int>(BaseColorListToInts(Team))));

            return name;
        }
    }
}
