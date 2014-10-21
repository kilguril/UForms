using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Controls.Fields;

namespace UForms.Controls.Sliders
{
    public class FloatSlider : AbstractField< float >
    {
        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }        

        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 300.0f, 16.0f ); }
        }

        public float LeftValue    { get; set; }
        public float RightValue     { get; set; }
        
        public FloatSlider( float leftValue = 0, float rightValue = 0, string label = "", float value = 0 ) : base( value, label )
        {
            LeftValue   = leftValue;
            RightValue  = rightValue;
        }

        public FloatSlider( Vector2 position, Vector2 size, float leftValue = 0, float rightValue = 0, string label = "", float value = 0 ) : base( position, size, value, label )
        {
            LeftValue = leftValue;
            RightValue = rightValue;
        }

        protected override float DrawAndUpdateValue()
        {
            return EditorGUI.Slider( m_fieldRect, Label, m_cachedValue, LeftValue, RightValue );
        }

        protected override bool TestValueEquality( float oldval, float newval )
        {
            return oldval.Equals( newval );
        }
    }
}