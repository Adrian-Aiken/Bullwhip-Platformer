using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Super_BullWhip
{
    public static class MyMath
    {
        public static float Distance(Vector3 p1, Vector3 p2)
        {
            return (float)Math.Sqrt(((p1.X - p2.X) * (p1.X - p2.X)) + ((p1.Y - p2.Y) * (p1.Y - p2.Y)) + ((p1.Z - p2.Z) * (p1.Z - p2.Z)));
        }
        public static float AngleDistance(float ang1, float ang2)
		{
			float test1 = ang1 - ang2;
			float test2 = ang1 - (ang2 - 360);
            float test3 = (ang1 - 360) - ang2;
			//trace("Angles: " + test1 + ", " + test2 + ", " + test3);
			if (Math.Abs(test1) < Math.Abs(test2) && Math.Abs(test1) < Math.Abs(test3))
			return test1;
			else if (Math.Abs(test2) < Math.Abs(test1) && Math.Abs(test2) < Math.Abs(test3))
			return test2;
			else if (Math.Abs(test3) < Math.Abs(test1) && Math.Abs(test3) < Math.Abs(test2))
			return test3;
			else return test1;
		}
        public static float angleBetween(Vector3 v1, Vector3 v2)
		{
			//float ang1, ang2;
			//ang1 = (float)Math.Atan2(v1.Y, v1.X);
            //ang2 = (float)Math.Atan2(v2.Y, v2.X);
            return MathHelper.ToDegrees((float)Math.Atan2(v1.Y - v2.Y, v1.X - v2.X));
			//return (ang2 - ang1)/DEGRAD;
		}
        public static float LengthDirX(float dir, float dist)
        {
            return (float)Math.Sin(MathHelper.ToRadians(dir)) * dist;
        }
        public static float LengthDirY(float dir, float dist)
        {
            return (float)Math.Cos(MathHelper.ToRadians(dir)) * dist;
        }
    }
}
