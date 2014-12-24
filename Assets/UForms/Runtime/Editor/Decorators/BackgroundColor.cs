using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;
using UForms.Attributes;

namespace UForms.Decorators
{
    /// <summary>
    /// This decorator paint's the controls bounding screen rectangle with a solid color. Useful mainly for debugging.
    /// </summary>

    [ExposeControl( "Background Color", "Decorators" )]
    public class BackgroundColor : Decorator
    {
        /// <summary>
        /// The background color.
        /// </summary>
        public Color BgColor { get; set; }

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public BackgroundColor()
            : base()
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="color">The background color.</param>
        public BackgroundColor( Color color )
            : base()
        {
            BgColor = color;
        }

        /// <summary>
        /// Implementation of the OnDraw step.
        /// </summary>
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