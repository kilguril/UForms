using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;

namespace UForms.Decorators
{
    public class BackgroundColor : Decorator
    {
        public Color BgColor { get; set; }

        public BackgroundColor()
            : base()
        {

        }

        public BackgroundColor( Color color )
            : base()
        {
            BgColor = color;
        }

        protected override void OnDraw()
        {
            if ( m_boundControl.ResetPivotRoot )
            {
                EditorGUI.DrawRect( new Rect( 0.0f, 0.0f, m_boundControl.ScreenRect.width, m_boundControl.ScreenRect.height ), BgColor );
            }
            else
            {
                EditorGUI.DrawRect( m_boundControl.ScreenRect, BgColor );
            }
        }
    }
}