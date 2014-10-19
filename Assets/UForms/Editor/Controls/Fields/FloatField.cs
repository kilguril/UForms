using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class FloatField : AbstractField< float >
    {
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