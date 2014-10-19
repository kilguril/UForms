using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class LabelField : AbstractField< string >
    {
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