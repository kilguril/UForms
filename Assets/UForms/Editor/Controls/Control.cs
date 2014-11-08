// Uncomment this to visually debug control screen rects
//#define UFORMS_DEBUG_RECTS

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Decorators;
using UForms.Core;
using UForms.Application;

namespace UForms.Controls
{
    public class Control : IDrawable
    {
        public enum VisibilityMode
        {
            Visible,
            Hidden,
            Collapsed
        }

        public enum MetricsUnits
        {
            Pixel,
            Percentage
        }

        // Dirty flag should be used to trigger a repaint on internal component changes, as otherwise repaint will only be invoked by specific editor events
        // flag will propagate upwards and will be collected by the application from the root component if it reaches it.
        public bool Dirty
        {
            get { return m_dirty; }
            set 
            {
                if ( m_container == null )
                {
                    m_dirty = value;
                    if ( value && m_application != null )
                    {
                        m_dirtyFrame = m_application.Frame;
                    }
                }
                else
                {
                    if ( value )
                    {
                        m_container.Dirty = true;
                    }
                }
            }
        }

        private uint m_dirtyFrame;

        public Rect Bounds        
        {
            get             { return m_bounds; }
            private set     { m_bounds = value; } 
        }

        // Final computed screen rect taking into account screen position, margins and size
        public Rect ScreenRect
        {
            get         { return m_screenRect; }
            private set { m_screenRect = value; }
        }


        // Since click coordinates are based on the current viewport, we will need to know the viewport offset in case of scrolling
        public Vector2 ViewportOffset
        {
            get         { return m_container == null ? m_viewportOffset : m_container.ViewportOffset + m_viewportOffset; }
            set         { m_viewportOffset = value; }
        }


        // Cached parent's screen position so deep elements don't have to traverse all the way up
        public Vector2 ParentScreenPosition         
        {
            get             { return m_screenPosition; } 
            set             { m_screenPosition = value; } 
        }

        // Position is used to hint to the control it's desired position on screen.
        public Vector2 Position 
        {
            get { return m_position; }
            set { m_position = value; } 
        }

        public MetricsUnits PositionXUnits
        {
            get;
            set;
        }

        public MetricsUnits PositionYUnits
        {
            get;
            set;
        }

        // Size is used to hint to the control it's desired size on screen.
        public Vector2 Size 
        {
            get { return m_size; }
            set { m_size = value; } 
        }

        public MetricsUnits WidthUnits
        {
            get; set;
        }

        public MetricsUnits HeightUnits
        {
            get;
            set;
        }
     
        public Vector2 MarginLeftTop
        {
            get { return m_marginLeftTop; }
            set { m_marginLeftTop = value; }
        }

        public Vector2 MarginRightBottom
        {
            get { return m_marginRightBottom; }
            set { m_marginRightBottom = value; }
        }

        public MetricsUnits MarginLeftUnits
        {
            get;
            set;
        }

        public MetricsUnits MarginRightUnits
        {
            get;
            set;
        }

        public MetricsUnits MarginTopUnits
        {
            get;
            set;
        }

        public MetricsUnits MarginBottomUnits
        {
            get;
            set;
        }

        // Is this control enabled? this property will propagate to all child contorls and can be applied to interactive controls as well as containers
        public bool Enabled
        {
            get;
            set;
        }

        // What is the visibility mode of the control?
        // Visible = default
        // Hidden = layout control and reserve space but don't draw
        // Collapsed = don't layout, generate empty rect and don't draw
        public VisibilityMode    Visibility
        {
            get { return m_visibility; }
            set { m_visibility = value; }
        }

        // Panels should override this property to specify they reset the pivot offset to 0,0
        public bool ResetPivotRoot
        {
            get;
            set;
        }

        // Default size for this control
        protected virtual Vector2 DefaultSize
        {
            get { return Vector2.zero; }
        }


        public      List<Control>     Children      { get; private set; }        // Contained children elements.               
        public      List<Decorator>   Decorators { get; private set; }


        protected   Control           m_container;                               // Containing element if control is in a hierarchy.

        private     Rect              m_bounds;
        private     Rect              m_screenRect;

        private     Vector2           m_screenPosition;
        private     Vector2           m_viewportOffset;

        private     Vector2           m_position;
        private     Vector2           m_size;

        private     Vector2           m_marginLeftTop;
        private     Vector2           m_marginRightBottom;

        private     VisibilityMode    m_visibility;

        private     bool              m_dirty;

        private     UFormsApplication m_application;


        #region Other events

        protected virtual void OnUpdate() { }

        #endregion


        #region Internal Drawing Events

        protected virtual void OnBeforeDraw() { }
        protected virtual void OnDraw() { }
        protected virtual void OnLayout() { }
        protected virtual void OnAfterLayout() { }
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

        public Control()
        {
            Init( Vector2.zero, DefaultSize );
        }


        public Control( Vector2 position, Vector2 size )
        {
            Init( position, size );
        }

        private void Init( Vector2 position, Vector2 size )
        {
            Children = new List<Control>();
            Decorators = new List<Decorator>();

            Position = position;
            Size = size;

            Enabled = true;
            Visibility = VisibilityMode.Visible;

            ResetPivotRoot = false;

#if UFORMS_DEBUG_RECTS
            AddDecorator( new BackgroundColor( new Color( Random.value, Random.value, Random.value ) ) );
#endif
        }


        public void SetApplicationContext( UFormsApplication app )
        {
            if ( m_application != app )
            {
                m_application = app;

                foreach( Control child in Children )
                {
                    child.SetApplicationContext( app );
                }
            }
        }

        public Control AddChild( Control child )
        {
            if ( child.Dirty )
            {
                Dirty = true;
            }

            child.m_container = this;
            child.SetApplicationContext( m_application );

            Children.Add( child );

            return child;
        }


        public Control RemoveChild( Control child )
        {
            if ( child.m_container == this )
            {
                child.m_container = null;

                Children.Remove( child );
            }

            return child;
        }


        public Decorator AddDecorator( Decorator decorator )
        {
            decorator.SetControl( this );

            Decorators.Add( decorator );

            return decorator;
        }


        public Decorator RemoveDecorator( Decorator decorator )
        {
            if ( Decorators.Contains( decorator ) )
            {
                decorator.SetControl( null );

                Decorators.Remove( decorator );
            }

            return decorator;
        }


        public Control SetMargin( float left, float top, float right, float bottom,
            MetricsUnits leftu = MetricsUnits.Pixel, MetricsUnits topu = MetricsUnits.Pixel, MetricsUnits rightu = MetricsUnits.Pixel, MetricsUnits bottomu = MetricsUnits.Pixel )
        {
            MarginLeftTop       = new Vector2( left, top );
            MarginRightBottom   = new Vector2( right, bottom );

            MarginLeftUnits     = leftu;
            MarginRightUnits    = rightu;
            MarginTopUnits      = topu;
            MarginBottomUnits   = bottomu;

            return this;
        }        


        public Control SetSize( float x, float y, MetricsUnits wu = MetricsUnits.Pixel, MetricsUnits hu = MetricsUnits.Pixel )
        {
            return SetSize( new Vector2( x, y ), wu, hu );
        }


        public Control SetSize( Vector2 size, MetricsUnits wu = MetricsUnits.Pixel, MetricsUnits hu = MetricsUnits.Pixel )
        {
            Size        = size;
            WidthUnits  = wu;
            HeightUnits = hu;
            return this;
        }


        public Control SetWidth( float width, MetricsUnits wu = MetricsUnits.Pixel )
        {
            Size       = new Vector2( width, Size.y );
            WidthUnits = wu;

            return this;
        }


        public Control SetHeight( float height, MetricsUnits hu = MetricsUnits.Pixel )
        {
            Size        = new Vector2( Size.x, height );
            HeightUnits = hu;

            return this;
        }


        public Control SetPosition( float x, float y, MetricsUnits xu = MetricsUnits.Pixel, MetricsUnits yu = MetricsUnits.Pixel )
        {
            return SetPosition( new Vector2( x, y ), xu, yu );
        }


        public Control SetPosition( Vector2 position, MetricsUnits xu = MetricsUnits.Pixel, MetricsUnits yu = MetricsUnits.Pixel )
        {
            Position        = position;
            PositionXUnits  = xu;
            PositionYUnits  = yu;
            return this;
        }


        public bool PointInControl( Vector2 p )
        {
            return ScreenRect.Contains( ViewportToAbsolutePosition( p ) );
        }


        public Vector2 ViewportToAbsolutePosition( Vector2 p )
        {
            return p + ViewportOffset;
        }


        // Returns the content bounds rectangle without factoring the Size property
        public Rect GetContentBounds()
        {
            Rect r = new Rect( 0, 0, 0, 0 );

            foreach ( Control child in Children )
            {
                r.x      = Mathf.Min( r.x, child.Position.x );
                r.y      = Mathf.Min( r.y, child.Position.y );
                r.width  = Mathf.Max( r.width, child.Visibility == VisibilityMode.Collapsed ? 0.0f : child.Position.x + child.Size.x );
                r.height = Mathf.Max( r.height, child.Visibility == VisibilityMode.Collapsed ? 0.0f : child.Position.y + child.Size.y );
            }

            return r;
        }


        public void Update()
        {
            OnUpdate();

            foreach( Control child in Children )
            {
                child.Update();
            }
        }


        public void Layout()
        {
            ResetPivotRoot = false;

            // Cache parent screen position
            if ( m_container == null || m_container.ResetPivotRoot )
            {
                ParentScreenPosition = Vector2.zero;
            }
            else
            {
                ParentScreenPosition = m_container.ParentScreenPosition + m_container.Position + m_container.MarginLeftTop;
            }            

            ScreenRect = new Rect(
                ParentScreenPosition.x 
                + ( PositionXUnits == MetricsUnits.Percentage ? m_container.ScreenRect.width * ( Position.x / 100.0f ) : Position.x )
                + ( MarginLeftUnits == MetricsUnits.Percentage ? m_container.ScreenRect.width * ( MarginLeftTop.x / 100.0f ) : MarginLeftTop.x ),

                ParentScreenPosition.y
                + ( PositionYUnits == MetricsUnits.Percentage ? m_container.ScreenRect.height * ( Position.y / 100.0f ) : Position.y )
                + ( MarginTopUnits == MetricsUnits.Percentage ? m_container.ScreenRect.height * ( MarginLeftTop.y / 100.0f ) : MarginLeftTop.y ),

                ( WidthUnits == MetricsUnits.Percentage ? m_container.ScreenRect.width * ( Size.x / 100.0f ) : Size.x )
                - ( MarginLeftUnits == MetricsUnits.Percentage ? m_container.ScreenRect.width * ( MarginLeftTop.x / 100.0f ) : MarginLeftTop.x )
                - ( MarginRightUnits == MetricsUnits.Percentage ? m_container.ScreenRect.width * ( MarginRightBottom.x / 100.0f ) : MarginRightBottom.x ),

                ( HeightUnits == MetricsUnits.Percentage ? m_container.ScreenRect.height * ( Size.y / 100.0f ) : Size.y )
                - ( MarginTopUnits == MetricsUnits.Percentage ? m_container.ScreenRect.height * ( MarginLeftTop.y / 100.0f ) : MarginLeftTop.y )
                - ( MarginBottomUnits == MetricsUnits.Percentage ? m_container.ScreenRect.height * ( MarginRightBottom.y / 100.0f ) : MarginRightBottom.y )
            );

            foreach ( Decorator decorator in Decorators )
            {
                decorator.Layout();
            }

            OnLayout();

            if ( m_visibility != VisibilityMode.Collapsed )
            {
                foreach( Control child in Children )
                {
                    child.Layout();
                }

                // Update bounds
                float x = 0.0f;
                float y = 0.0f;
                float w = Size.x;
                float h = Size.y;

                Rect content = GetContentBounds();
                x = Mathf.Min( x, content.x );
                y = Mathf.Min( y, content.y );
                w = Mathf.Max( w, content.width );
                h = Mathf.Max( h, content.height );

                x += Position.x;
                y += Position.y;

                Bounds = new Rect( x, y, w, h );
            }
            else
            {
                Bounds = new Rect( Position.x, Position.y, 0.0f, 0.0f );
            }           

            OnAfterLayout();

            foreach ( Decorator decorator in Decorators )
            {
                decorator.AfterLayout();
            }
        }

        public void Draw()
        {            
            // We will cache the enabled property at the beginning of the draw phase so we can safely determine if we should close it as soon as drawing completes.
            // This is a fail safe in case the state changes while drawing is being processed (one example would be buttons invoking actions if clicked immediately, inside the draw call).
            bool localScopeEnabled = Enabled;
            if ( !localScopeEnabled )
            {
                EditorGUI.BeginDisabledGroup( true );
            }

            if ( m_visibility == VisibilityMode.Visible )
            {
                foreach ( Decorator decorator in Decorators )
                {
                    decorator.BeforeDraw();
                }

                OnBeforeDraw();

                foreach ( Decorator decorator in Decorators )
                {
                    decorator.Draw();
                }

                foreach ( Control child in Children )
                {
                    child.Draw();
                }

                OnDraw();

                foreach ( Decorator decorator in Decorators )
                {
                    decorator.AfterDraw();
                }
            }                        

            if ( !localScopeEnabled )
            {
                EditorGUI.EndDisabledGroup();
            }

            if ( m_container == null )
            {
                // 10 seems to be the magic number of frames we need to keep a repaint request active for the layout to settle nicely and responsively.
                // Should probably figure out why the hell this does not get resolved in 2 passes ( assuming in a single pass not all layouting elements are in sync due to execution order )
                if ( Dirty && m_application && m_application.Frame > m_dirtyFrame + 10 )
                {
                    Dirty = false;
                }
            }
        }


        public void ProcessEvents( Event e )
        {
            // Don't process events for collapsed elements
            if ( Visibility == VisibilityMode.Collapsed )
            {
                return;
            }

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
    }
}