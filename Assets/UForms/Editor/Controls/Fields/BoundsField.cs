using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class BoundsField : AbstractField< Bounds >
    {
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 48.0f ); }
        }

        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        public BoundsField( Bounds value = default(Bounds), string label = "" ) : base( value, label )
        {
            
        }
        

        public BoundsField( Vector2 position, Vector2 size, Bounds value = default(Bounds), string label = "" ) : base( position, size, value, label )
        {
            
        }

        protected override Bounds DrawAndUpdateValue()
        {
            return EditorGUI.BoundsField( ScreenRect, new GUIContent( Label ), m_cachedValue );
        }

        protected override bool TestValueEquality( Bounds oldval, Bounds newval )
        {
            return oldval.Equals( newval );
        }

        
    }
}