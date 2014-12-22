using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    /// <summary>
    /// 
    /// </summary>
    public class CurveField : AbstractField< AnimationCurve >
    {
        /// <summary>
        /// 
        /// </summary>
        public Color    CurveColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Rect     CurveRect { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override bool UseBackingFieldChangeDetection
        {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curveColor"></param>
        /// <param name="curveRect"></param>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public CurveField( Color curveColor = default(Color), Rect curveRect = default(Rect), AnimationCurve value = default(AnimationCurve), string label = "" ) : base( value, label )
        {
            CurveColor = curveColor;
            CurveRect = curveRect;       
            
            if ( value == null )
            {
                m_cachedValue = AnimationCurve.Linear( 0f, 0f, 1f, 1f );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="curveColor"></param>
        /// <param name="curveRect"></param>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public CurveField( Vector2 position, Vector2 size, Color curveColor = default(Color), Rect curveRect = default(Rect), AnimationCurve value = default(AnimationCurve), string label = "" ) : base( position, size, value, label )
        {
            CurveColor = curveColor;
            CurveRect = curveRect;

            if ( value == null )
            {
                m_cachedValue = AnimationCurve.Linear( 0f, 0f, 1f, 1f );
            }
        }


        /// <summary>
        /// Apparently EditorGUI.CurveField is tightnly related to the inspector GUI and will throw an expection if the curve editor is opened without an inspector panel available.
        /// To bypass this issue, we will disable the control's interactivity if no selection is available.
        /// TODO :: probably need to find a more elegant solution as the current behavior limits the usefulness of this control.
        /// </summary>
        /// <returns></returns>
        protected override AnimationCurve DrawAndUpdateValue()
        {
            bool isActive = ( Selection.objects.Length > 0 );
            AnimationCurve temp = m_cachedValue;

            if ( isActive )
            {
                temp = EditorGUI.CurveField( ScreenRect, Label, m_cachedValue, CurveColor, CurveRect );
            }
            else
            {
                EditorGUI.BeginDisabledGroup( true );
                EditorGUI.CurveField( ScreenRect, Label, m_cachedValue, CurveColor, CurveRect );
                EditorGUI.EndDisabledGroup();
            }

            return temp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
        protected override bool TestValueEquality( AnimationCurve oldval, AnimationCurve newval )
        {
            if ( oldval == null || newval == null )
            {
                if ( oldval == null && newval == null )
                {
                    return true;
                }

                return false;
            }

            return oldval.Equals( newval );
        }
    }
}