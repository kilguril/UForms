using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class Label : Control
    {
        /// <summary>
        /// 
        /// </summary>
        public bool     Selectable  { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public  bool    DropShadow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string   Text        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override Vector2 DefaultSize {
            get { return new Vector2( 100.0f, 30.0f ); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="selectable"></param>
        public Label( string text = "", bool selectable = false ) : base()
        {
            Text = text;
            Selectable = selectable;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="text"></param>
        /// <param name="selectable"></param>
        public Label( Vector2 position, Vector2 size, string text = "", bool selectable = false) : base( position, size )
        {            
            Text        = text;
            Selectable  = selectable;
        }


        /// <summary>
        /// 
        /// </summary>
        protected override void OnDraw()
        {
            if ( Selectable )
            {
                EditorGUI.SelectableLabel( ScreenRect, Text );
            }
            else
            {
                if ( DropShadow )
                {
                    EditorGUI.DropShadowLabel( ScreenRect, Text );
                }
                else
                {
                    GUI.Label( ScreenRect, Text );
                }
            }            
        }
    }
}