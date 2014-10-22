using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class IntField : AbstractField< int >
    {
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 100.0f, 16.0f ); }
        }

        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        public IntField( int value = 0, string label = "" ) : base( value, label )
        {
            
        }

        public IntField( Vector2 position, Vector2 size, int value = 0, string label = "" ) : base( position, size, value, label )
        {

        }

        protected override int DrawAndUpdateValue()
        {
            return EditorGUI.IntField( ScreenRect, Label, m_cachedValue );
        }

        protected override bool TestValueEquality( int oldval, int newval )
        {
            return oldval.Equals( newval );
        }
    }
}