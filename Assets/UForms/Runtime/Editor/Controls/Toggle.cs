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

    [ExposeControl( "Toggle", "General" )]
    public class Toggle : AbstractField< bool >
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
        public bool LabelOnRight { get; set; }


        public Toggle() : base()
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="value"></param>
        /// <param name="labelOnRight"></param>
        public Toggle( string label = "", bool value = false, bool labelOnRight = true ) : base( value, label )
        {
            LabelOnRight = labelOnRight;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="label"></param>
        /// <param name="value"></param>
        /// <param name="labelOnRight"></param>
        public Toggle( Vector2 position, Vector2 size, string label = "", bool value = false, bool labelOnRight = true ) : base( position, size, value, label )
        {
            LabelOnRight = labelOnRight;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override bool DrawAndUpdateValue()
        {            
            if ( LabelOnRight )
            {
                return EditorGUI.ToggleLeft( ScreenRect, Label, m_cachedValue );
            }
            else
            {
                return EditorGUI.Toggle( ScreenRect, Label, m_cachedValue );
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
        protected override bool TestValueEquality( bool oldval, bool newval )
        {
            return oldval.Equals( newval );
        }


    }
}