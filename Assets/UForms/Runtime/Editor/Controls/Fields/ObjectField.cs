using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Attributes;

namespace UForms.Controls.Fields
{
    /// <summary>
    /// 
    /// </summary>

    [ExposeControl( "Object Field", "Fields" )]
    public class ObjectField : AbstractField< Object >
    {   
        /// <summary>
        /// 
        /// </summary>
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override bool UseBackingFieldChangeDetection
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Type          Type              { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool                 AllowSceneObjects { get; set; }


        public ObjectField() : base ()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="allowSceneObjects"></param>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public ObjectField(  System.Type type, bool allowSceneObjects = false, Object value = null, string label = "" ) : base(  value, label )
        {
            Type = type;
            AllowSceneObjects = allowSceneObjects;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="type"></param>
        /// <param name="allowSceneObjects"></param>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public ObjectField( Vector2 position, Vector2 size, System.Type type, bool allowSceneObjects = false, Object value = null, string label = "" ) : base( position, size, value, label )
        {
            Type                = type;
            AllowSceneObjects   = allowSceneObjects;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Object DrawAndUpdateValue()
        {
            return EditorGUI.ObjectField( ScreenRect, Label, m_cachedValue, Type, AllowSceneObjects );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
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