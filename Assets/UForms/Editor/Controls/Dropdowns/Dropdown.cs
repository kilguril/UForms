using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Controls.Fields;

namespace UForms.Controls.Dropdowns
{
    public class Dropdown : AbstractField< int >
    {
        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }            
        }

        public string[] OptionNames     { get; set; }
        
        public Dropdown( string[] optionNames, string label = "", int selection = 0 ) : base( selection, label )
        {
            OptionNames = optionNames;
        }

        public Dropdown( Vector2 position, Vector2 size, string[] optionNames, string label = "", int selection = 0 ) : base( position, size, selection, label )
        {
            OptionNames = optionNames;
        }

        protected override int DrawAndUpdateValue()
        {
            return EditorGUI.Popup( m_fieldRect, Label, m_cachedValue, OptionNames );
        }

        protected override bool TestValueEquality( int oldval, int newval )
        {
            return oldval.Equals( newval );
        }


    }
}