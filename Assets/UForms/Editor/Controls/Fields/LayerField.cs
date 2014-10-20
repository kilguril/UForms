using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class LayerField : AbstractField< int >
    {
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        public LayerField( int value = 0, string label = "" ) : base( value, label )
        {
            
        }

        public LayerField( Vector2 position, Vector2 size, int value = 0, string label = "" ) : base( position, size, value, label )
        {

        }

        protected override int DrawAndUpdateValue()
        {            
            return EditorGUI.LayerField( m_fieldRect, Label, m_cachedValue );
        }

        protected override bool TestValueEquality( int oldval, int newval )
        {
            return oldval.Equals( newval );
        }
    }
}