using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Controls.Fields;

namespace UForms.Controls.Dropdowns
{
    public class IntDropdown : AbstractField< int >
    {
        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }        

        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        public int[]    OptionValues    { get; set; }

        public string[] OptionNames     { get; set; }
        
        public IntDropdown( int[] optionValues, string[] optionNames, string label = "", int selection = 0 ) : base( selection, label )
        {
            OptionValues = optionValues;
            OptionNames  = optionNames;
        }

        public IntDropdown( Vector2 position, Vector2 size, int[] optionValues, string[] optionNames, string label = "", int selection = 0 ) : base( position, size, selection, label )
        {
            OptionValues = optionValues;
            OptionNames = optionNames;
        }

        protected override int DrawAndUpdateValue()
        {
            return EditorGUI.IntPopup( m_fieldRect, Label, m_cachedValue, OptionNames, OptionValues );
        }

        protected override bool TestValueEquality( int oldval, int newval )
        {
            return oldval.Equals( newval );
        }
    }
}