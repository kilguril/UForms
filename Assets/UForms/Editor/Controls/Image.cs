using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls
{
    public class Image : Control
    {
        public Texture      DrawTexture     { get; set; }
        public Material     DrawMaterial    { get; set; }
        public bool         DrawTransparent { get; set; }
        public bool         DrawAlpha       { get; set; }
        public ScaleMode    Scale { get; set; }

        protected override Vector2 DefaultSize {
            get { return new Vector2( 64.0f, 64.0f ); }
        }

        public Image( Texture tex = null, ScaleMode scaleMode = ScaleMode.ScaleToFit, bool drawAlpha = false, bool drawTransparent = true, Material mat = null ) : base()
        {
            DrawTexture     = tex;
            Scale           = scaleMode;
            DrawAlpha       = drawAlpha;
            DrawTransparent = drawTransparent;
            DrawMaterial    = mat;
        }


        public Image( Vector2 position, Vector2 size, Texture tex = null, ScaleMode scaleMode = ScaleMode.ScaleToFit, bool drawAlpha = false, bool drawTransparent = true, Material mat = null ) : base( position, size )
        {
            DrawTexture = tex;
            Scale = scaleMode;
            DrawAlpha = drawAlpha;
            DrawTransparent = drawTransparent;
            DrawMaterial = mat;
        }

        protected override void OnDraw()
        {
            if ( DrawTexture == null )
            {
                return;
            }

            if ( DrawAlpha )
            {
                EditorGUI.DrawTextureAlpha( ScreenRect, DrawTexture, Scale );
            }
            else
            {
                if ( DrawTransparent )
                {
                    EditorGUI.DrawTextureTransparent( ScreenRect, DrawTexture, Scale );
                }
                else
                {
                    EditorGUI.DrawPreviewTexture( ScreenRect, DrawTexture, DrawMaterial, Scale );
                }
            }
        }
    }
}