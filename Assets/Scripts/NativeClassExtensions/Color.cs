using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NativeClassExtensions
{
    public static class ColorExtensions
    {
        public static Color Subtractive(this Color color, Color SecondColor)
        {
            Color output = new Color();
            output.r = (color.r*SecondColor.r);
            output.g = (color.g*SecondColor.g);
            output.b = (color.b*SecondColor.b);
            output.a = 1;
            return output;
        }

        public static Color Overlay(this Color color, Color SecondColor)
        {
            Color output = new Color();
            if(color.r < 0.5f) output.r = 2*(color.r*SecondColor.r);
            else output.r = 1 - 2*(1-color.r)*Mathf.Sqrt(color.r)*(2*SecondColor.r-1);
            if(color.g < 0.5f) output.g = 2*(color.g*SecondColor.g);
            else output.g = 1 - 2*(1-color.g)*Mathf.Sqrt(color.g)*(2*SecondColor.g-1);
            if(color.b < 0.5f) output.b = 2*(color.b*SecondColor.b);
            else output.b = 1 - 2*(1-color.b)*Mathf.Sqrt(color.b)*(2*SecondColor.b-1);
            output.a = 1;
            return output;
        }
    }
}