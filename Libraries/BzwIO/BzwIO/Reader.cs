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
				text = text.Substring(0, text.IndexOf('#') - 1).Trim();
			return text;
		}

		internal static string GetFirstWord(string text)
		{
			string tmp = TrimTrainingComments(text);
			if(tmp.Contains(" "))
				tmp = text.Substring(0, text.IndexOf(' ') - 1).Trim();
			return tmp;
		}

		internal static string GetRestOfWords(string text)
		{
			string tmp = TrimTrainingComments(text);
			if(tmp.Contains(" "))
				tmp = text.Substring(text.IndexOf(' ')).Trim();
			return tmp;
		}

		internal static List<double> ParseDoubleVector(string line)
		{
			List<double> vec = new List<double>();
			foreach(string s in line.Split(" ".ToCharArray()))
			{
				double d = 0;
				double.TryParse(s, out d);
				vec.Add(d);
			}
			return vec;
		}

		public static Map ReadMap(StreamReader inStream)
		{
			Map map = new Map();

			BasicObject obj = null;
			while (!inStream.EndOfStream)
			{
				string line = inStream.ReadLine().Trim();

				if (line == string.Empty || line[0] == '#')
					continue;

				if (obj == null)
				{
					obj = new BasicObject();
					obj.ObjectType = TrimTrainingComments(line);
				}
				else
				{
					string cmd = GetFirstWord(line).ToUpperInvariant();
					if (cmd == "END")
					{
						obj.Finish();
						map.Objects.Add(obj);
						obj = null;
					}
					else
						obj.AddCodeLine(cmd,TrimTrainingComments(line));
				}

			}

			if (obj != null)	// should not happen, but don't loose data
			{
				obj.Finish();
				map.Objects.Add(obj);
				obj = null;
			}

			return map;
		}
    }
}
