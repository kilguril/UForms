using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    /// <summary>
    /// 
    /// </summary>
    public class EnumMaskField : AbstractField< System.Enum >
    {
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
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public EnumMaskField( System.Enum value, string label = "" ) : base( value, label )
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public EnumMaskField( Vector2 position, Vector2 size, System.Enum value, string label = "" ) : base( position, size, value, label )
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override System.Enum DrawAndUpdateValue()
        {
            if ( m_cachedValue == null )
            {
                // Throw some kind of exception here? this value should always be initialized...
                return null;
            }

            return EditorGUI.EnumMaskField( ScreenRect, Label, m_cachedValue );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
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