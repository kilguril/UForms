using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Attributes;
using UForms.Controls.Fields;

namespace UForms.Controls.Sliders
{
    /// <summary>
    /// 
    /// </summary>

    [ExposeInDesigner( "Float Slider", "Sliders" )]
    public class FloatSlider : AbstractField< float >
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
        public float LeftValue    { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float RightValue     { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="leftValue"></param>
        /// <param name="rightValue"></param>
        /// <param name="label"></param>
        /// <param name="value"></param>
        public FloatSlider( float leftValue = 0, float rightValue = 0, string label = "", float value = 0 ) : base( value, label )
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
        public FloatSlider( Vector2 position, Vector2 size, float leftValue = 0, float rightValue = 0, string label = "", float value = 0 ) : base( position, size, value, label )
        {
            LeftValue = leftValue;
            RightValue = rightValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override float DrawAndUpdateValue()
        {
            return EditorGUI.Slider( ScreenRect, Label, m_cachedValue, LeftValue, RightValue );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
        protected override bool TestValueEquality( float oldval, float newval )
        {
            return oldval.Equals( newval );
        }
    }
}