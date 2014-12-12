using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Controls.Fields;

namespace UForms.Controls.Dropdowns
{
    /// <summary>
    /// 
    /// </summary>
    public class Dropdown : AbstractField< int >
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
        public string[] OptionNames     { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionNames"></param>
        /// <param name="label"></param>
        /// <param name="selection"></param>
        public Dropdown( string[] optionNames, string label = "", int selection = 0 ) : base( selection, label )
        {
            OptionNames = optionNames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="optionNames"></param>
        /// <param name="label"></param>
        /// <param name="selection"></param>
        public Dropdown( Vector2 position, Vector2 size, string[] optionNames, string label = "", int selection = 0 ) : base( position, size, selection, label )
        {
            OptionNames = optionNames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override int DrawAndUpdateValue()
        {
            return EditorGUI.Popup( ScreenRect, Label, m_cachedValue, OptionNames );
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