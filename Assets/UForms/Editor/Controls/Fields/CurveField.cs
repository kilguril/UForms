using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class CurveField : AbstractField< AnimationCurve >
    {
        public Color    CurveColor { get; set; }
        public Rect     CurveRect { get; set; }

        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        protected override bool UseBackingFieldChangeDetection
        {
            get { return false; }
        }

        public CurveField( Color curveColor = default(Color), Rect curveRect = default(Rect), AnimationCurve value = default(AnimationCurve), string label = "" ) : base( value, label )
        {
            CurveColor = curveColor;
            CurveRect = curveRect;       
            
            if ( value == null )
            {
                m_cachedValue = AnimationCurve.Linear( 0f, 0f, 1f, 1f );
            }
        }

        public CurveField( Vector2 position, Vector2 size, Color curveColor = default(Color), Rect curveRect = default(Rect), AnimationCurve value = default(AnimationCurve), string label = "" ) : base( position, size, value, label )
        {
            CurveColor = curveColor;
            CurveRect = curveRect;

            if ( value == null )
            {
                m_cachedValue = AnimationCurve.Linear( 0f, 0f, 1f, 1f );
            }
        }

        // Apparently EditorGUI.CurveField is tightnly related to the inspector GUI and will throw an expection if the curve editor is opened without an inspector panel available.
        // To bypass this issue, we will disable the control's interactivity if no selection is available.
        // TODO :: probably need to find a more elegant solution as the current behavior limits the usefulness of this control.
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