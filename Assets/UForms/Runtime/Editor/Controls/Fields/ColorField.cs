using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Attributes;

namespace UForms.Controls.Fields
{
    /// <summary>
    /// 
    /// </summary>

    [ExposeInDesigner( "Color Field", "Fields" )]
    public class ColorField : AbstractField< Color >
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
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public ColorField( Color value = default(Color), string label = "" ) : base( value, label )
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public ColorField( Vector2 position, Vector2 size, Color value = default(Color), string label = "" ) : base( position, size, value, label )
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Color DrawAndUpdateValue()
        {            
            return EditorGUI.ColorField( ScreenRect, Label, m_cachedValue );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
        protected override bool TestValueEquality( Color oldval, Color newval )
        {
            return oldval.Equals( newval );
        }
    }
}