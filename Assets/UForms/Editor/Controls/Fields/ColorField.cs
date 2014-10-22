using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class ColorField : AbstractField< Color >
    {
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        public ColorField( Color value = default(Color), string label = "" ) : base( value, label )
        {
            
        }

        public ColorField( Vector2 position, Vector2 size, Color value = default(Color), string label = "" ) : base( position, size, value, label )
        {

        }

        protected override Color DrawAndUpdateValue()
        {            
            return EditorGUI.ColorField( ScreenRect, Label, m_cachedValue );
        }

        protected override bool TestValueEquality( Color oldval, Color newval )
        {
            return oldval.Equals( newval );
        }
    }
}