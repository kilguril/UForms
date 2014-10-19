using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class LabelField : AbstractField< string >
    {
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        public LabelField( string value = "", string label = "" ) : base( value, label )
        {
            
        }

        public LabelField( Vector2 position, Vector2 size, string value = "", string label = "" ) : base( position, size, value, label )
        {

        }

        protected override string DrawAndUpdateValue()
        {
            // Not sure why this is a field by Unity's definition...
            EditorGUI.LabelField( m_fieldRect, Label, m_cachedValue );
            return Value;
        }

        protected override bool TestValueEquality( string oldval, string newval )
        {
            return true;    // Never going to change!
        }
    }
}