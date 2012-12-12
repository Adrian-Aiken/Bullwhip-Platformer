using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Super_BullWhip
{
    public class Controls
    {
        public static int Pressed = 3;
       public static int Released = 2;
        public static int Held = 1;
        public static int None = 0;
        public static Controls Instance;
        public MouseState mouse;
        private int mouseLB =0;
        private int mouseLB2 = 0;
        public int mouseLB3 = 0;
        private int mouseRB = 0;
        private int mouseRB2 = 0;
        public int mouseRB3 = 0;
        public bool jump = false;
        Keys[] keyArray = new Keys[0];
        Buttons[] buttonArray = new Buttons[0];
        String[] keyString = new String[0];
        String[] buttonString = new String[0];
        int[] stateArray;
        int[] stateArray2;
        int[] stateArray3;
        int[] stateArray4;
        int[] buttonStateArray;
        int[] buttonStateArray2;
        int[] buttonStateArray3;
        int[] buttonStateArray4;
        float vibration = 0;
        float[] vibrationArray = new float[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        float[] vibrationSpeedArray = new float[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int vibrationIndex = 0;
        public int controlMode = 0;
        public Controls()
        {
            Instance = this;
            addKey(Keys.Z);
            addKey(Keys.X);
            addKey(Keys.Up);
            addKey(Keys.Down);
            addKey(Keys.Left);
            addKey(Keys.Right);
            addKey(Keys.A);
            addKey(Keys.S);
            addKey(Keys.D);
            addKey(Keys.F);
            addKey(Keys.C);
            addKey(Keys.V);
            addKey(Keys.B);
            addKey(Keys.Escape);
            addKey(Keys.W);
            addKey(Keys.Q);
            addKey(Keys.Q);
            addKey(Keys.E);
            addKey(Keys.R);
            addKey(Keys.Space);
            addKey(Keys.Enter);
            addKey(Keys.LeftShift);
            addKey(Keys.Delete);
            addKey(Keys.T);
            addKey(Keys.P);
            addKey(Keys.D1);
            addKey(Keys.D2);
            addKey(Keys.N);
            addKey(Keys.M);
            addKey(Keys.LeftControl);
            addButton(Buttons.A);
            addButton(Buttons.B);
            addButton(Buttons.X);
            addButton(Buttons.Y);
            addButton(Buttons.DPadUp);
            addButton(Buttons.DPadDown);
            addButton(Buttons.DPadLeft);
            addButton(Buttons.DPadRight);
        }
        public void update()
        {
            mouseLB3 = 0;
            mouseLB2 = mouseLB;
            mouseRB3 = 0;
            mouseRB2 = mouseRB;
            //Console.WriteLine(getButton(Buttons.DPadRight));
            mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
            mouseLB = 1 ;
                else
            mouseLB = 0;
            if (mouse.RightButton == ButtonState.Pressed)
                mouseRB = 1;
            else
                mouseRB = 0;
            vibration = 0;
            
            for (int i = 0; i < vibrationArray.Length; i++)
            {
                vibration += vibrationArray[i];
                vibrationArray[i] += (0-vibrationArray[i]) * vibrationSpeedArray[i];
                //if (vibrationArray[i] < 0)
                {
                   // vibrationArray[i] = 0;
                    //vibrationSpeedArray[i] = 0;
                }
            }
            //vibration *= vibrationArray.Length;
            if (vibration > 1)
                vibration = 1;
            if (vibration < 0)
                vibration = 0;
            //Console.WriteLine(vibration);
            GamePad.SetVibration(PlayerIndex.One, vibration, vibration);
            if (mouseLB == 1 && mouseLB2 == 1)
                mouseLB3 = 1;
            if (mouseLB == 1 && mouseLB2 == 0)
                mouseLB3 = 3;
            if (mouseLB == 0 && mouseLB2 == 1)
                mouseLB3 = 2;
            if (mouseLB == 0 && mouseLB2 == 0)
                mouseLB3 = 0;

            if (mouseRB == 1 && mouseRB2 == 1)
                mouseRB3 = 1;
            if (mouseRB == 1 && mouseRB2 == 0)
                mouseRB3 = 3;
            if (mouseRB == 0 && mouseRB2 == 1)
                mouseRB3 = 2;
            if (mouseRB == 0 && mouseRB2 == 0)
                mouseRB3 = 0;
            
            for (int i = 0; i < keyArray.Length; i += 1)
            {
                stateArray3[i] = 0;
                stateArray2[i] = stateArray[i];
                if (Keyboard.GetState().IsKeyDown(keyArray[i]))
                {
                    stateArray[i] = 1;
                }
                if (Keyboard.GetState().IsKeyUp(keyArray[i]))
                {
                    stateArray[i] = 0;
                }
                if (stateArray[i] == 1 && stateArray2[i] == 0)
                {
                    controlMode = 0;
                    stateArray3[i] = 3;
                }
                if (stateArray[i] == 0 && stateArray2[i] == 1)
                    stateArray3[i] = 2;
                if (stateArray[i] == 0 && stateArray2[i] == 0)
                {
                    stateArray3[i] = 0;
                    stateArray4[i] = 0;
                }
                if (stateArray[i] == 1 && stateArray2[i] == 1)
                {
                    stateArray3[i] = 1;
                    stateArray4[i] += 1;
                }
            }
            for (int i = 0; i < buttonArray.Length; i += 1)
            {
                buttonStateArray3[i] = 0;
                buttonStateArray2[i] = buttonStateArray[i];
                if (GamePad.GetState(PlayerIndex.One).IsButtonDown(buttonArray[i]))
                {
                    buttonStateArray[i] = 1;
                }
                if (GamePad.GetState(PlayerIndex.One).IsButtonUp(buttonArray[i]))
                {
                    buttonStateArray[i] = 0;
                }
                if (buttonStateArray[i] == 1 && buttonStateArray2[i] == 0)
                {
                    controlMode = 1;
                    buttonStateArray3[i] = 3;
                }
                if (buttonStateArray[i] == 0 && buttonStateArray2[i] == 1)
                    buttonStateArray3[i] = 2;
                if (buttonStateArray[i] == 0 && buttonStateArray2[i] == 0)
                {
                    buttonStateArray3[i] = 0;
                    buttonStateArray4[i] = 0;
                }
                if (buttonStateArray[i] == 1 && buttonStateArray2[i] == 1)
                {
                    buttonStateArray3[i] = 1;
                    buttonStateArray4[i] += 1;
                }
            }
            
        }
        public void vibrate(float intensity, float speed)
    {
        if (intensity < 0)
            intensity = 0;
        if (intensity > 1)
            intensity = 1;
        vibrationArray[vibrationIndex] = intensity;
        vibrationSpeedArray[vibrationIndex] = speed;
        vibrationIndex += 1;
        if (vibrationIndex >= vibrationArray.Length)
            vibrationIndex = 0;
    }
        public int getKey(String s)
        {
            int ind = Array.IndexOf(keyString, s);
            if (controlMode == 1)
                return -1;
            try
            {
                return stateArray3[ind];
            }
            catch (IndexOutOfRangeException)
            {
                
                return 0;
            }
        }
        public static int GetKey(String s)
        {
            return Instance.getKey(s);
        }
        public int getKey(Keys k)
        {
            int ind = Array.IndexOf(keyString, k.ToString());
            if (controlMode == 1)
                return -1;
            try
            {
                return stateArray3[ind];
            }
            catch (IndexOutOfRangeException)
            {
                addKey(k);
                return getKey(k);
            }
        }
        public static int GetKey(Keys s)
        {
            return Instance.getKey(s);
        }
        public int getButton(String b)
        {
            if (controlMode == 0)
                return -1;
            int ind = Array.IndexOf(buttonString, b);
            try
            {
                return buttonStateArray3[ind];
            }
            catch (IndexOutOfRangeException)
            {

                return 0;
            }
        }
        public static int GetButton(String b)
        {
            return Instance.getButton(b);
        }
        public int getButton(Buttons b)
        {
            if (controlMode == 0)
                return -1;
            int ind = Array.IndexOf(buttonString, b.ToString());
            try
            {
                return buttonStateArray3[ind];
            }
            catch (IndexOutOfRangeException)
            {
                addButton(b);
                return getButton(b);
            }
        }
        public static int GetButton(Buttons b)
        {
            return Instance.getButton(b);
        }
        public int getKeyTime(String s)
        {
            int ind = Array.IndexOf(keyString, s);
            return stateArray4[ind];
        }
        public static int GetKeyTime(String s)
        {
            return Instance.getKeyTime(s);
        }
        private void addKey(Keys k)
        {
            Keys[] temp = new Keys[keyArray.Length + 1];
            for (int i = 0; i < keyArray.Length; i += 1)
            {
                temp[i] = keyArray[i];
            }
            temp[keyArray.Length] = k;
            keyArray = temp;
            
            keyString = new String[keyArray.Length];
            for (int i = 0; i < keyArray.Length; i += 1)
                keyString[i] = keyArray[i].ToString();
            stateArray = new int[keyArray.Length];
            stateArray2 = new int[keyArray.Length];
            stateArray3 = new int[keyArray.Length];
            stateArray4 = new int[keyArray.Length];
            for (int i = 0; i < keyArray.Length; i += 1)
            {
                stateArray[i] = 0;
                stateArray2[i] = 0;
                stateArray3[i] = 0;
                stateArray4[i] = 0;
            }


        }
        private void addButton(Buttons b)
        {
            Buttons[] temp = new Buttons[buttonArray.Length + 1];
            for (int i = 0; i < buttonArray.Length; i += 1)
            {
                temp[i] = buttonArray[i];
            }
            temp[buttonArray.Length] = b;
            buttonArray = temp;

            buttonString = new String[buttonArray.Length];
            for (int i = 0; i < buttonArray.Length; i += 1)
                buttonString[i] = buttonArray[i].ToString();
            buttonStateArray = new int[buttonArray.Length];
            buttonStateArray2 = new int[buttonArray.Length];
            buttonStateArray3 = new int[buttonArray.Length];
            buttonStateArray4 = new int[buttonArray.Length];
            for (int i = 0; i < buttonArray.Length; i += 1)
            {
                buttonStateArray[i] = 0;
                buttonStateArray2[i] = 0;
                buttonStateArray3[i] = 0;
                buttonStateArray4[i] = 0;
            }


        }
        


    }
}
