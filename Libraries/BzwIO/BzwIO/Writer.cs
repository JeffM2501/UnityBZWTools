using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using BZFlag.IO.Elements;

namespace BZFlag.IO
{
	public static class Writer
	{
		public static bool WriteMap(StreamWriter outStream, Map map)
		{
			map.PrepForSave();

			WriteObject(outStream, map.WorldInfo);
			WriteObject(outStream, map.WorldOptions);

			foreach(var o in map.Objects)
				WriteObject(outStream, o);

			return true;
		}

		private static void WriteObject(StreamWriter outStream, BasicObject obj)
		{
			if(obj == null || obj.Code.Count == 0)
				return;

			outStream.WriteLine(obj.ObjectType);
			foreach(var s in obj.Code)
				outStream.WriteLine(s);

			outStream.WriteLine(obj.ObjectTerminator);
			outStream.WriteLine();
		}
	}
}
