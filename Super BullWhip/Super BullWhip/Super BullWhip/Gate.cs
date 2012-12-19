using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace Super_BullWhip
{
    public class Gate : Obj
    {
        // Open/close state of the gate. Default closed, permanently open when opened
        bool open = false;


        public Gate(Game1 Doc, float X, float Y, float Z)
            : base(Doc, X, Y, Z, Doc.LoadTex("Gate"))
        {
            scale = 1f;
            isFloor = false;
            createRecBody(1, 0, 0.5f, true, true);
        }


        public void openGate(){
            open = true;
            this.alpha = 0.25f;
            doc.getWorld().RemoveBody(body);
            body = null;
        }

    }
}
