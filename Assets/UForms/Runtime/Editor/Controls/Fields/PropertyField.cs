using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    /// <summary>
    /// 
    /// </summary>
    public class PropertyField : AbstractField< SerializedProperty >
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
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IncludeChildren { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="label"></param>
        /// <param name="includeChildren"></param>
        public PropertyField(  SerializedProperty value, string label = "", bool includeChildren = false ) : base(  value, label )
        {
            IncludeChildren = includeChildren;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="label"></param>
        /// <param name="includeChildren"></param>
        public PropertyField( Vector2 position, Vector2 size, SerializedProperty value, string label = "", bool includeChildren = false ) : base( position, size, value, label )
        {
            IncludeChildren = includeChildren;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override SerializedProperty DrawAndUpdateValue()
        {
            IncludeChildren = EditorGUI.PropertyField( ScreenRect, m_cachedValue, new GUIContent( Label ), IncludeChildren );
            return m_cachedValue;
        }


        /// <summary>
        /// 
        /// </summary>
        protected override void OnLayout()
        {
            Size.Set(
                Size.x,
                EditorGUI.GetPropertyHeight( Value, new GUIContent( Label ), IncludeChildren )
            );

            base.OnLayout();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
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