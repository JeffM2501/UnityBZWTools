using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BZFlag.IO.Types;

namespace BZFlag.IO.Elements
{
    public class Physics : BasicObject
    {
        public Vector3F Linear = new Vector3F();
        public Vector3F Angular = new Vector3F();
        public float Slide = 0;
        public string Death = string.Empty;

        public List<string> Attributes = new List<string>();

        public Physics()
        {
            ObjectType = "Physics";
        }

        public override bool AddCodeLine(string command, string line)
        {
            if (!base.AddCodeLine(command, line))
            {
                if (command == "LINEAR")
                    Linear = Vector3F.Read(Reader.GetRestOfWords(line));
                else if (command == "ANGULAR")
                    Angular = Vector3F.Read(Reader.GetRestOfWords(line));
                else if (command == "SLIDE")
                    float.TryParse(Reader.GetRestOfWords(line), out Slide);
                else if (command == "DEATH")
                    Death = Reader.GetRestOfWords(line);
                else
                    Attributes.Add(line);
            }

            return true;
        }

        public override string BuildCode()
        {
            Code.Clear();

            if (!Linear.IsZero())
                AddCode(1, "linear", Linear);

            if (!Angular.IsZero())
                AddCode(1, "angular", Angular);

            if (Slide > 0)
                AddCode(1, "slide", Slide);

            if (Death != string.Empty)
                AddCode(1, "death", Death);

            foreach (var s in Attributes)
                AddCode(2, s);

            return ObjectType;
        }
    }
}
