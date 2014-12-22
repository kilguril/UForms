using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Attributes;
using UForms.Controls.Fields;

namespace UForms.Controls
{
    /// <summary>
    /// 
    /// </summary>

    [ExposeInDesigner( "Text Area", "General" )]
    public class TextArea : AbstractField< string >
    {
        /// <summary>
        /// 
        /// </summary>
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 64.0f ); }
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
        public TextArea( string value = "" ) : base( value, null )
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        public TextArea( Vector2 position, Vector2 size, string value = "" ) : base( position, size, value, null )
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string DrawAndUpdateValue()
        {
            return EditorGUI.TextArea( ScreenRect, m_cachedValue );
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