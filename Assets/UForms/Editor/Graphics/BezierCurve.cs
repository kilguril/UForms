using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace UForms.Graphics
{
    /// <summary>
    /// A bezier curve, defined by two points, and optionally two tangents.
    /// </summary>
    public class BezierCurve : Shape
    {
        private static Texture2D LineTexture
        {
            get
            {
                if ( _lineTex == null )
                {
                    _lineTex = new Texture2D( 2, 2 );
                    _lineTex.SetPixels32( new Color32[] { Color.white, Color.white, Color.white, Color.white } );
                    _lineTex.Apply();
                }

                return _lineTex;
            }
        }

        private static Texture2D _lineTex;

        /// <summary>
        /// Describes how the tangents should be handled.
        /// </summary>
        public enum TangentMode
        {
            /// <summary>
            /// Uniform tangents, constrained on the Y axis and derived from the line length on the X axis.
            /// </summary>
            AutoX,
            
            /// <summary>
            /// Uniform tangents, constrained on the X axis and derived from the line length on the Y axis.
            /// </summary>
            AutoY,

            /// <summary>
            /// Tangents are specified by the user.
            /// </summary>
            Manual
        }

        /// <summary>
        /// The line width in pixels.
        /// </summary>
        public float LineWidth { get; set; }

        /// <summary>
        /// The texture to use when rendering the line. If unspecified, will use a default solid color texture.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// The tangents mode for the line.
        /// </summary>
        public TangentMode Tangents { get; set; }

        /// <summary>
        /// The fixed tangent distance for automatic tangent modes.
        /// </summary>
        public float TangentDistance { get; set; }

        /// <summary>
        /// First tangent.
        /// </summary>
        public Vector2 FromTangent { get; set; }

        /// <summary>
        /// Second tangent.
        /// </summary>
        public Vector2 ToTangent { get; set; }

        /// <summary>
        /// First point.
        /// </summary>
        public Vector2 From { get; set; }

        /// <summary>
        /// Second point.
        /// </summary>
        public Vector2 To { get; set; }

        /// <summary>
        /// Bezier curve constructor.
        /// </summary>
        /// <param name="from">First point.</param>
        /// <param name="to">Second point.</param>
        /// <param name="color">Line color.</param>
        /// <param name="lineWidth">Line width.</param>
        /// <param name="tangentMode">Tangent mode.</param>
        /// <param name="fromTangent">First tangent.</param>
        /// <param name="toTangent">Second tangent.</param>
        /// <param name="zindex">Drawing order.</param>
        /// <param name="drawEarly">If set to true, shape will render before controls.</param>
        public BezierCurve( Vector2 from, Vector2 to, Color color, float lineWidth = 1.0f, TangentMode tangentMode = TangentMode.AutoX, Vector2 fromTangent = default(Vector2), Vector2 toTangent = default(Vector2), int zindex = 0, bool drawEarly = false )
        {
            From            = from;
            To              = to;

            FromTangent     = fromTangent;
            ToTangent       = toTangent;
            Tangents        = tangentMode;

            LineWidth = lineWidth;

            TangentDistance = 0.3f;            

            DrawColor       = color;

            ZIndex          = zindex;
            DrawEarly       = drawEarly;
        }

        /// <summary>
        /// Draws method, used internally.
        /// </summary>
        public override void Draw()
        {
            switch( Tangents )
            {
                case TangentMode.AutoX:
                {
                    float dx = To.x - From.x;

                    FromTangent = From + Vector2.right * dx * TangentDistance;
                    ToTangent = To - Vector2.right * dx * TangentDistance;
                }
                break;

                case TangentMode.AutoY:
                {
                    float dy = To.y - From.y;

                    FromTangent = From + Vector2.up * dy * TangentDistance;
                    ToTangent = To - Vector2.up * dy * TangentDistance;
                }
                break;
            }

            Texture2D tex = ( Texture != null ? Texture : LineTexture );
            Handles.DrawBezier( From, To, FromTangent, ToTangent, DrawColor, tex, LineWidth );
        }
    }
}