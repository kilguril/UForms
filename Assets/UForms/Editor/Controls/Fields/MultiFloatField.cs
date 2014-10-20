using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public class MultiFloatField : AbstractField< float[] >
    {
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 32.0f ); }
        }

        protected override bool UseBackingFieldChangeDetection
        {
            get { return false; }
        }

        public string[] SubLabels
        {
            get { return m_sublabels; }
            set { m_sublabels = value; GenerateGUIContent(); }
        }

        private string[]     m_sublabels;
        private GUIContent[] m_sublabelGuiContent;

        public MultiFloatField( float[] value, string[] sublabels, string label = "" ) : base( value, label )
        {
            SubLabels = sublabels;
        }

        public MultiFloatField( Vector2 position, Vector2 size, float[] value, string[] sublabels, string label = "" ) : base( position, size, value, label )
        {
            SubLabels = sublabels;
        }

        protected override float[] DrawAndUpdateValue()
        {
            EditorGUI.MultiFloatField( m_fieldRect, new GUIContent( Label ), m_sublabelGuiContent, m_cachedValue );
            return m_cachedValue;
        }

        protected override bool TestValueEquality( float[] oldval, float[] newval )
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

        private void GenerateGUIContent()
        {
            if ( m_sublabels == null )
            {
                m_sublabelGuiContent = null;
                return;
            }

            m_sublabelGuiContent = new GUIContent[ m_sublabels.Length ];

            for ( int i = 0; i < m_sublabelGuiContent.Length; i++ )
            {
                m_sublabelGuiContent[ i ] = new GUIContent( m_sublabels[ i ] );
            }
        }
    }
}