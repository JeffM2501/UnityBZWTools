using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BZFlag.IO.Elements;
using BZFlag.IO.Elements.Shapes;

namespace BZFlag.IO
{
	public static class ElementFactory
	{
		private static Dictionary<string, Type> ObjectFactories = new Dictionary<string, Type>();

		private static void AddFactory(string name, Type type)
		{
			name = name.ToUpperInvariant();
			if(ObjectFactories.ContainsKey(name))
				ObjectFactories[name] = type;
			else
				ObjectFactories.Add(name, type);
		}

		static ElementFactory()
		{
			AddFactory("world", typeof(World));
			AddFactory("options", typeof(Options));
			AddFactory("box", typeof(Box));
			AddFactory("pyramid", typeof(Pyramid));
		}
		 
		public static BasicObject Create(string name)
		{
			string key = name.ToUpperInvariant();
			if(ObjectFactories.ContainsKey(key))
				return Activator.CreateInstance(ObjectFactories[key]) as BasicObject;

			return new BasicObject();
		}
	}
}
