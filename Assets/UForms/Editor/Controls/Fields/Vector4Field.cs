using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class Vector4Field : AbstractField< Vector4 >
    {
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 32.0f ); }
        }

        public Vector4Field( Vector4 value = default(Vector4), string label = "" ) : base( value, label )
        {
            
        }

        public Vector4Field( Vector4 position, Vector4 size, Vector4 value = default(Vector4), string label = "" ) : base( position, size, value, label )
        {

        }

        protected override Vector4 DrawAndUpdateValue()
        {
            return EditorGUI.Vector4Field( m_fieldRect, Label, m_cachedValue );
        }

        protected override bool TestValueEquality( Vector4 oldval, Vector4 newval )
        {
            return oldval.Equals( newval );
        }
    }
}