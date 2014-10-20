using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class ObjectField : AbstractField< Object >
    {         
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        public System.Type          Type              { get; set; }
        public bool                 AllowSceneObjects { get; set; }


        public ObjectField(  System.Type type, bool allowSceneObjects = false, Object value = null, string label = "" ) : base(  value, label )
        {
            Type = type;
            AllowSceneObjects = allowSceneObjects;
        }


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