using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;

namespace UForms.Decorators
{
    /// <summary>
    /// This decorator resizes the control to match the size of the contained content.
    /// </summary>
    public class FitContent : Decorator
    {
        /// <summary>
        /// Should the control be resized horizontally.
        /// </summary>
        public bool FitHorizontal { get; set; }

        /// <summary>
        /// Should the control be resized vertically.
        /// </summary>
        public bool FitVertical { get; set; }

        /// <summary>
        /// Should the control be shrunk horizontally in case the content does not fill the control.
        /// </summary>
        public bool AllowShrinkHorizontal { get; set; }

        /// <summary>
        /// Should the control be shrunk vertically in case the content does not fill the control.
        /// </summary>
        public bool AllowShrinkVertical { get; set; }

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public FitContent() : base()
        {
            FitHorizontal = true;
            FitVertical   = true;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fitHorizontal">Should the control be resized horizontally.</param>
        /// <param name="fitVertical">Should the control be resized vertically.</param>
        /// <param name="allowShrinkHorizontal">Should the control be shrunk horizontally in case the content does not fill the control.</param>
        /// <param name="allowShrinkVertical">Should the control be shrunk vertically in case the content does not fill the control.</param>
        public FitContent( bool fitHorizontal, bool fitVertical, bool allowShrinkHorizontal, bool allowShrinkVertical ) : base()
        {
            FitHorizontal         = fitHorizontal;
            FitVertical           = fitVertical;
            AllowShrinkHorizontal = allowShrinkHorizontal;
            AllowShrinkVertical   = allowShrinkVertical;
        }

        /// <summary>
        /// Implementation of the OnLayout step.
        /// </summary>
        protected override void OnLayout()
        {            
            Rect content = m_boundControl.GetContentBounds();

            float sizex = 0.0f;
            float sizey = 0.0f;

            if ( FitHorizontal )
            {
                if ( AllowShrinkHorizontal )
                {
                    sizex = content.xMax;
                }
                else
                {
                    sizex = ( content.xMax > m_boundControl.Size.x ? content.xMax : m_boundControl.Size.x );
                }
            }
            else
            {
                sizex = m_boundControl.Size.x;
            }

            if ( FitVertical )
            {
                if ( AllowShrinkVertical )
                {
                    sizey = content.yMax;
                }
                else
                {
                    sizey = ( content.yMax > m_boundControl.Size.y ? content.yMax : m_boundControl.Size.y );
                }
            }
            else
            {
                sizey = m_boundControl.Size.y;
            }

            m_boundControl.SetSize( sizex, sizey, FitHorizontal ? Control.MetricsUnits.Pixel : m_boundControl.WidthUnits, FitVertical ? Control.MetricsUnits.Pixel : m_boundControl.HeightUnits );
        }
    }
}