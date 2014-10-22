using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Controls.Fields;

namespace UForms.Controls
{
    public class TextArea : AbstractField< string >
    {
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 64.0f ); }
        }

        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        public TextArea( string value = "" ) : base( value, null )
        {
        }

        public TextArea( Vector2 position, Vector2 size, string value = "" ) : base( position, size, value, null )
        {
        }

        protected override string DrawAndUpdateValue()
        {
            return EditorGUI.TextArea( ScreenRect, m_cachedValue );
        }

        protected override bool TestValueEquality( string oldval, string newval )
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