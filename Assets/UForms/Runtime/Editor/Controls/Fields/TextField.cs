using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Attributes;

namespace UForms.Controls.Fields
{
    /// <summary>
    /// 
    /// </summary>

    [ExposeControl( "Text Field", "Fields" )]
    public class TextField : AbstractField< string >
    {
        /// <summary>
        /// 
        /// </summary>
        public bool PasswordMask { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        public TextField() : base() { }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="label"></param>
        /// <param name="passwordMask"></param>
        public TextField( string value, string label, bool passwordMask = false ) : base( value, label )
        {
            PasswordMask = passwordMask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="label"></param>
        /// <param name="passwordMask"></param>
        public TextField( Vector2 position, Vector2 size, string value = "", string label = "", bool passwordMask = false ) : base( position, size, value, label )
        {
            PasswordMask = passwordMask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
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