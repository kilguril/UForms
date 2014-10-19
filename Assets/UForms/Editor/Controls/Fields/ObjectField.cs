using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class ObjectField : AbstractField< Object >
    {
        public System.Type          Type              { get; set; }
        public bool                 AllowSceneObjects { get; set; }

        public ObjectField( Vector2 position, Vector2 size, System.Type type, bool allowSceneObjects = false, Object value = null, string label = "" ) : base( position, size, value, label )
        {
            Type                = type;
            AllowSceneObjects   = allowSceneObjects;
        }

        protected override Object DrawAndUpdateValue()
        {
            return EditorGUI.ObjectField( m_fieldRect, Label, m_cachedValue, Type, AllowSceneObjects );
        }

        protected override bool TestValueEquality( Object oldval, Object newval )
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