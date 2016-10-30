using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Elements
{
	public class BasicObject
	{
		public virtual string ObjectTerminator {  get { return "end"; } }

		public string ObjectType = string.Empty;
		public List<string> Code = new List<string>();

		public string Name = string.Empty;

		public string GUID = string.Empty;


		private static Random RNG = new Random();

		public BasicObject()
		{
			ObjectType = "Unknown";

			GUID = RNG.Next().ToString() + RNG.Next().ToString() + RNG.Next().ToString();
		}

		public virtual bool AddCodeLine(string command, string line)
		{
			Code.Add(line);

			if(command == "NAME")
				Name = Reader.GetRestOfWords(line);
			else
				return false;

			return true;
		}

		public virtual void Finish()
		{

		}

		public virtual void BuildCode()
		{
			string[] code = Code.ToArray();
			Code.Clear();

			foreach(string c in code)
				AddCode(1, c, string.Empty);
		}

		protected StringBuilder GetIndent(int indent)
		{
			StringBuilder sb = new StringBuilder();
			for(int i = 0; i < indent; i++)
				sb.Append("\t");

			return sb;
		}

		public void AddCode(int indent, string name, string value)
		{
			StringBuilder sb = GetIndent(indent);
			sb.Append(name);
			if (value != string.Empty)
			{
				sb.Append(" ");
				sb.Append(value);
			}

			Code.Add(sb.ToString());
		}

		public void AddCode(int indent, string name, float value)
		{
			AddCode(indent,name,value.ToString());
		}

		public void AddCode(int indent, string name, bool value)
		{
			AddCode(indent, name, value ? "1" : "0");
		}

		public void AddCode(int indent, string name, float[] values)
		{
			StringBuilder sb = new StringBuilder();
			for(int i = 0; i < values.Length; i++)
			{
				sb.Append(values[i].ToString());
				if(i != values.Length - 1)
					sb.Append(" ");
			}

			AddCode(indent, name, sb.ToString());
		}
	}
}
