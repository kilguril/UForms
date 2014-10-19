using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class RectField : AbstractField< Rect >
    {
        public RectField( Vector2 position, Vector2 size, Rect value = default(Rect), string label = "" ) : base( position, size, value, label )
        {

        }

        protected override Rect DrawAndUpdateValue()
        {
            return EditorGUI.RectField( m_fieldRect, Label, m_cachedValue );
        }

        protected override bool TestValueEquality( Rect oldval, Rect newval )
        {
            return oldval.Equals( newval );
        }
    }
}