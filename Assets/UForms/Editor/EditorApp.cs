using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms
{
    public class EditorApp : EditorWindow
    {
        public View ActiveView { 
            get { return m_activeView; }
            protected set { SetActiveView( value ); }
        }

        private View m_activeView;
        private Rect m_windowRect; // Rect for containing window

        protected virtual void OnGUI()
        {
            UpdateWindowRect();

            if ( ActiveView != null )
            {
                ActiveView.Bounds = m_windowRect;
                ActiveView.ProcessEvents( Event.current );
                ActiveView.Draw();
            }
        }

        private void SetActiveView( View view )
        {
            m_activeView = view;
            m_activeView.Container = null;
            m_activeView.Bounds = m_windowRect;
        }

        private void UpdateWindowRect()
        {
            m_windowRect.Set(
                0,
                0,
                position.width,
                position.height
            );
        }
    }
}