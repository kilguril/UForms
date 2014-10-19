using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class IntField : AbstractField< int >
    {
        public IntField( Vector2 position, Vector2 size, int value = 0, string label = "" ) : base( position, size, value, label )
        {

        }

        protected override int DrawAndUpdateValue()
        {
            return EditorGUI.IntField( m_fieldRect, Label, m_cachedValue );
        }

        protected override bool TestValueEquality( int oldval, int newval )
        {
            return oldval.Equals( newval );
        }
    }
}