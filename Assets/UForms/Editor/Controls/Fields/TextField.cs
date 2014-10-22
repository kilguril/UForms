using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class TextField : AbstractField< string >
    {
        public bool PasswordMask { get; set; }

        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        public TextField( string value = "", string label = "", bool passwordMask = false ) : base( value, label )
        {
            PasswordMask = passwordMask;
        }

        public TextField( Vector2 position, Vector2 size, string value = "", string label = "", bool passwordMask = false ) : base( position, size, value, label )
        {
            PasswordMask = passwordMask;
        }

        protected override string DrawAndUpdateValue()
        {
            if ( PasswordMask )
            {
                return EditorGUI.PasswordField( ScreenRect, Label, m_cachedValue );
            }
            else
            {
                return EditorGUI.TextField( ScreenRect, Label, m_cachedValue );
            }
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