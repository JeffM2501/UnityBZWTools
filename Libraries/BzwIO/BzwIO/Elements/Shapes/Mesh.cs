using BZFlag.IO.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Elements.Shapes
{
    public class Mesh : PhaseableObject
    {
        public List<Vector3F> InsidePoints = new List<Vector3F>();
        public List<Vector3F> OutsidePoints = new List<Vector3F>();

        public List<Vector3F> Vertecies = new List<Vector3F>();
        public List<Vector3F> Normals = new List<Vector3F>();
        public List<Vector2F> UVs = new List<Vector2F>();

        public class Transformations
        {
            public enum TransformTypes
            {
                Scale,
                Shift,
                Shear,
                Spin,
            }
            public TransformTypes Transform = TransformTypes.Shift;
            public Vector4F Value = new Vector4F();
        }

        public List<Transformations> Transforms = new List<Transformations>();


        public Mesh()
        {
            ObjectType = "Mesh";
        }

        public override bool AddCodeLine(string command, string line)
        {
            string nub = Reader.GetRestOfWords(line);

            if (command == "VERTEX")
                Vertecies.Add(Vector3F.Read(nub));
            else if (!base.AddCodeLine(command, line))
                return false;

            return true;
        }

        public override string BuildCode()
        {
            string name = base.BuildCode();

           //     AddCode(1, "FlipZ", string.Empty);

            return name;
        }
    }
}
