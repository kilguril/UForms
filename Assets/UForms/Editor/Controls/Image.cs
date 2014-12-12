using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class Image : Control
    {
        /// <summary>
        /// 
        /// </summary>
        public Texture      DrawTexture     { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Material     DrawMaterial    { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool         DrawTransparent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool         DrawAlpha       { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ScaleMode    Scale           { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Rect?        TexCoords       { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override Vector2 DefaultSize {
            get { return new Vector2( 64.0f, 64.0f ); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="texCoords"></param>
        /// <param name="scaleMode"></param>
        /// <param name="drawAlpha"></param>
        /// <param name="drawTransparent"></param>
        /// <param name="mat"></param>
        public Image( Texture tex = null, Rect? texCoords = null, ScaleMode scaleMode = ScaleMode.ScaleToFit, bool drawAlpha = false, bool drawTransparent = true, Material mat = null ) : base()
        {
            DrawTexture     = tex;
            Scale           = scaleMode;
            DrawAlpha       = drawAlpha;
            DrawTransparent = drawTransparent;
            DrawMaterial    = mat;
            TexCoords       = texCoords;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="tex"></param>
        /// <param name="texCoords"></param>
        /// <param name="scaleMode"></param>
        /// <param name="drawAlpha"></param>
        /// <param name="drawTransparent"></param>
        /// <param name="mat"></param>
        public Image( Vector2 position, Vector2 size, Texture tex = null, Rect? texCoords = null, ScaleMode scaleMode = ScaleMode.ScaleToFit, bool drawAlpha = false, bool drawTransparent = true, Material mat = null ) : base( position, size )
        {
            DrawTexture         = tex;
            Scale               = scaleMode;
            DrawAlpha           = drawAlpha;
            DrawTransparent     = drawTransparent;
            DrawMaterial        = mat;
            TexCoords           = texCoords;
        }


        /// <summary>
        /// 
        /// </summary>
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
                if ( TexCoords != null )
                {
                    GUI.DrawTextureWithTexCoords( ScreenRect, DrawTexture, TexCoords ?? default(Rect), DrawTransparent );
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
}