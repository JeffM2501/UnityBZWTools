using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Elements.Shapes
{
	public class Pyramid : PositionableObject
	{
		public bool TopDown = false;
		public override void Finish()
		{
			base.Finish();

			if (Size[2] < 0)
			{
				TopDown = true;
				Size[2] = (float)Math.Abs(Size[2]);
			}
		}

		public override void BuildCode()
		{
			if (TopDown)
			{
				Size[2] = Size[2] * -1;
			}
			base.BuildCode();

			if(TopDown)
			{
				Size[2] = Size[2] * -1;
			}
		}
	}
}
