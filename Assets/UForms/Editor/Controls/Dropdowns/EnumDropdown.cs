using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Controls.Fields;

namespace UForms.Controls.Dropdowns
{
    /// <summary>
    /// 
    /// </summary>
    public class EnumDropdown : AbstractField< System.Enum >
    {
        /// <summary>
        /// 
        /// </summary>
        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

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
        /// <param name="value"></param>
        /// <param name="label"></param>
        public EnumDropdown( System.Enum value, string label = "" ) : base( value, label )
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public EnumDropdown( Vector2 position, Vector2 size, System.Enum value, string label = "" ) : base( position, size, value, label )
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override System.Enum DrawAndUpdateValue()
        {
            return EditorGUI.EnumPopup( ScreenRect, Label, m_cachedValue );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
        protected override bool TestValueEquality( System.Enum oldval, System.Enum newval )
        {
            return oldval.Equals( newval );
        }
    }
}