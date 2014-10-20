using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class Vector3Field : AbstractField<Vector3>
    {
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        public Vector3Field( Vector3 value = default(Vector3), string label = "" ) : base( value, label )
        {
            
        }

        public Vector3Field( Vector3 position, Vector3 size, Vector3 value = default(Vector3), string label = "" ) : base( position, size, value, label )
        {

        }

        protected override Vector3 DrawAndUpdateValue()
        {
            return EditorGUI.Vector3Field( m_fieldRect, Label, m_cachedValue );
        }

        protected override bool TestValueEquality( Vector3 oldval, Vector3 newval )
        {
            return oldval.Equals( newval );
        }
    }
}