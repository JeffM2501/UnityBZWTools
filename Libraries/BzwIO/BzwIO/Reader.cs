using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using BZFlag.IO.Elements;

namespace BZFlag.IO
{
    public static class Reader
    {
		internal static string TrimTrainingComments(string text)
		{
			if (text.Contains("#"))
				text = text.Substring(0, text.IndexOf('#')).Trim();
			return text;
		}

		internal static string GetFirstWord(string text)
		{
			string tmp = TrimTrainingComments(text);
			if(tmp.Contains(" "))
				tmp = text.Substring(0, text.IndexOf(' ')).Trim();
			return tmp;
		}

		internal static string GetRestOfWords(string text)
		{
			string tmp = TrimTrainingComments(text);
			if(tmp.Contains(" "))
				tmp = text.Substring(text.IndexOf(' ')).Trim();
			return tmp;
		}

		internal static List<float> ParseFloatVector(string line)
		{
			List<float> vec = new List<float>();
			foreach(string s in line.Split(" ".ToCharArray()))
			{
				if(s == string.Empty || Char.IsWhiteSpace(s[0]))
					continue;

				float d = 0;
				float.TryParse(s, out d);
				vec.Add(d);
			}
			return vec;
		}

		public static Map ReadMap(StreamReader inStream)
		{
			Map map = new Map();
			map.IntForLoad();

			BasicObject obj = null;
			while (!inStream.EndOfStream)
			{
				string line = inStream.ReadLine().Trim();

				if (line == string.Empty || line[0] == '#')
					continue;

                string cmd_norm = GetFirstWord(line);

                string cmd = cmd_norm.ToUpperInvariant();

				if (obj == null)
				{
					obj = ElementFactory.Create(cmd);
                    obj.Init(cmd_norm, GetRestOfWords(TrimTrainingComments(line)));
				}
				else
				{
					if (cmd == "END")
					{
						obj.Finish();
						map.AddObject(obj);
						obj = null;
					}
					else
						obj.AddCodeLine(cmd,TrimTrainingComments(line));
				}

			}

			if (obj != null)	// should not happen, but don't loose data
			{
				obj.Finish();
				map.AddObject(obj);
				obj = null;
			}

			return map;
		}
    }
}
