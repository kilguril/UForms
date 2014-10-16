using UnityEngine;
using System.Collections;

namespace UForms
{
    public abstract class Element : IDrawable
    {
        public Element Container { get; set; }

        public Rect ScreenBounds
        {
            get
            {
                if ( Container == null )
                {
                    return m_bounds;
                }

                Rect r = Container.ScreenBounds;

                return new Rect
                (
                    r.xMin + m_bounds.xMin,
                    r.yMin + m_bounds.yMin,
                    m_bounds.width,
                    m_bounds.height
                );
            }
        }


        public Rect Bounds
        {
            get
            {
                return m_bounds;
            }

            set
            {
                m_bounds = value;
                OnBoundsModified();
            }
        }

        private Rect m_bounds;

        public Element( Rect bounds = new Rect() )
        {

        }

        public abstract void Draw();


        #region Internal Events

        protected virtual void OnBoundsModified() { }

        #endregion

        #region Incoming Events

        public virtual void ProcessEvents( Event e )
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
        }

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
    }
}