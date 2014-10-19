using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class EnumMaskField : AbstractField< System.Enum >
    {
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        public EnumMaskField( System.Enum value, string label = "" ) : base( value, label )
        {
            
        }


        public EnumMaskField( Vector2 position, Vector2 size, System.Enum value, string label = "" ) : base( position, size, value, label )
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