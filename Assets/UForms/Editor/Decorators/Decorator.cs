using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;

namespace UForms.Decorators
{
    public class Decorator
    {
        protected Control     m_boundControl;

        public void SetControl( Control control )
        {
            m_boundControl = control;
        }

        public void BeforeDraw()
        {
            if ( m_boundControl != null )
            {
                OnBeforeDraw();
            }
        }

        public void Draw()
        {
            if ( m_boundControl != null )
            {
                OnDraw();
            }
        }

        public void AfterDraw()
        {
            if ( m_boundControl != null )
            {
                OnAfterDraw();
            }
        }

        public void Layout()
        {
            if ( m_boundControl != null )
            {
                OnLayout();
            }
        }

        public void AfterLayout()
        {
            if ( m_boundControl != null )
            {
                OnAfterLayout();
            }
        }

        protected virtual void OnBeforeDraw() { }
        protected virtual void OnDraw() { }
        protected virtual void OnAfterDraw() { }
        protected virtual void OnLayout() { }
        protected virtual void OnAfterLayout() { }
    }
}