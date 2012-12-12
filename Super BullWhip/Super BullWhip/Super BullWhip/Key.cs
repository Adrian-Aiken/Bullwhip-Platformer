using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Super_BullWhip
{
    public class Key : Obj
    {
        // List of gates controlled by this key
        private List<Gate> gates;
        // Color of key/gate
        private Color color;


        public Key(Game1 Doc, float X, float Y, float Z, Color color)
            : base(Doc, X, Y, Z, Doc.LoadTex("Key"))
        {
            this.color = color;
            gates = new List<Gate>();
        }


        public void addGate(Gate gate)
        {
            gates.Add(gate);
        }


        public override void Update()
        {
            if ( (Math.Abs(doc.player.x - x) < 50) && (Math.Abs(doc.player.y - y) < 50) )
            {
                foreach (Gate g in gates)
                    g.openGate();

                doc.RemoveObj(this);
            }
        }

    }
}
