using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Attributes;

namespace UForms.Controls.Fields
{
    /// <summary>
    /// 
    /// </summary>

    [ExposeInDesigner( "Tag Field", "Fields" )]
    public class TagField : AbstractField< string >
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
        public TagField( string value = "", string label = "" ) : base( value, label )
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public TagField( Vector2 position, Vector2 size, string value = "", string label = "" ) : base( position, size, value, label )
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string DrawAndUpdateValue()
        {
            return EditorGUI.TagField( ScreenRect, Label, m_cachedValue );
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