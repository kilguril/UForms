using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace UForms.Graphics
{
    public class Line : Shape
    {
        public Vector2 From { get; set; }

        public Vector2 To { get; set; }

        public Line( Vector2 from, Vector2 to, Color color, int zindex = 0, bool drawEarly = false )
        {
            From        = from;
            To          = to;
            DrawColor   = color;
            
            ZIndex      = zindex;
            DrawEarly   = drawEarly;
        }

        public override void Draw()
        {
            Handles.color = DrawColor;
            Handles.DrawLine( From, To );            
        }
    }
}