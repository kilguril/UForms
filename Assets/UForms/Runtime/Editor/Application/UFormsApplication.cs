using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Controls;

namespace UForms.Application
{
    /// <summary>
    /// <c>UFormsApplication</c> defines a basic editor application. 
    /// Extend this class with your own functionality.
    /// </summary>
    public class UFormsApplication : EditorWindow
    {
        /// <summary>
        /// OnGUI call frame counter, for internal use.
        /// </summary>
        public uint       Frame { get { return m_frame; } }

        private Vector2   m_cachedScreenSize;
        private Control   m_rootObject;

        private uint      m_frame;

        /// <summary>
        /// Adds a child to the control list.
        /// </summary>
        /// <param name="control">Control object to add.</param>
        /// <returns>The method returns the control that was added.</returns>
        public Control AddChild( Control control )
        {
            if ( m_rootObject != null )
            {
                m_rootObject.AddChild( control );
            }

            return control;
        }

        /// <summary>
        /// Removes a child from the control list.
        /// </summary>
        /// <param name="control">Control object to remove.</param>
        /// <returns>The method returns the control that was removed.</returns>
        public Control RemoveChild( Control control )
        {
            if ( m_rootObject != null )
            {
                m_rootObject.RemoveChild( control );
            }

            return control;
        }

        /// <summary>
        /// Override this method to provide additional functionality on initialization
        /// </summary>
        protected virtual void OnInitialize() { }

        /// <summary>
        /// Standard Unity OnGUI call. Make sure to call <c>base.OnGUI()</c> when overriding this method.
        /// </summary>
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

            m_frame++;
        }

        /// <summary>
        /// Standard Unity Update call. Make sure to call <c>base.Update()</c> when overriding this method.
        /// </summary>
        protected virtual void Update()
        {
            if ( m_rootObject != null )
            {
                if ( m_rootObject.Dirty )
                {                    
                    Repaint();
                }

                m_rootObject.Update();
            }
        }

        private void Initialize()
        {
            m_frame = 0;

            m_cachedScreenSize = new Vector2( position.width, position.height );
            m_rootObject = new Control( Vector2.zero, m_cachedScreenSize );
            m_rootObject.SetApplicationContext( this );

            OnInitialize();
        }
    }
}