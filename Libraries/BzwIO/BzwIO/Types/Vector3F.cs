using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Types
{
    public class Vector3F : IEnumerable<float>
    {
        protected float[] Pos = new float[] { 0, 0, 0 };

        public float X
        {
            get { return Pos[0]; }
            set { Pos[0] = value; }
        }

        public float Y
        {
            get { return Pos[1]; }
            set { Pos[1] = value; }
        }

        public float Z
        {
            get { return Pos[2]; }
            set { Pos[2] = value; }
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

        public Vector3F() { }

        public Vector3F(float x, float y, float z)
        {
            Pos[0] = x;
            Pos[1] = y;
            Pos[2] = z;
        }

        public Vector3F(float[] v, int start = 0)
        {
            Pos[0] = v[start];
            Pos[1] = v[start+1];
            Pos[2] = v[start+2];
        }

        public Vector3F(Vector2F v)
        {
            Pos[0] = v.X;
            Pos[1] = v.Y;
        }

        public Vector3F(Vector4F v)
        {
            Pos[0] = v.X;
            Pos[1] = v.Y;
            Pos[2] = v.Z;
        }

        public static Vector3F Read(string line)
        {
            Vector3F v = new Vector3F();

            var vec = Reader.ParseFloatVector(line);
            for (int i = 0; i < 3 && i < vec.Count; i++)
                v[i] = vec[i];

            return v;
        }
    }
}
