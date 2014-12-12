using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;

namespace UForms.Decorators
{
    /// <summary>
    /// Decorators provide a way to enhance a control's functionality in cases where the functionality is not a part of the core control and can be applied to any control.
    /// </summary>
    public class Decorator
    {
        /// <summary>
        /// Reference to the control this decorator is attached to.
        /// </summary>
        protected Control     m_boundControl;

        /// <summary>
        /// Sets the attached control.
        /// </summary>
        /// <param name="control">Reference to the attached control.</param>
        public void SetControl( Control control )
        {
            m_boundControl = control;
        }

        /// <summary>
        /// Implementation of the BeforeDraw step.
        /// </summary>
        public void BeforeDraw()
        {
            if ( m_boundControl != null )
            {
                OnBeforeDraw();
            }
        }

        /// <summary>
        /// Implementation of the Draw step.
        /// </summary>
        public void Draw()
        {
            if ( m_boundControl != null )
            {
                OnDraw();
            }
        }

        /// <summary>
        /// Implementation of the AfterDraw step.
        /// </summary>
        public void AfterDraw()
        {
            if ( m_boundControl != null )
            {
                OnAfterDraw();
            }
        }

        /// <summary>
        /// Implementation of the Layout step.
        /// </summary>
        public void Layout()
        {
            if ( m_boundControl != null )
            {
                OnLayout();
            }
        }

        /// <summary>
        /// Implementation of the AfterLayout step.
        /// </summary>
        public void AfterLayout()
        {
            if ( m_boundControl != null )
            {
                OnAfterLayout();
            }
        }

        /// <summary>
        /// Virtual method for the BeforeDraw step. Override this to add functionality at this step.
        /// </summary>
        protected virtual void OnBeforeDraw() { }

        /// <summary>
        /// Virtual method for the Draw step. Override this to add functionality at this step.
        /// </summary>
        protected virtual void OnDraw() { }

        /// <summary>
        /// Virtual method for the AfterDraw step. Override this to add functionality at this step.
        /// </summary>
        protected virtual void OnAfterDraw() { }

        /// <summary>
        /// Virtual method for the Layout step. Override this to add functionality at this step.
        /// </summary>
        protected virtual void OnLayout() { }

        /// <summary>
        /// Virtual method for the AfterLayout step. Override this to add functionality at this step.
        /// </summary>
        protected virtual void OnAfterLayout() { }
    }
}