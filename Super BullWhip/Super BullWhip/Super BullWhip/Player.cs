using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Super_BullWhip
{
    public class Player:Obj
    {
        
        float gravity = 0.5f;
        Obj Point;
        Obj[] rangePoints;
        float radius = 400;
        float deg = 0;
        public Player(Game1 Doc, float X, float Y, float Z)
            :base(Doc,X,Y,Z,Doc.LoadTex("Character"))
        {
            Global.Player = this;
            origin.Y = tex.Height;
            scale = 0.5f;
            //Point = new Obj(doc
        }
        public override void earlyUpdate()
        {
            base.earlyUpdate();
            ySpeed += gravity;

            //friction slowdown
            if (xSpeed > 0)
            {
                xSpeed -= .5f;
            }
            else if (xSpeed < 0)
            {
                xSpeed += .5f;
            }
            if (y > 0)
            {
                ySpeed = 0;
                y = 0;
            }
            if (doc.controls.getKey(Keys.Space) == Controls.Pressed)
            {
                ySpeed = -20;
            }
            else if (doc.controls.getKey(Keys.Left) == Controls.Held)
            {
                xSpeed = -10;
            }
            else if (doc.controls.getKey(Keys.Right) == Controls.Held)
            {
                xSpeed = 10;
            }
            else if (doc.controls.getKey(Keys.R) == Controls.Held)
            {
                //This is the Whipping which is context based.

                //Search area in front of Player
                

                //Grab nearest point and run code for it.
                float minDist = radius;
                for(int index = 0; index < doc.objList.Count; index++){
                    //check distance    
                    if(doc.objList[index].type != PointType.Normal && MyMath.Distance(pos,doc.objList[index].pos) < minDist)
                    {
                        minDist = MyMath.Distance(pos,doc.objList[index].pos);
                        Point = doc.objList[index];
                    }
                }
                if(Point == null) return;
                if(Point.type == PointType.SwingPoint)
                {
                    swing(Point);
                }
                else if(Point.type == PointType.SlingPoint)
                {
                    sling(Point);
                }
                else if(Point.type == PointType.Pullblock)
                {
                    pull(Point);
                }
            }
        }


        //Swinging from a swing point
        private void swing(Obj obj)
        {
            Console.WriteLine("Swing!");
            //take the distance and swing the player
            float dist = MyMath.Distance(pos, obj.pos);
            float xx = MyMath.LengthDirX(deg, dist);
            float yy = MyMath.LengthDirY(deg, dist);
            x = obj.x + xx;
            y = obj.y + yy;
            deg++;
        }

        //Slingshot
        private void sling(Obj obj)
        {
            //take the distance and sling the player double in the right direction
        }

        //Pull an object
        private void pull(Obj obj)
        {
            //pull the object towards the player one movement.

        }
    }
}
