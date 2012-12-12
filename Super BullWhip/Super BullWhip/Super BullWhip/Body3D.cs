using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace Super_BullWhip
{
    public class Body3D : Obj
    {
        Body baseBody;

        public Body3D (Game1 doc, float X, float Y, float Z, Texture2D texture, World world, float width, float height, float density, float depth)
            : base(doc, X, Y, Z, texture)
        {
            baseBody = BodyFactory.CreateRectangle(world, width, height, density);
            this.depth = depth;
        }
    }
}
