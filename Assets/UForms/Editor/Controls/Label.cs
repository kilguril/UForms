using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls
{
    public class Label : Control
    {
        public bool     Selectable  { get; set; }

        public  bool    DropShadow { get; set; }
        public string   Text        { get; set; }


        protected override Vector2 DefaultSize {
            get { return new Vector2( 100.0f, 30.0f ); }
        }

        public Label( string text = "", bool selectable = false ) : base()
        {
            Text = text;
            Selectable = selectable;
        }


        public Label( Vector2 position, Vector2 size, string text = "", bool selectable = false) : base( position, size )
        {            
            Text        = text;
            Selectable  = selectable;
        }

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