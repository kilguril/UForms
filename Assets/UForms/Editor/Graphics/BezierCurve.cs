using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace UForms.Graphics
{
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


        public enum TangentMode
        {
            AutoX,
            AutoY,
            Manual
        }

        public float LineWidth { get; set; }

        public Texture2D Texture { get; set; }

        public TangentMode Tangents { get; set; }

        public float TangentDistance { get; set; }

        public Vector2 FromTangent { get; set; }

        public Vector2 ToTangent { get; set; }

        public Vector2 From { get; set; }

        public Vector2 To { get; set; }

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