using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Controls.Fields;

namespace UForms.Controls
{
    public class Foldout : AbstractField< bool >
    {
        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        public bool ToggleOnLabelClick { get; set; }

        public Foldout( string label = "", bool unfolded = false, bool toggleOnLabelClick = true ) : base( unfolded, label )
        {
            ToggleOnLabelClick = toggleOnLabelClick;
        }

        public Foldout( Vector2 position, Vector2 size, string label = "", bool unfolded = false, bool toggleOnLabelClick = true ) : base( position, size, unfolded, label )
        {
            ToggleOnLabelClick = toggleOnLabelClick;
        }

        protected override bool DrawAndUpdateValue()
        {
            return EditorGUI.Foldout( ScreenRect, m_cachedValue, Label, ToggleOnLabelClick );            
        }

        protected override bool TestValueEquality( bool oldval, bool newval )
        {
            return oldval.Equals( newval );
        }


    }
}