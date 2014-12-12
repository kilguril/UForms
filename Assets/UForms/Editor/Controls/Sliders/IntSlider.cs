using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Controls.Fields;

namespace UForms.Controls.Sliders
{
    /// <summary>
    /// 
    /// </summary>
    public class IntSlider : AbstractField< int >
    {
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
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 300.0f, 16.0f ); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int LeftValue    { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RightValue   { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="leftValue"></param>
        /// <param name="rightValue"></param>
        /// <param name="label"></param>
        /// <param name="value"></param>
        public IntSlider( int leftValue = 0, int rightValue = 0, string label = "", int value = 0 ) : base( value, label )
        {
            LeftValue   = leftValue;
            RightValue  = rightValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="leftValue"></param>
        /// <param name="rightValue"></param>
        /// <param name="label"></param>
        /// <param name="value"></param>
        public IntSlider( Vector2 position, Vector2 size, int leftValue = 0, int rightValue = 0, string label = "", int value = 0 ) : base( position, size, value, label )
        {
            LeftValue = leftValue;
            RightValue = rightValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override int DrawAndUpdateValue()
        {
            return EditorGUI.IntSlider( ScreenRect, Label, m_cachedValue, LeftValue, RightValue );
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