using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    /// <summary>
    /// 
    /// </summary>
    public class RectField : AbstractField< Rect >
    {
        /// <summary>
        /// 
        /// </summary>
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 48.0f ); }
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
        public RectField( Rect value = default(Rect), string label = "" ) : base( value, label )
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public RectField( Vector2 position, Vector2 size, Rect value = default(Rect), string label = "" ) : base( position, size, value, label )
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Rect DrawAndUpdateValue()
        {
            return EditorGUI.RectField( ScreenRect, Label, m_cachedValue );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
        protected override bool TestValueEquality( Rect oldval, Rect newval )
        {
            return oldval.Equals( newval );
        }
    }
}