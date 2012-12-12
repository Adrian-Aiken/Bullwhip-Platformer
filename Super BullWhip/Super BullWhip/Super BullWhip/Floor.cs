using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Super_BullWhip
{
    class Floor:Obj
    {
        public Floor(Game1 Doc, float X, float Y, Texture2D Tex)
            : base(Doc, X, Y, 0, Tex)
        {

        }
    }
}
