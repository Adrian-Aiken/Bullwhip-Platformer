using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Dynamics.Joints;
namespace Super_BullWhip
{
    public class Player:Obj
    {
        
        float gravity = 0.5f;
        Obj Point;
        Obj[] rangePoints;
        float radius = 400;
        float deg = 0;
        float topspeed = 5;
        Obj obj, obj2;
        DistanceJoint f;
        DistanceJoint whipJoint;
        Obj[] whip;
        Obj whipPoint;
        public Player(Game1 Doc, float X, float Y, float Z)
            :base(Doc,X,Y,Z,Doc.LoadTex("Character"))
        {
            Global.Player = this;
            scale = 0.5f;
            //Point = new Obj(doc
            createRecBody(10, 0, 1f, true,false);
            obj = new Obj(doc, x + 400, y - 400, z, doc.LoadTex("Square"));
            obj.createRecBody(1,0.5f,0.3f, true, true);
            obj2 = new Obj(doc, x + 800, y - 400, z, doc.LoadTex("Square"));
            obj2.createRecBody(1, 0.5f, 0.3f, true, true);
            //body.JointList = new JointEdge();
            //JointFactory.CreateDistanceJoint(doc.getWorld(), body, obj.body, /*ConvertUnits.ToSimUnits(pos.ToVector2() + new Vector2(0,400))*/body.Position, obj.body.Position);
            //r = JointFactory.CreateRevoluteJoint(doc.World, body, obj.body, obj.body.Position);
            /*f = JointFactory.CreateDistanceJoint(doc.World, body, obj.body, obj.body.Position, obj.body.Position);
            f.Frequency = 100;*/
            //Console.WriteLine(r.MotorEnabled);
            //body.JointList.Other = obj.body;
            //body.JointList.Other
            whip = new Obj[7];
            for (int i = 0; i < whip.Length; i++)
            {
                Obj w = new Obj(doc, x + 50 +  (i * 50), y, z, doc.LoadTex("Square"));
                whip[i] = w;
                w.Scale.X = 0.5f;
                w.Scale.Y = 0.5f;
                w.createRecBody(1, 0, 0, true, false);
                w.body.IgnoreCollisionWith(body);
                if (i > 0)
                {
                    for (int o = i - 1; o >= 0; o--)
                    {
                        w.body.IgnoreCollisionWith(whip[i].body);
                    }
                }
                if (i == 0)
                {
                    f = JointFactory.CreateDistanceJoint(doc.World, body, w.body, w.body.Position, w.body.Position);
                    f.DampingRatio = 1f;
                    f.Length = ConvertUnits.ToSimUnits(150);
                }
                else
                {
                    f = JointFactory.CreateDistanceJoint(doc.World, w.body, whip[i - 1].body, whip[i - 1].body.Position, whip[i - 1].body.Position);
                    f.DampingRatio = 1f;
                    f.Frequency = 10;
                    f.Length = ConvertUnits.ToSimUnits(50);
                }
                if (i == whip.Length - 1)
                {
                    w.body.Position = obj.body.Position;
                    whipPoint = w;
                    whipJoint = JointFactory.CreateDistanceJoint(doc.World, w.body, obj.body, obj.body.Position, obj.body.Position);
                    Console.WriteLine(whipJoint.Frequency);
                    whipJoint.Frequency = 10f;
                    whipJoint.Length = ConvertUnits.ToSimUnits(10);
                    whipJoint.DampingRatio = 1;
                }
            }

        }
        private void whipTo(Obj o)
        {
            whipJoint.BodyB = o.body;
            whipJoint.LocalAnchorA = whipJoint.LocalAnchorB = o.body.Position = o.body.Position;
            whipJoint.Frequency = 10;
            whipPoint.body.IgnoreCollisionWith(o.body);
        }
        private void endWhip()
        {
            whipJoint.Frequency = 0.0001f;
        }
        public override void earlyUpdate()
        {
            base.earlyUpdate();
            if (Controls.GetKey(Keys.Space) == Controls.Pressed || Controls.GetButton(Buttons.A) == Controls.Pressed)
            {
                
                ContactEdge c = body.ContactList;
                if (c != null)
                {
                    body.ApplyLinearImpulse(new Vector2(0, -150));
                    Console.WriteLine(c.Contact.IsTouching());
                }
            }
            if (doc.controls.getKey(Keys.Left) == Controls.Held || Controls.GetButton(Buttons.LeftThumbstickLeft) == Controls.Held)
            {
                if (body.GetLinearVelocityFromLocalPoint(new Vector2(x, y)).X > -topspeed)
                body.ApplyLinearImpulse(new Vector2(-4.5f, 0));
                //Console.WriteLine(body.GetLinearVelocityFromLocalPoint(new Vector2(x, y)));
            }
            if (doc.controls.getKey(Keys.Right) == Controls.Held || Controls.GetButton(Buttons.LeftThumbstickRight) == Controls.Held)
            {
                if (body.GetLinearVelocityFromLocalPoint(new Vector2(x, y)).X < topspeed)
                body.ApplyLinearImpulse(new Vector2(4.5f, 0));
            }
            if (Controls.GetKey(Keys.R) == Controls.Pressed || Controls.GetButton(Buttons.RightShoulder) == Controls.Pressed)
            {
                whipTo(obj2);
            }
            else if (doc.controls.getKey(Keys.R) == Controls.Held)
            {
                //This is the Whipping which is context based.

                //Search area in front of Player


                //Grab nearest point and run code for it.
                float minDist = radius;
                for (int index = 0; index < doc.objList.Count; index++)
                {
                    //check distance    
                    if (doc.objList[index].type != PointType.Normal && MyMath.Distance(pos, doc.objList[index].pos) < minDist)
                    {
                        minDist = MyMath.Distance(pos, doc.objList[index].pos);
                        Point = doc.objList[index];
                    }
                }
                if (Point == null) return;
                if (Point.type == PointType.SwingPoint)
                {
                    swing(Point);
                }
                else if (Point.type == PointType.SlingPoint)
                {
                    sling(Point);
                }
                else if (Point.type == PointType.Pullblock)
                {
                    pull(Point);
                }
            }
            if (Controls.GetKey(Keys.R) == Controls.Released || Controls.GetButton(Buttons.RightShoulder) == Controls.Released)
            {
                endWhip();
            }
            //Console.WriteLine(body.GetLinearVelocityFromLocalPoint(new Vector2(x, y)));
            //Console.WriteLine(body.Friction);
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
