using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class FloatField : AbstractField< float >
    {
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 100.0f, 16.0f ); }
        }

        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        public FloatField( float value = 0, string label = "" ) : base( value, label )
        {
            
        }

        public FloatField( Vector2 position, Vector2 size, float value = 0, string label = "" ) : base( position, size, value, label )
        {

        }

        protected override float DrawAndUpdateValue()
        {
            return EditorGUI.FloatField( m_fieldRect, Label, m_cachedValue );
        }

        protected override bool TestValueEquality( float oldval, float newval )
        {
            return oldval.Equals( newval );
        }
    }
}