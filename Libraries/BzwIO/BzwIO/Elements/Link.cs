using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Elements
{
	public class Link : BasicObject
	{
        public class PorterLink
        {
            public string Code = string.Empty;

            public bool Wildcard = false;

            public string TargetGroup = string.Empty;
            public string TargetName = string.Empty;
            public bool Front = false;

            protected bool IsWildcardCharacter(char c)
            {
                return c == '*' || c == '?';
            }

			protected bool IsWildcardCharacter(string str)
			{
				return str != string.Empty && IsWildcardCharacter(str[0]);
			}

			public void Import(string code)
            {
                Code = code;

                if (code == string.Empty)
                {
                    Wildcard = true;
                    return;
                }

                Wildcard = false;

                if (code.Contains(':'))
                {
                    string[] parts = code.Split(":".ToCharArray());
                    if (parts.Length == 2)
                    {
                        TargetName = parts[0];
                        if (IsWildcardCharacter(parts[1]))
                        {
                            TargetGroup = TargetName;
                            TargetName = string.Empty;
                            Wildcard = true;
                        }
                        else
                            Front = parts[1].ToLowerInvariant() != "b";
                    }
                    else if (parts.Length > 2)
                    {
                        // groups and shit!
                        TargetGroup = parts[0];
                        TargetName = parts[1];
                        if (IsWildcardCharacter(parts[2]))
                            Wildcard = true;
                        else
                            Front = parts[1].ToLowerInvariant() != "b";
                    }
                }
                else if (IsWildcardCharacter(code.Trim()[0]))
                    Wildcard = true;
                else
                {
                    int faceID = 0;
                    int.TryParse(code, out faceID);

                    TargetName = (faceID / 2).ToString();
                    Front = faceID % 2 == 1;
                }
            }

            public string GetCode()
            {
                Code = string.Empty;

                if (TargetGroup != string.Empty)
                    Code += TargetGroup + ":";
                if (TargetName != string.Empty)
                    Code += TargetName + ":";

                if (Wildcard)
                    Code += "*";
                else
                    Code += Front ? "f" : "b";

                return Code;
            }
        }

        public PorterLink From = new PorterLink();
        public PorterLink To = new PorterLink();

        public List<string> Attributes = new List<string>();

        public Link()
		{
			ObjectType = "Link";
		}

		public override bool AddCodeLine(string command, string line)
		{
			if (!base.AddCodeLine(command, line))
			{
                if (command == "FROM")
                    From.Import(Reader.GetRestOfWords(line));
                else if (command == "TO")
                    To.Import(Reader.GetRestOfWords(line));
                else
                    Attributes.Add(line);
			}

			return true;
		}

		public override string BuildCode()
		{
			Code.Clear();

			AddCode(1, "Name", Name);
            AddCode(1, "From", From.GetCode());
            AddCode(1, "To", To.GetCode());
            foreach (var s in Attributes)
				AddCode(2, s, string.Empty);

			return ObjectType;
		}
	}
}
