using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace Super_BullWhip
{
    public class Wall : Obj
    {
        public Wall(Game1 Doc, float X, float Y, float Z, int width, int height)
            : base(Doc, X, Y, Z, Doc.LoadTex("Platform"))
        {
            scale = 1f;
            isFloor = true;
            /*body = BodyFactory.CreateRectangle(doc.getWorld(), ConvertUnits.ToSimUnits(tex.Width), ConvertUnits.ToSimUnits(tex.Height), 1f, new Vector2( ConvertUnits.ToSimUnits(x),  ConvertUnits.ToSimUnits(y)));
            body.IsStatic = true;
            body.Restitution = 0.2f;
            if (isFloor)
            {
                body.Friction = 0.5f;
            }
            body.FixedRotation = true;*/
            createRecBody(1,0, 0.5f, false, true);
        }
    }
}
