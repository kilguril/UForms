using UnityEngine;
using System.Collections;

namespace UForms.Controls
{
    public class Label : Control
    {
        public string Text { get; set; }

        private Rect  m_labelRect;

        public Label( Vector2 position, Vector2 size, string text = "") : base( position, size )
        {            
            Text = text;
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
            GUI.Label( m_labelRect, Text );
        }
    }
}