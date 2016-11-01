using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using BZFlag.IO;

namespace ReaderTest
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Need a bzw filename");
				return;
			}

			FileInfo file = new FileInfo(args[0]);
			StreamReader sr = file.OpenText();

			var map = Reader.ReadMap(sr);
			sr.Close();

			FileInfo newFile = new FileInfo(file.FullName + "_Parsed");

			if(newFile.Exists)
				newFile.Delete();

			Stream s = newFile.OpenWrite();
			StreamWriter sw = new StreamWriter(s);

			Writer.WriteMap(sw, map);
			sw.Close();
			s.Close();
		}

	}
}
