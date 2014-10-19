using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class Vector2Field : AbstractField< Vector2 >
    {
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        public Vector2Field( Vector2 value = default(Vector2), string label = "" ) : base( value, label )
        {
            
        }

        public Vector2Field( Vector2 position, Vector2 size, Vector2 value = default(Vector2), string label = "" ) : base( position, size, value, label )
        {

        }

        protected override Vector2 DrawAndUpdateValue()
        {
            return EditorGUI.Vector2Field( m_fieldRect, Label, m_cachedValue );
        }

        protected override bool TestValueEquality( Vector2 oldval, Vector2 newval )
        {
            return oldval.Equals( newval );
        }
    }
}