using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class PropertyField : AbstractField< SerializedProperty >
    {         
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        protected override bool UseBackingFieldChangeDetection
        {
            get { return false; }
        }

        public bool IncludeChildren { get; set; }

        public PropertyField(  SerializedProperty value, string label = "", bool includeChildren = false ) : base(  value, label )
        {
            IncludeChildren = includeChildren;
        }


        public PropertyField( Vector2 position, Vector2 size, SerializedProperty value, string label = "", bool includeChildren = false ) : base( position, size, value, label )
        {
            IncludeChildren = includeChildren;
        }

        protected override SerializedProperty DrawAndUpdateValue()
        {
            IncludeChildren = EditorGUI.PropertyField( ScreenRect, m_cachedValue, new GUIContent( Label ), IncludeChildren );
            return m_cachedValue;
        }

        protected override void OnLayout()
        {
            Size.Set(
                Size.x,
                EditorGUI.GetPropertyHeight( Value, new GUIContent( Label ), IncludeChildren )
            );

            base.OnLayout();
        }

        protected override bool TestValueEquality( SerializedProperty oldval, SerializedProperty newval )
        {
            if ( oldval == null || newval == null )
            {
                if ( oldval == null && newval == null )
                {
                    return true;
                }

                return false;
            }

            return oldval.Equals( newval );
        }
    }
}