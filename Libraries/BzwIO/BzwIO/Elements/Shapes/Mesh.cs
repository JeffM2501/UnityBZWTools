using BZFlag.IO.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BZFlag.IO.Types;

namespace BZFlag.IO.Elements.Shapes
{
    public class Mesh : PositionableObject
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

        private Face TempFace = null;

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
            else if (command == "FACE")
            {
                if (TempFace != null)
                    Faces.Add(TempFace);

                TempFace = new Face();
            }
            else if (command == "ENDFACE")
            {
                if (TempFace != null)
                    Faces.Add(TempFace);

                TempFace = null;
            }
            else if (command == "PHYDRV")
            {
                if (TempFace != null)
                    TempFace.PhysicsDriver = nub;
                else
                    PhysicsDriver = nub;
            }
            else if (command == "NOCLOSTERS")
            {
                if (TempFace != null)
                    TempFace.NoClusters = true;
                else
                    NoClusters = true;
            }
            else if (command == "SMOOTHBOUNCE")
            {
                if (TempFace != null)
                    TempFace.SmoothBounce = true;
                else
                    SmoothBounce = true;
            }
            else if (command == "PASSABLE")
            {
                if (TempFace != null)
                    TempFace.Passable = true;
            }
            else if (command == "DRIVETHROUGH")
            {
                if (TempFace != null)
                    TempFace.DriveThrough = true;
            }
            else if (command == "SHOOTTHROUGH")
            {
                if (TempFace != null)
                    TempFace.ShootThrough = true;
            }
            else if (!base.AddCodeLine(command, line))
                return false;

            return true;
        }

        public override void Finish()
        {
            if (TempFace != null)
                Faces.Add(TempFace);

            TempFace.ToString();
            base.Finish();
        }

        public override string BuildCode()
        {
            string name = base.BuildCode();

            foreach (var p in InsidePoints)
                AddCode(1, "inside", p);

            foreach (var p in OutsidePoints)
                AddCode(1, "outside", p);

            foreach (var p in Vertecies)
                AddCode(1, "vertex", p);
            foreach (var p in Normals)
                AddCode(1, "normal", p);
            foreach (var p in UVs)
                AddCode(1, "texcoord", p);

            foreach (var xform in Transforms)
            {
                switch (xform.TransformType)
                {
                    case Transformation.TransformTypes.Scale:
                        AddCode(1, "scale", new Vector3F(xform.Value));
                        break;
                    case Transformation.TransformTypes.Shear:
                        AddCode(1, "shear", new Vector3F(xform.Value));
                        break;
                    case Transformation.TransformTypes.Shift:
                        AddCode(1, "shift", new Vector3F(xform.Value));
                        break;
                    case Transformation.TransformTypes.Spin:
                        AddCode(1, "spin", xform.Value);
                        break;
                }
            }

            if (PhysicsDriver != string.Empty)
                AddCode(1, "phydrv", PhysicsDriver);

            if (NoClusters)
                AddCode(1, "noclusters", string.Empty);

            if (SmoothBounce)
                AddCode(1, "smoothbounce", string.Empty);

            foreach(var face in Faces)
            {
                AddCode(1, "face", string.Empty);

                AddCode(2, "vertices", string.Join(" ",Utilities.GetStringList<int>(face.Vertecies)));
                AddCode(2, "normals", string.Join(" ", Utilities.GetStringList<int>(face.Normals)));
                AddCode(2, "texcoords", string.Join(" ", Utilities.GetStringList<int>(face.UVs)));

                if (face.PhysicsDriver != string.Empty)
                    AddCode(2, "phydrv", PhysicsDriver);

                if (face.NoClusters)
                    AddCode(2, "noclusters", string.Empty);

                if (face.SmoothBounce)
                    AddCode(2, "smoothbounce", string.Empty);

                if (face.Passable)
                    AddCode(2, "passable", string.Empty);
                else
                {
                    if (face.ShootThrough)
                        AddCode(2, "shootthrough", string.Empty);
                    if (face.DriveThrough)
                        AddCode(2, "drivethrough", string.Empty);
                }

                AddCode(1, "endface", string.Empty);
            }

           return name;
        }
    }
}
