using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Attributes;

namespace UForms.Controls.Fields
{
    /// <summary>
    /// 
    /// </summary>
    public class MaskField : AbstractField<int>
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
        public List<string> Options { get; private set; }


        public MaskField() : base() { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <param name="label"></param>
        public MaskField( int value, string[] options = default(string[]), string label = "" ) : base( value, label )
        {
            Options = new List<string>();

            if ( options != null )
            {
                Options.AddRange( options );
            }           
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <param name="label"></param>
        public MaskField( Vector2 position, Vector2 size, int value = 0, string[] options = default(string[]), string label = "" ) : base( position, size, value, label )
        {
            Options = new List<string>();

            if ( options != null )
            {
                Options.AddRange( options );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override int DrawAndUpdateValue()
        {            
            return EditorGUI.MaskField( ScreenRect, Label, m_cachedValue, Options.ToArray() );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
        protected override bool TestValueEquality( int oldval, int newval )
        {            
            return oldval.Equals( newval );
        }
    }
}