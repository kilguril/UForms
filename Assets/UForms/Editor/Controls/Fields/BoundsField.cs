using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    /// <summary>
    /// 
    /// </summary>
    public class BoundsField : AbstractField< Bounds >
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
        public BoundsField( Bounds value = default(Bounds), string label = "" ) : base( value, label )
        {
            
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public BoundsField( Vector2 position, Vector2 size, Bounds value = default(Bounds), string label = "" ) : base( position, size, value, label )
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Bounds DrawAndUpdateValue()
        {
            return EditorGUI.BoundsField( ScreenRect, new GUIContent( Label ), m_cachedValue );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
        protected override bool TestValueEquality( Bounds oldval, Bounds newval )
        {
            return oldval.Equals( newval );
        }

        
    }
}