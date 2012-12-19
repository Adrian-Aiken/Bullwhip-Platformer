using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace Super_BullWhip
{
    public class Obj
    {
        // body for the FarseerPhysics world
        public Body body;
        // a reference to the game manager class, in case we need to access methods or variables from it
        protected Game1 doc;
        // the object's position in the world, has X, Y, and Z. The Z is mostly useful for placing things in the background so that we can get parallax scrolling.
        public Vector3 pos;
        // the origin of the sprite, has X and Y, basically the center of where it is drawn, you'll see when you start using it
        Vector2 screenPos = Vector2.Zero;
        // the object's sprite (called a texture), it's static for now, will add a separate class for animated objects later
        protected Texture2D tex;
        // the object's scale, with X and Y
        public Vector2 Scale = Vector2.One;
        // a multiplyer for the object's scale
        public float scale = 1;
        // the scale the object is drawn it, this is relative to the amount of zoom on the camera
        private Vector2 drawScale = Vector2.One;
        // zFactor is basically a number that is calculated based on the difference between the object's z-position and the camera's z-position
        float zFactor = 1;
        // rotation is obvious, depth is just a something extra to use to sort how the objects are drawn when they are at the same z-position
        public float rot, depth = 0;
        // alpha is the object's transparency, 0 is fully transparent, and 1 is fully opaque
        public float alpha = 1;
        // basically an overlay color for the object, so like, for the whipspace portals, we would only need one sprite, and then change the color of its object
        public Color color = Color.White;
        // getters and setters for the position, speed, etc, so that instead of having to type obj.pos.x, you can just type obj.x
        // type of object
        public enum PointType { SwingPoint, SlingPoint, Pullblock, Normal }
        public PointType type = PointType.Normal;
        protected Vector2 or;
        public bool isFloor = false;
        public float x
        {
            get { return pos.X; }
            set { pos.X = value; }
        }
        public float y
        {
            get { return pos.Y; }
            set { pos.Y = value; }
        }
        public float z
        {
            get { return pos.Z; }
            set { pos.Z = value; }
        }
        public Obj(Game1 Doc, float X, float Y, float Z, Texture2D texture)
        {
            doc = Doc;
            tex = texture;
            pos = new Vector3(X, Y, Z);
            // each new object is added to the Game1 class' list of objects
            doc.AddObj(this);
            or = new Vector2((float)tex.Width / 2f, (float)tex.Height / 2);
        }
        public virtual void earlyUpdate()
        {
            if (body != null)
                updateBody();
        }
        public virtual void Update()
        {

        }
        public virtual void lateUpdate()
        {
            
            float zDiff = Camera.z - pos.Z + Camera.Depth;
            if (zDiff != 1)
                zFactor = (float)Math.Pow(Camera.Depth / zDiff, Camera.Power);
            else zFactor = 1;
            drawScale = Scale * zFactor;
            screenPos.X = (Global.Width / 2f) + ((pos.X - Camera.x) * zFactor);
            screenPos.Y = (Global.Height / 2f) + ((pos.Y - Camera.y) * zFactor);
        }
        protected virtual void updateBody()
        {
            x = ConvertUnits.ToDisplayUnits(body.Position.X);
            y = ConvertUnits.ToDisplayUnits(body.Position.Y);
            rot = MathHelper.ToDegrees(body.Rotation);
            rot = body.Rotation;
        }
        public virtual void createRecBody(float density, float bounce, float friction, bool fixedRotation, bool isStatic)
        {
            body = BodyFactory.CreateRectangle(doc.getWorld(), ConvertUnits.ToSimUnits(tex.Width * scale * Scale.X), ConvertUnits.ToSimUnits(tex.Height * scale * Scale.Y), density, new Vector2(ConvertUnits.ToSimUnits(x), ConvertUnits.ToSimUnits(y)));
            body.BodyType = BodyType.Dynamic;
            body.Restitution = bounce;
            body.Friction = friction;
            body.FixedRotation = fixedRotation;
            body.IsStatic = isStatic;
        }
        protected virtual void destroyBody()
        {
            if (body != null)
            {
                body.Dispose();
            }
            body = null;
        }
        public void Draw()
        {
            // draws the object using its texture, screen position, rotation and scale
            doc.SpriteDraw(tex, screenPos, color * alpha, or, rot, drawScale * scale * Scale);
        }
        public void remove()
        {
            doc.RemoveObj(this);
        }
    }
}
