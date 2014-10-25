using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;

namespace UForms.Decorators
{
    public class FitContent : Decorator
    {
        public bool FitHorizontal { get; set; }
        public bool FitVertical { get; set; }
        public bool AllowShrinkHorizontal { get; set; }
        public bool AllowShrinkVertical { get; set; }

        public FitContent() : base()
        {
            FitHorizontal = true;
            FitVertical   = true;
        }

        public FitContent( bool fitHorizontal, bool fitVertical, bool allowShrinkHorizontal, bool allowShrinkVertical ) : base()
        {
            FitHorizontal         = fitHorizontal;
            FitVertical           = fitVertical;
            AllowShrinkHorizontal = allowShrinkHorizontal;
            AllowShrinkVertical   = allowShrinkVertical;
        }

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
                    sizex = content.yMax;
                }
                else
                {
                    sizey = ( content.yMax > m_boundControl.Size.y ? content.yMax : m_boundControl.Size.y );
                }
            }
            else
            {
                sizex = m_boundControl.Size.y;
            }

            m_boundControl.SetSize( sizex, sizey, Control.MetricsUnits.Pixel, Control.MetricsUnits.Pixel );
        }
    }
}