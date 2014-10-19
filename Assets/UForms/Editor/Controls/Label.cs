using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls
{
    public class Label : Control
    {
        public bool     Selectable  { get; set; }
        public string   Text        { get; set; }

        private Rect    m_labelRect;

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


        protected override void OnLayout()
        {
            m_labelRect.Set(
                ScreenPosition.x + MarginLeftTop.x,
                ScreenPosition.y + MarginLeftTop.y,
                Size.x,
                Size.y
            );
        }


        protected override void OnDraw()
        {
            if ( Selectable )
            {
                EditorGUI.SelectableLabel( m_labelRect, Text );
            }
            else
            {
                GUI.Label( m_labelRect, Text );    
            }            
        }
    }
}