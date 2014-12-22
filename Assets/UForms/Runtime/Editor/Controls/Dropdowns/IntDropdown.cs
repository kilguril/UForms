using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Controls.Fields;

namespace UForms.Controls.Dropdowns
{
    /// <summary>
    /// 
    /// </summary>
    public class IntDropdown : AbstractField< int >
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
        public int[]    OptionValues    { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string[] OptionNames     { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionValues"></param>
        /// <param name="optionNames"></param>
        /// <param name="label"></param>
        /// <param name="selection"></param>
        public IntDropdown( int[] optionValues, string[] optionNames, string label = "", int selection = 0 ) : base( selection, label )
        {
            OptionValues = optionValues;
            OptionNames  = optionNames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="optionValues"></param>
        /// <param name="optionNames"></param>
        /// <param name="label"></param>
        /// <param name="selection"></param>
        public IntDropdown( Vector2 position, Vector2 size, int[] optionValues, string[] optionNames, string label = "", int selection = 0 ) : base( position, size, selection, label )
        {
            OptionValues = optionValues;
            OptionNames = optionNames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override int DrawAndUpdateValue()
        {
            return EditorGUI.IntPopup( ScreenRect, Label, m_cachedValue, OptionNames, OptionValues );
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