using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class EnumMaskField : AbstractField< System.Enum >
    {
        public EnumMaskField( Vector4 position, Vector4 size, System.Enum value, string label = "" ) : base( position, size, value, label )
        {

        }

        protected override System.Enum DrawAndUpdateValue()
        {
            if ( m_cachedValue == null )
            {
                // Throw some kind of exception here? this value should always be initialized...
                return null;
            }

            return EditorGUI.EnumMaskField( m_fieldRect, Label, m_cachedValue );
        }

        protected override bool TestValueEquality( System.Enum oldval, System.Enum newval )
        {
            if ( oldval == null || newval == null )
            {
                if ( oldval == null && newval == null )
                {
                    return true;
                }

                return false;
            }

            return oldval.Equals( newval );
        }
    }
}