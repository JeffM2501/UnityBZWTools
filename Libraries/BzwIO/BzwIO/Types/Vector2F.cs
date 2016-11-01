using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZFlag.IO.Types
{
    public class Vector2F : IEnumerable<float>
    {
        protected float[] Pos = new float[] { 0, 0};

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

        public static Vector2F ReadVector3(string line)
        {
            Vector2F v = new Vector2F();

            var vec = Reader.ParseFloatVector(line);
            for (int i = 0; i < 2 && i < vec.Count; i++)
                v[i] = vec[i];

            return v;
        }
    }
}