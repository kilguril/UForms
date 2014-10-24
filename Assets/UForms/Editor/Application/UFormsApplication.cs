using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Controls;

namespace UForms.Application
{
    public class UFormsApplication : EditorWindow
    {
        private Vector2   m_cachedScreenSize;
        private Control   m_rootObject;

        public void AddControl( Control control )
        {
            if ( m_rootObject != null )
            {
                m_rootObject.AddChild( control );
            }
        }

        public void RemoveControl( Control control )
        {
            if ( m_rootObject != null )
            {
                m_rootObject.RemoveChild( control );
            }
        }

        protected virtual void OnInitialize() { }

        protected virtual void OnGUI()
        {
            if ( m_rootObject == null )
            {
                Initialize();
            }

            if ( position.width != m_cachedScreenSize.x || position.height != m_cachedScreenSize.y )
            {
                m_cachedScreenSize.x = position.width;
                m_cachedScreenSize.y = position.height;

                if ( m_rootObject != null )
                {
                    m_rootObject.Size = m_cachedScreenSize;
                }
            }

            if ( m_rootObject != null )
            {                                
                m_rootObject.Layout();
                m_rootObject.Draw();
                m_rootObject.ProcessEvents( Event.current );               
            }            
        }   

        protected virtual void Update()
        {
            if ( m_rootObject != null )
            {
                if ( m_rootObject.Dirty )
                {
                    Repaint();
                }
            }
        }

        private void Initialize()
        {            
            m_cachedScreenSize = new Vector2( position.width, position.height );
            m_rootObject = new Control( Vector2.zero, m_cachedScreenSize );

            OnInitialize();
        }
    }
}