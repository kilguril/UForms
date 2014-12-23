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
    /// 
    [ExposeControl( "Foldout", "General" )]
    public class Foldout : AbstractField< bool >
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
        public bool ToggleOnLabelClick { get; set; }


        public Foldout() : base()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="unfolded"></param>
        /// <param name="toggleOnLabelClick"></param>
        public Foldout( string label = "", bool unfolded = false, bool toggleOnLabelClick = true ) : base( unfolded, label )
        {
            ToggleOnLabelClick = toggleOnLabelClick;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="label"></param>
        /// <param name="unfolded"></param>
        /// <param name="toggleOnLabelClick"></param>
        public Foldout( Vector2 position, Vector2 size, string label = "", bool unfolded = false, bool toggleOnLabelClick = true ) : base( position, size, unfolded, label )
        {
            ToggleOnLabelClick = toggleOnLabelClick;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override bool DrawAndUpdateValue()
        {
            return EditorGUI.Foldout( ScreenRect, m_cachedValue, Label, ToggleOnLabelClick );            
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