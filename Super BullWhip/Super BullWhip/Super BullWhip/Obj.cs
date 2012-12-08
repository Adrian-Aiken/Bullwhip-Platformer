using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Super_BullWhip
{
    public class Obj
    {
        // a reference to the game manager class, in case we need to access methods or variables from it
        protected Game1 doc;
        // the object's position in the world, has X, Y, and Z. The Z is mostly useful for placing things in the background so that we can get parallax scrolling.
        public Vector3 pos;
        // the object's speed, is added to pos in each frame
        public Vector3 speed = Vector3.Zero;
        // the origin of the sprite, has X and Y, basically the center of where it is drawn, you'll see when you start using it
        protected Vector2 origin;
        // the object's on-screen position, this is relative to the camera, and is used in the Draw method
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
        // rotation is self explanitory, just the rotation it's drawn it, depth is just a something extra to use to sort how the objects are drawn when they are at the same z-position
        public float rot, depth = 0;
        // alpha is the object's transparency, 0 is fully transparent, and 1 is fully opaque
        public float alpha = 1;
        // basically an overlay color for the object, so like, for the whipspace portals, we would only need one sprite, and then change the color of its object
        public Color color = Color.White;
        // getters and setters for the position, speed, etc, so that instead of having to type obj.pos.x, you can just type obj.x
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
        public float xSpeed
        {
            get { return speed.Y; }
            set { speed.Y = value; }
        }
        public float ySpeed
        {
            get { return speed.Y; }
            set { speed.Y = value; }
        }
        public float zSpeed
        {
            get { return speed.Z; }
            set { speed.Z = value; }
        }
        public Obj(Game1 Doc, float X, float Y, float Z, Texture2D texture)
        {
            doc = Doc;
            tex = texture;
            pos = new Vector3(X, Y, Z);
            //sets the origin to the center of the texture by default
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            // each new object is added to the Game1 class' list of objects
            doc.AddObj(this);
        }
        public virtual void earlyUpdate()
        {
            pos += speed;
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
        public void Draw()
        {
            // draws the object using its texture, screen position, rotation and scale
            doc.SpriteDraw(tex, screenPos, color * alpha, origin, rot, drawScale * scale);
        }
    }
}
