using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Super_BullWhip
{
    public static class Camera
    {
        static Random rand = new Random();
        private static Vector3 pos;
        private static Vector3 shakePos, realpos;
        public static float Depth = 1000;
        public static float Power = 3;
        public static Obj Target;
        private static Vector3 targpos;
        private static float shakespeed, shakespeedtarget, shakespeedspeed, shakespeedx, shakespeedy, shakeaccx, shakeaccy, shakeposx, shakeposy, shakesize, shakesizetarget, shakesizespeed, movex, movey = 0;
        public static float x
        {
            get { return realpos.X; }
            //set { pos.X = value; }
        }
        public static float y
        {
            get { return realpos.Y; }
            //set { pos.Y = value; }
        }
        public static float z
        {
            get { return realpos.Z; }
            //set { pos.Z = value; }
        }
        public static void init()
        {
            pos = targpos = realpos = shakePos = Vector3.Zero;
            shakeaccy = shakeaccx = 1;
        }
        public static void Update()
        {
            if (Target != null)
            {
                targpos += (Target.pos - targpos) * 0.1f * Global.Speed;
                pos += (targpos - pos) * 0.1f * Global.Speed;
            }
            SetShakeSize(40, 1);
            //SetShakeSpeed(0.03f, 1);
            Shake();
            realpos = pos + shakePos;
        }
        private static void Shake()
        {
            shakespeed += (shakespeedtarget - shakespeed) * shakespeedspeed;
            shakesize += (shakesizetarget - shakesize) * shakesizespeed;
            if (shakespeedx > 1)
                shakeaccx = -1 - ((float)rand.NextDouble() * 2);
            else if (shakespeedx < -1)
                shakeaccx = 1 + ((float)rand.NextDouble() * 2);
            if (shakespeedy > 1)
                shakeaccy = -1 - ((float)rand.NextDouble() * 2);
            else if (shakespeedy < -1)
                shakeaccy = 1 + ((float)rand.NextDouble() * 2);
            shakespeedx += shakespeed * shakeaccx * Global.Speed;
            shakespeedy += shakespeed * shakeaccy * Global.Speed;
            shakeposx += (shakespeed * shakespeedx);
            shakeposy += (shakespeed * shakespeedy);
            if (shakeposx < -1 && movex == -1)
                movex = 1;
            if (shakeposx > 1 && movex == 1)
                movex = 1;
            if (shakeposy < -1 && movey == -1)
                movey = 1;
            if (shakeposy > 1 && movey == 1)
                movey = 1;
            shakeposx += (-shakeposx) * 0.03f * Global.Speed;
            shakeposy += (-shakeposy) * 0.03f * Global.Speed;
            shakePos = new Vector3(shakeposx, shakeposy, 0) * shakesize;
            //Console.WriteLine(shakespeedtarget);
        }
        public static void SetShakeSpeed(float target, float targetspeed)
        {
            shakespeedtarget = target;
            shakespeedspeed = targetspeed;
        }
        public static void SetShakeSize(float target, float targetspeed)
        {
            shakesizetarget = target;
            shakesizespeed = targetspeed;
        }
    }
}
