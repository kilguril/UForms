using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Controls.Fields;

namespace UForms.Controls.Dropdowns
{
    public class EnumDropdown : AbstractField< System.Enum >
    {
        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }            
        }

        public EnumDropdown( System.Enum value, string label = "" ) : base( value, label )
        {

        }

        public EnumDropdown( Vector2 position, Vector2 size, System.Enum value, string label = "" ) : base( position, size, value, label )
        {

        }

        protected override System.Enum DrawAndUpdateValue()
        {
            return EditorGUI.EnumPopup( m_fieldRect, Label, m_cachedValue );
        }

        protected override bool TestValueEquality( System.Enum oldval, System.Enum newval )
        {
            return oldval.Equals( newval );
        }
    }
}