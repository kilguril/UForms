using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Attributes;

namespace UForms.Controls.Fields
{
    /// <summary>
    /// 
    /// </summary>

    [ExposeInDesigner( "Int Field", "Fields" )]
    public class IntField : AbstractField< int >
    {
        /// <summary>
        /// 
        /// </summary>
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 100.0f, 16.0f ); }
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
        public IntField( int value = 0, string label = "" ) : base( value, label )
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public IntField( Vector2 position, Vector2 size, int value = 0, string label = "" ) : base( position, size, value, label )
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override int DrawAndUpdateValue()
        {
            return EditorGUI.IntField( ScreenRect, Label, m_cachedValue );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
        protected override bool TestValueEquality( int oldval, int newval )
        {
            return oldval.Equals( newval );
        }
    }
}