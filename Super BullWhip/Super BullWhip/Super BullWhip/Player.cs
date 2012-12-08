using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
namespace Super_BullWhip
{
    public class Player:Obj
    {
        float gravity = 0.5f;
        public Player(Game1 Doc, float X, float Y, float Z)
            :base(Doc,X,Y,Z,Doc.LoadTex("Character"))
        {
            Global.Player = this;
            origin.Y = tex.Height;
            scale = 0.5f;
        }
        public override void earlyUpdate()
        {
            base.earlyUpdate();
            ySpeed += gravity;
            if (y > 0)
            {
                ySpeed = 0;
                y = 0;
            }
            if (doc.controls.getKey(Keys.Z) == Controls.Pressed)
            {
                ySpeed = -20;
            }
        }
    }
}
