using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;

namespace Super_BullWhip
{
    public class Player:Obj
    {
        
        float gravity = 0.5f;
        Obj Point;
        float radius = 400;
        float deg = 0;
        int increment = 3;
        CircleShape c;
        public Player(Game1 Doc, float X, float Y, float Z)
            :base(Doc,X,Y,Z,Doc.LoadTex("Character"))
        {
            Global.Player = this;
            origin.Y = tex.Height;
            scale = 0.5f;
            c = new CircleShape(100, 1);
            //Point = new Obj(doc
        }
        public override void earlyUpdate()
        {
            base.earlyUpdate();
            ySpeed += gravity;

            //friction slowdown
            if (xSpeed > 0.5)
            {
                xSpeed -= .5f;
            }
            else if (xSpeed < -0.5)
            {
                xSpeed += .5f;
            }
            else
            {
                xSpeed = 0;
            }

            //Landing
            if (y > 0)
            {
                ySpeed = 0;
                y = 0;
            }
            if (Controls.GetKey(Keys.Space) == Controls.Pressed)
            {
                ySpeed = -20;
            }
            else if (Controls.GetKey(Keys.Left) == Controls.Held)
            {
                xSpeed = -10;
            }
            else if (Controls.GetKey(Keys.Right) == Controls.Held)
            {
                xSpeed = 10;
            }
            else if (Controls.GetKey(Keys.R) == Controls.Held)
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
                if (Point == null)
                {
                    //deg = 0;
                    return;
                }
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

                Point = null;
            }
        }


        //Swinging from a swing point
        private void swing(Obj obj)
        {
            Console.WriteLine("Swing!");
            //take the distance and swing the player
            float velocity; //speed of travel
            float dir; //direction of travel
            float tensionAll;
            float tensionX;
            float tensionY;
            Vector3 nextPoint;
            float dist = MyMath.Distance(pos, obj.pos);
            deg = MyMath.angleBetween(pos, obj.pos) + increment;
            Console.WriteLine("deg = " + deg);

            //only start swinging once in swingable range
            if (deg > 0)
            {
                //Console.WriteLine("increment: " + increment);
                if (((deg < 20) && (increment > 0)) || ((deg > 160) && (increment < 0)))
                {
                    increment *= -1;
                }

                //get the next point in sequence
                nextPoint.X = MyMath.LengthDirX(deg, dist) + obj.x;
                nextPoint.Y = MyMath.LengthDirY(deg, dist) + obj.y;
                nextPoint.Z = z;

                //get the velocity and direction of velocity between the two points.
                velocity = MyMath.Distance(pos, nextPoint);
                dir = deg = MyMath.angleBetween(pos, nextPoint);


                xSpeed = MyMath.LengthDirX(dir, velocity);
                ySpeed = MyMath.LengthDirY(dir, velocity);

                
                //tension
                //First, find the amount of movement away from the swing point
                tensionAll = velocity * (float)Math.Sin(MathHelper.ToRadians(dir - (deg - increment)));
                Console.WriteLine("Tension: " + tensionAll);

                tensionX = MyMath.LengthDirX(deg, tensionAll);
                tensionY = MyMath.LengthDirY(deg, tensionAll);

                xSpeed -= tensionX; // *((dist / radius) + .2f);
                ySpeed -= tensionY; // *((dist / radius) + .2f);
                 
                //Console.WriteLine("dist tension: " + (dist / radius));
                

            }



            //float xx = MyMath.LengthDirX(deg, dist);
            //float yy = MyMath.LengthDirY(deg, dist);
            //xSpeed = -(xx * 0.01f);
            //ySpeed = -(yy * 0.01f);
            //deg++;
        }

        //Slingshot
        private void sling(Obj obj)
        {
            Console.WriteLine("Sling!");
            //take the distance and sling the player double in the right direction
            float dist = MyMath.Distance(pos, obj.pos);
            float angle; //the angle between the player and point

            angle = MyMath.angleBetween(pos, obj.pos);

            xSpeed -= MyMath.LengthDirX(angle, dist) * .025f;
            ySpeed -= MyMath.LengthDirY(angle, dist) * .025f;

        }

        //Pull an object
        private void pull(Obj obj)
        {
            Console.WriteLine("Pull!");
            //pull the object towards the player one movement.
            float dist = MyMath.Distance(pos, obj.pos);
            float angle = MyMath.angleBetween(obj.pos, pos);

            obj.xSpeed -= MyMath.LengthDirX(angle, dist) * .01f;
            //obj.ySpeed -= MyMath.LengthDirY(angle, dist) * .01f;
        }
    }
}
