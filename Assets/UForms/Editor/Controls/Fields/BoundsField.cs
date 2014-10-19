using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class BoundsField : AbstractField< Bounds >
    {
        public BoundsField( Vector2 position, Vector2 size, Bounds value = default(Bounds), string label = "" ) : base( position, size, value, label )
        {
            
        }

        protected override Bounds DrawAndUpdateValue()
        {
            return EditorGUI.BoundsField( m_fieldRect, new GUIContent( Label ), m_cachedValue );
        }

        protected override bool TestValueEquality( Bounds oldval, Bounds newval )
        {
            return oldval.Equals( newval );
        }
    }
}