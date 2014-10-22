using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Controls.Fields;

namespace UForms.Controls
{
    public class Toggle : AbstractField< bool >
    {
        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        public bool LabelOnRight { get; set; }

        public Toggle( string label = "", bool value = false, bool labelOnRight = true ) : base( value, label )
        {
            LabelOnRight = labelOnRight;
        }

        public Toggle( Vector2 position, Vector2 size, string label = "", bool value = false, bool labelOnRight = true ) : base( position, size, value, label )
        {
            LabelOnRight = labelOnRight;
        }

        protected override bool DrawAndUpdateValue()
        {            
            if ( LabelOnRight )
            {
                return EditorGUI.ToggleLeft( ScreenRect, Label, m_cachedValue );
            }
            else
            {
                return EditorGUI.Toggle( ScreenRect, Label, m_cachedValue );
            }            
        }

        protected override bool TestValueEquality( bool oldval, bool newval )
        {
            return oldval.Equals( newval );
        }


    }
}