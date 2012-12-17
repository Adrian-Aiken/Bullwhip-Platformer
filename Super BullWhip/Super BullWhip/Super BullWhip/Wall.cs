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
        public Wall(Game1 Doc, float X, float Y, float Z, int width, int height, bool isFloor)
            : base(Doc, X, Y, Z, Doc.LoadTex("Platform"))
        {
            scale = 0.5f;
            body = BodyFactory.CreateRectangle(doc.getWorld(), ConvertUnits.ToSimUnits(width), ConvertUnits.ToSimUnits(height), 10f, new Vector2(x, y));
            body.IsStatic = true;
            body.Restitution = 0.2f;
            if (isFloor)
            {
                body.Friction = 0.2f;
            }
            body.FixedRotation = true;

        }
       /** public override void earlyUpdate()
        {
            base.earlyUpdate();

            x = body.Position.X;
            y = body.Position.Y;

            if (doc.controls.getKey(Keys.Space) == Controls.Pressed)
            {
                body.ApplyLinearImpulse(new Vector2(0, 20));
            }
            else if (doc.controls.getKey(Keys.Left) == Controls.Held)
            {
                body.ApplyLinearImpulse(new Vector2(-5, 0));
            }
            else if (doc.controls.getKey(Keys.Right) == Controls.Held)
            {
                body.ApplyLinearImpulse(new Vector2(50, 0));
            }
            else if (doc.controls.getKey(Keys.R) == Controls.Pressed)
            {
                //This is the Whipping which is context based.

                //Search area in front of Player

                //Grab nearest point and run code for it.
                /*
                float minDist = 100;
                for(int index = 0; index < [number of points available]; index++){
                    //check distance    
                    if([distance] < minDist)
                    {
                        minDist = [distance];
                        Object = [object at index];
                    }
                }
     
                if([object is swing point])
                {
                    swing(object);
                }
                else if([object is slingshot point])
                {
                    sling(object);
                }
                else if([object is pullable block])
                {
                    pull(object)
                }
                

            }
        }


        //Swinging from a swing point
        private void swing()
        {
            //take the distance and swing the player
        }

        //Slingshot
        private void slingshot()
        {
            //take the distance and sling the player double in the right direction
        }

        //Pull an object
        private void pull()
        {
            //pull the object towards the player one movement.

        }
**/
    }
}
