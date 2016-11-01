using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Types
{
    public class Vector4F : IEnumerable<float>
    {
        protected float[] Pos = new float[] { 0, 0, 0, 0 };

        public float A
        {
            get { return Pos[0]; }
            set { Pos[0] = value; }
        }

        public float X
        {
            get { return Pos[1]; }
            set { Pos[1] = value; }
        }

        public float Y
        {
            get { return Pos[2]; }
            set { Pos[2] = value; }
        }

        public float Z
        {
            get { return Pos[3]; }
            set { Pos[3] = value; }
        }

        public float this[int key]
        {
            get
            {
                return Pos[key];
            }
            set
            {
                Pos[key] = value;
            }
        }

        public IEnumerator<float> GetEnumerator()
        {
            return ((IEnumerable<float>)Pos).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<float>)Pos).GetEnumerator();
        }


        public Vector4F() { }

        public Vector4F(float x, float y, float z)
        {
            Pos[0] = 0;
            Pos[1] = x;
            Pos[2] = y;
            Pos[3] = z;
        }

        public Vector4F(float a, float x, float y, float z)
        {
            Pos[0] = a;
            Pos[1] = x;
            Pos[2] = y;
            Pos[3] = z;
        }

        public Vector4F(float[] v, int start = 0)
        {
            Pos[0] = v[start];
            Pos[1] = v[start + 1];
            Pos[2] = v[start + 2];
            Pos[3] = v[start + 3];
        }

        public Vector4F(Vector2F v)
        {
            Pos[1] = v.X;
            Pos[2] = v.Y;
        }

        public Vector4F(Vector3F v)
        {
            Pos[1] = v.X;
            Pos[2] = v.Y;
            Pos[3] = v.Z;
        }

        public Vector4F(Vector3F axis, float angle)
        {
            Pos[0] = angle;
            Pos[1] = axis.X;
            Pos[2] = axis.Y;
            Pos[3] = axis.Z;
        }

        public static Vector4F Read(string line)
        {
            Vector3F v = new Vector3F();

            var vec = Reader.ParseFloatVector(line);
            for (int i = 0; i < 4 && i < vec.Count; i++)
                v[i] = vec[i];

            return v;
        }
    }
}
