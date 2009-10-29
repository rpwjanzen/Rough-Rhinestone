using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particles {
    struct PVector2 {
        public static readonly PVector2 Zero = new PVector2(FInt.ZeroF, FInt.ZeroF);
        public static readonly PVector2 Up = new PVector2(FInt.ZeroF, FInt.OneF);
        public static readonly PVector2 Down = new PVector2(FInt.ZeroF, FInt.NOneF);
        public static readonly PVector2 Left = new PVector2(FInt.NOneF, FInt.ZeroF);
        public static readonly PVector2 Right = new PVector2(FInt.OneF, FInt.ZeroF);

        public FInt X;
        public FInt Y;

        static FInt scratch1 = FInt.ZeroF;
        static FInt scratch2 = FInt.ZeroF;

        public PVector2(FInt x, FInt y) {
            X = x;
            Y = y;
        }

        public FInt LengthSquared() {
            FInt.Multiply(ref X, ref X, out scratch1);
            FInt.Multiply(ref Y, ref Y, out scratch2);
            FInt.Add(ref scratch1, ref scratch2, out scratch1);
            return scratch1;
        }

        public FInt LengthSquared(out FInt i) {
            FInt.Multiply(ref X, ref X, out scratch1);
            FInt.Multiply(ref Y, ref Y, out scratch2);
            FInt.Add(ref scratch1, ref scratch2, out i);
            return i;
        }

        public static PVector2 operator +(PVector2 a, PVector2 b) {
            return new PVector2(a.X + b.X, a.Y + b.Y);
        }

        public static void Add(ref PVector2 a, ref PVector2 b, out PVector2 c) {
            FInt.Add(ref a.X, ref b.X, out c.X);
            FInt.Add(ref a.Y, ref b.Y, out c.Y);
        }

        public static PVector2 operator -(PVector2 a, PVector2 b) {
            return new PVector2(a.X - b.X, a.Y - b.Y);
        }

        public static void Sub(ref PVector2 a, ref PVector2 b, out PVector2 dest) {
            FInt.Sub(ref a.X, ref b.X, out dest.X);
            FInt.Sub(ref a.Y, ref b.Y, out dest.Y);
        }

        public static PVector2 operator *(PVector2 a, FInt i) {
            return new PVector2(a.X * i, a.Y * i);
        }

        public static void Multiply(ref PVector2 v, ref FInt i, out PVector2 o) {
            FInt.Multiply(ref v.X, ref i, out o.X);
            FInt.Multiply(ref v.Y, ref i, out o.Y);
        }

        public static PVector2 operator /(PVector2 a, FInt i) {
            return new PVector2(a.X / i, a.Y / i);
        }

        public static bool operator ==(PVector2 a, PVector2 b) {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(PVector2 a, PVector2 b) {
            return a.X != b.X || a.Y != b.Y;
        }

        public override bool Equals(object obj) {
            return (obj is PVector2 && this == (PVector2)obj);
        }

        public override int GetHashCode() {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public Microsoft.Xna.Framework.Vector2 ToVector2() {
            return new Microsoft.Xna.Framework.Vector2((float)X.ToDouble(), (float)Y.ToDouble());
        }
    }
}
