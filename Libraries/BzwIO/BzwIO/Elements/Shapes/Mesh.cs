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

        public class Face
        {
            public List<int> Vertecies = new List<int>();
            public List<int> Normals = new List<int>();
            public List<int> UVs = new List<int>();

            public string PhysicsDriver = string.Empty;
            public bool SmoothBounce = false;
            public bool NoClusters = false;

            public bool Passable = false;
            public bool ShootThrough = false;
            public bool DriveThrough = false;
        }

        public List<Face> Faces = new List<Face>();

        public class Transformation
        {
            public enum TransformTypes
            {
                Scale,
                Shift,
                Shear,
                Spin,
            }
            public TransformTypes TransformType = TransformTypes.Shift;
            public Vector4F Value = new Vector4F();

            public Transformation() { }

            public Transformation( string type, string data)
            {
                if (type == "SCALE")
                {
                    TransformType = TransformTypes.Scale;
                    Value = new Vector4F(Vector3F.Read(data));
                }
                else if (type == "SHIFT")
                {
                    TransformType = TransformTypes.Shift;
                    Value = new Vector4F(Vector3F.Read(data));
                }
                else if (type == "SHEAR")
                {
                    TransformType = TransformTypes.Shear;
                    Value = new Vector4F(Vector3F.Read(data));
                }
                else if (type == "SPIN")
                {
                    TransformType = TransformTypes.Spin;
                    Value = Vector4F.Read(data);
                }
            }
        }

        public List<Transformation> Transforms = new List<Transformation>();

        public string PhysicsDriver = string.Empty;
        public bool SmoothBounce = false;
        public bool NoClusters = false;

        public Mesh()
        {
            ObjectType = "Mesh";
        }

        public override bool AddCodeLine(string command, string line)
        {
            string nub = Reader.GetRestOfWords(line);

            if (command == "VERTEX")
                Vertecies.Add(Vector3F.Read(nub));
            else if (command == "NORMAL")
                Normals.Add(Vector3F.Read(nub));
            else if (command == "TEXTCOORD")
                UVs.Add(Vector2F.Read(nub));
            else if (command == "INSIDE")
                InsidePoints.Add(Vector3F.Read(nub));
            else if (command == "OUTSIDE")
                OutsidePoints.Add(Vector3F.Read(nub));
            else if (command == "SHIFT" || command == "SPIN" || command == "SCALE" || command == "SHEAR")
                Transforms.Add(new Transformation(command,nub));
            else if (command == "PHYDRV")
                OutsidePoints.Add(Vector3F.Read(nub));
            else if (command == "NOCLOSTERS")
                OutsidePoints.Add(Vector3F.Read(nub));
            else if (command == "SMOOTHBOUNCE")
                OutsidePoints.Add(Vector3F.Read(nub));
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
