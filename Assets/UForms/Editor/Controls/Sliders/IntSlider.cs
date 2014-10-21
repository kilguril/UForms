using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Controls.Fields;

namespace UForms.Controls.Sliders
{
    public class IntSlider : AbstractField< int >
    {
        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }        

        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 300.0f, 16.0f ); }
        }

        public int LeftValue    { get; set; }
        public int RightValue   { get; set; }
        
        public IntSlider( int leftValue = 0, int rightValue = 0, string label = "", int value = 0 ) : base( value, label )
        {
            LeftValue   = leftValue;
            RightValue  = rightValue;
        }

        public IntSlider( Vector2 position, Vector2 size, int leftValue = 0, int rightValue = 0, string label = "", int value = 0 ) : base( position, size, value, label )
        {
            LeftValue = leftValue;
            RightValue = rightValue;
        }

        protected override int DrawAndUpdateValue()
        {
            return EditorGUI.IntSlider( m_fieldRect, Label, m_cachedValue, LeftValue, RightValue );
        }

        protected override bool TestValueEquality( int oldval, int newval )
        {
            return oldval.Equals( newval );
        }
    }
}