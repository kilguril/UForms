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

    [ExposeInDesigner( "MinMax Slider", "Sliders" )]
    public class MinMaxSlider : AbstractField< float[] >
    {
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
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 300.0f, 16.0f ); }
        }

        /// <summary>
        /// 
        /// </summary>
        public float MinLimit    { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float MaxLimit    { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float MinValue { get { return Value[ 0 ]; } }

        /// <summary>
        /// 
        /// </summary>
        public float MaxValue { get { return Value[ 1 ]; } }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minLimit"></param>
        /// <param name="maxLimit"></param>
        /// <param name="minvalue"></param>
        /// <param name="maxValue"></param>
        /// <param name="label"></param>
        public MinMaxSlider( float minLimit = 0, float maxLimit = 0, float minvalue = 0, float maxValue = 0, string label = "" ) : base( null, label )
        {
            m_cachedValue = new float[ 2 ]
            {
                minvalue,
                maxValue
            };

            MinLimit   = minLimit;
            MaxLimit  = maxLimit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="minLimit"></param>
        /// <param name="maxLimit"></param>
        /// <param name="minvalue"></param>
        /// <param name="maxValue"></param>
        /// <param name="label"></param>
        public MinMaxSlider( Vector2 position, Vector2 size, float minLimit = 0, float maxLimit = 0, float minvalue = 0, float maxValue = 0, string label = "" ) : base( position, size, null, label )
        {
            m_cachedValue = new float[ 2 ]
            {
                minvalue,
                maxValue
            };

            MinLimit = minLimit;
            MaxLimit = maxLimit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override float[] DrawAndUpdateValue()
        {
            EditorGUI.MinMaxSlider( new GUIContent( Label ), ScreenRect, ref m_cachedValue[ 0 ], ref m_cachedValue[ 1 ], MinLimit, MaxLimit );
            //return EditorGUI.Slider( ScreenRect, Label, m_cachedValue, LeftValue, RightValue );
            return m_cachedValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
        protected override bool TestValueEquality( float[] oldval, float[] newval )
        {
            return true;
        }
    }
}