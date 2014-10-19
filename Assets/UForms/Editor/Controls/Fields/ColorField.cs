using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class ColorField : AbstractField< Color >
    {
        public ColorField( Vector2 position, Vector2 size, Color value = default(Color), string label = "" ) : base( position, size, value, label )
        {

        }

        protected override Color DrawAndUpdateValue()
        {            
            return EditorGUI.ColorField( m_fieldRect, Label, m_cachedValue );
        }

        protected override bool TestValueEquality( Color oldval, Color newval )
        {
            return oldval.Equals( newval );
        }
    }
}