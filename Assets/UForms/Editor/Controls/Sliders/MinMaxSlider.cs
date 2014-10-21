using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Controls.Fields;

namespace UForms.Controls.Sliders
{
    public class MinMaxSlider : AbstractField< float[] >
    {
        protected override bool UseBackingFieldChangeDetection
        {
            get { return false; }
        }        

        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 300.0f, 16.0f ); }
        }

        public float MinLimit    { get; set; }
        public float MaxLimit    { get; set; }

        public float MinValue { get { return Value[ 0 ]; } }
        public float MaxValue { get { return Value[ 1 ]; } }
        
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

        protected override float[] DrawAndUpdateValue()
        {
            EditorGUI.MinMaxSlider( new GUIContent( Label ), m_fieldRect, ref m_cachedValue[ 0 ], ref m_cachedValue[ 1 ], MinLimit, MaxLimit );
            //return EditorGUI.Slider( m_fieldRect, Label, m_cachedValue, LeftValue, RightValue );
            return m_cachedValue;
        }

        protected override bool TestValueEquality( float[] oldval, float[] newval )
        {
            return true;
        }
    }
}