using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UForms.Core;

namespace UForms.Controls
{
    public class Control : IDrawable
    {
        public Rect Bounds        
        {
            get             { return m_bounds; }
            private set     { m_bounds = value; } 
        }

        // Screen position are the coordinates relative to the topmost hierarchy element
        public Vector2 ScreenPosition         
        {
            get             { return m_screenPosition; } 
            private set     { m_screenPosition = value; } 
        }

        // Position is used to hint to the control it's desired position on screen.
        public Vector2 Position 
        {
            get { return m_position; }
            set { m_position = value; SetDirty(); } 
        }

        // Size is used to hint to the control it's desired size on screen.
        public Vector2 Size 
        {
            get { return m_size; }
            set { m_size = value; SetDirty(); } 
        }                


        public      List<Control> Children      { get; private set; }        // Contained children elements.               

        protected   bool          m_dirty;                                   // Do we need to do a layout step before drawing?
        protected   Control       m_container;                               // Containing element if control is in a hierarchy.

        private     Rect          m_bounds;
        private     Vector2       m_screenPosition;

        private     Vector2       m_position;
        private     Vector2       m_size;

        #region Internal Drawing Events

        protected virtual void OnBeforeDraw() { }
        protected virtual void OnDraw() { }
        protected virtual void OnLayout() { }

        #endregion

        #region Internal System Events

        protected virtual void OnContextClick( Event e ) { }
        protected virtual void OnDragExited( Event e ) { }
        protected virtual void OnDragPerform( Event e ) { }
        protected virtual void OnDragUpdated( Event e ) { }
        protected virtual void OnExecuteCommand( Event e ) { }
        protected virtual void OnIgnore( Event e ) { }
        protected virtual void OnKeyDown( Event e ) { }
        protected virtual void OnKeyUp( Event e ) { }
        protected virtual void OnLayout( Event e ) { }
        protected virtual void OnMouseDown( Event e ) { }
        protected virtual void OnMouseDrag( Event e ) { }
        protected virtual void OnMouseMove( Event e ) { }
        protected virtual void OnMouseUp( Event e ) { }
        protected virtual void OnRepaint( Event e ) { }
        protected virtual void OnScrollWheel( Event e ) { }
        protected virtual void OnUsed( Event e ) { }
        protected virtual void OnValidateCommand( Event e ) { }

        #endregion


        public Control( Vector2 position, Vector2 size )
        {
            Children = new List<Control>();

            Position = position;
            Size     = size;
        }

        public void AddChild( Control child )
        {
            child.m_container = this;

            Children.Add( child );
            SetDirty();
        }


        public void RemoveChild( Control child )
        {
            if ( child.m_container == this )
            {
                child.m_container = null;

                Children.Remove( child );
                SetDirty();
            }
        }


        public void Draw()
        {
            if ( m_dirty )
            {
                CacheScreenPosition();
            }

            OnBeforeDraw();

            foreach( Control child in Children )
            {
                child.Draw();
            }

            if ( m_dirty )
            {
                Layout();
            }

            OnDraw();

            m_dirty = false;
        }


        public void ProcessEvents( Event e )
        {
            if ( e == null )
            {
                return;
            }

            switch ( e.type )
            {
                case EventType.ContextClick:
                OnContextClick( e );
                break;

                case EventType.DragExited:
                OnDragExited( e );
                break;

                case EventType.DragPerform:
                OnDragPerform( e );
                break;

                case EventType.DragUpdated:
                OnDragUpdated( e );
                break;

                case EventType.ExecuteCommand:
                OnExecuteCommand( e );
                break;

                case EventType.Ignore:
                OnIgnore( e );
                break;

                case EventType.KeyDown:
                OnKeyDown( e );
                break;

                case EventType.KeyUp:
                OnKeyUp( e );
                break;

                case EventType.Layout:
                OnLayout( e );
                break;

                case EventType.MouseDown:
                OnMouseDown( e );
                break;

                case EventType.MouseDrag:
                OnMouseDrag( e );
                break;

                case EventType.MouseMove:
                OnMouseMove( e );
                break;

                case EventType.MouseUp:
                OnMouseUp( e );
                break;

                case EventType.Repaint:
                OnRepaint( e );
                break;

                case EventType.ScrollWheel:
                OnScrollWheel( e );
                break;

                case EventType.Used:
                OnUsed( e );
                break;

                case EventType.ValidateCommand:
                OnValidateCommand( e );
                break;
            }

            foreach( Control child in Children )
            {
                // If event was consumed during propagation, stop processing
                if ( e == null )
                {
                    return;
                }

                child.ProcessEvents( e );
            }
        }


        // Content bounds are handle automatically for most cases, this method can be used to estimate the content bounds before a layout step has been processed on all children
        public Rect GetContentBounds()
        {
            float xmin = 0.0f;
            float xmax = 0.0f;
            float ymin = 0.0f;
            float ymax = 0.0f;

            foreach ( Control child in Children )
            {
                xmin = Mathf.Min( xmin, child.Bounds.xMin );
                xmax = Mathf.Max( xmax, child.Bounds.xMax );
                ymin = Mathf.Min( ymin, child.Bounds.yMin );
                ymax = Mathf.Max( ymax, child.Bounds.yMax );
            }

            return new Rect( xmin, ymin, xmax - xmin, ymax - ymin );
        }


        private void Layout()
        {
            RecalculateBounds();
            OnLayout();

            m_dirty = false;
        }


        private void RecalculateBounds()
        {
            Rect content =  GetContentBounds();

            Bounds = new Rect(                
                Mathf.Min( Position.x + content.xMin, Position.x ),
                Mathf.Min( Position.y + content.yMin, Position.y ),
                Mathf.Max( content.width, Size.x ),
                Mathf.Max( content.height, Size.y )
            );
        }

        
        private void CacheScreenPosition()
        {
            if ( m_container != null )
            {
                ScreenPosition = m_container.ScreenPosition + Position;
            }
            else
            {
                ScreenPosition = Position;
            }
        }


        protected void SetDirty()
        {
            if ( !m_dirty )
            {
                m_dirty = true;

                if ( m_container != null )
                {
                    if ( !m_container.m_dirty )
                    {
                        m_container.SetDirty();
                    }
                }

                foreach ( Control child in Children )
                {
                    if ( !child.m_dirty )
                    {
                        child.SetDirty();
                    }
                }
            }
        }
    }
}