using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace UForms.Graphics
{
    /// <summary>
    /// A line shape defined by two screen points.
    /// </summary>
    public class Line : Shape
    {
        /// <summary>
        /// The first point of the line.
        /// </summary>
        public Vector2 From { get; set; }

        /// <summary>
        /// The second point of the line.
        /// </summary>
        public Vector2 To { get; set; }

        /// <summary>
        /// Line shape constructor.
        /// </summary>
        /// <param name="from">First point.</param>
        /// <param name="to">Second point.</param>
        /// <param name="color">Line color.</param>
        /// <param name="zindex">Drawing order.</param>
        /// <param name="drawEarly">If set to true, shape will render before controls.</param>
        public Line( Vector2 from, Vector2 to, Color color, int zindex = 0, bool drawEarly = false )
        {
            From        = from;
            To          = to;
            DrawColor   = color;
            
            ZIndex      = zindex;
            DrawEarly   = drawEarly;
        }

        /// <summary>
        /// Draws method, used internally.
        /// </summary>
        public override void Draw()
        {
            Handles.color = DrawColor;
            Handles.DrawLine( From, To );            
        }
    }
}