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
    public class Player:Obj
    {
        Body body;
        float gravity = 0.5f;
        public Player(Game1 Doc, float X, float Y, float Z)
            :base(Doc,X,Y,Z,Doc.LoadTex("Character"))
        {
            Global.Player = this;
            origin.Y = tex.Height;
            scale = 0.5f;
            body = BodyFactory.CreateRectangle(doc.getWorld(), ConvertUnits.ToSimUnits(20), ConvertUnits.ToSimUnits(80), 10f, new Vector2(400, 500));
            body.BodyType = BodyType.Dynamic;
            body.Restitution = 0.2f;
            body.Friction = 0.2f;
            
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
                */ 
                
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
    }
}
