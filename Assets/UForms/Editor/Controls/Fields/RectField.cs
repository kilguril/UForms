using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class RectField : AbstractField< Rect >
    {
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 48.0f ); }
        }

        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        public RectField( Rect value = default(Rect), string label = "" ) : base( value, label )
        {

        }


        public RectField( Vector2 position, Vector2 size, Rect value = default(Rect), string label = "" ) : base( position, size, value, label )
        {

        }


        protected override Rect DrawAndUpdateValue()
        {
            return EditorGUI.RectField( ScreenRect, Label, m_cachedValue );
        }


        protected override bool TestValueEquality( Rect oldval, Rect newval )
        {
            return oldval.Equals( newval );
        }
    }
}