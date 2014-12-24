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
    /// <summary>
    /// Defines a base class for all controls.
    /// </summary>
    public class Control : IDrawable
    {
        /// <summary>
        /// Control visibility mode.
        /// </summary>
        public enum VisibilityMode
        {
            /// <summary>
            /// Control is visible.
            /// </summary>
            Visible,

            /// <summary>
            /// Control is not visible, but it's space is reserved.
            /// </summary>
            Hidden,

            /// <summary>
            /// Control is not visible, and takes no space when doing layout.
            /// </summary>
            Collapsed
        }

        /// <summary>
        /// Defines available metrics units for measuring sizes.
        /// </summary>
        public enum MetricsUnits
        {
            /// <summary>
            /// Absolute pixel value.
            /// </summary>
            Pixel,

            /// <summary>
            /// Relative percentage value.
            /// </summary>
            Percentage
        }


        /// <summary>        
        /// Dirty flag should be used to trigger a repaint on internal component changes, as otherwise repaint will only be invoked by specific editor events
        /// flag will propagate upwards and will be collected by the application from the root component if it reaches it.
        /// </summary>
        [HideInInspector]
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

        /// <summary>
        /// This field was added for use with the designer to avoid the need to create additional metadata. It is not used anywhere else currently
        /// </summary>
        
        public string Id
        {
            get;
            set; 
        }

        /// <summary>
        /// I honestly don't remember what this one does...
        /// </summary>
        [HideInInspector]
        public Rect Bounds        
        {
            get             { return m_bounds; }
            private set     { m_bounds = value; } 
        }

        /// <summary>
        /// Final computed screen rect taking into account screen position, margins and size
        /// </summary>
        [HideInInspector]
        public Rect ScreenRect
        {
            get         { return m_screenRect; }
            private set { m_screenRect = value; }
        }


        /// <summary>
        /// Since click coordinates are based on the current viewport, we will need to know the viewport offset in case of scrolling
        /// </summary>
        [HideInInspector]
        public Vector2 ViewportOffset
        {
            get         { return m_container == null ? m_viewportOffset : m_container.ViewportOffset + m_viewportOffset; }
            set         { m_viewportOffset = value; }
        }


        /// <summary>
        /// Cached parent's screen position so deep elements don't have to traverse all the way up
        /// </summary>
        [HideInInspector]
        public Vector2 ParentScreenPosition         
        {
            get             { return m_screenPosition; } 
            set             { m_screenPosition = value; } 
        }

        /// <summary>
        /// Position is used to hint to the control it's desired position on screen.
        /// </summary>
        public Vector2 Position 
        {
            get { return m_position; }
            set { m_position = value; } 
        }

        /// <summary>
        /// 
        /// </summary>
        public MetricsUnits PositionXUnits
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public MetricsUnits PositionYUnits
        {
            get;
            set;
        }

        /// <summary>
        /// Size is used to hint to the control it's desired size on screen.
        /// </summary>
        public Vector2 Size 
        {
            get { return m_size; }
            set { m_size = value; } 
        }

        /// <summary>
        /// 
        /// </summary>
        public MetricsUnits WidthUnits
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public MetricsUnits HeightUnits
        {
            get;
            set;
        }
     
        /// <summary>
        /// 
        /// </summary>
        public Vector2 MarginLeftTop
        {
            get { return m_marginLeftTop; }
            set { m_marginLeftTop = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Vector2 MarginRightBottom
        {
            get { return m_marginRightBottom; }
            set { m_marginRightBottom = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public MetricsUnits MarginLeftUnits
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public MetricsUnits MarginRightUnits
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public MetricsUnits MarginTopUnits
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public MetricsUnits MarginBottomUnits
        {
            get;
            set;
        }
        
        /// <summary>
        /// Is this control enabled? this property will propagate to all child contorls and can be applied to interactive controls as well as containers 
        /// </summary>
        public bool Enabled
        {
            get;
            set;
        }


        /// <summary>
        /// What is the visibility mode of the control?
        /// Visible = default
        /// Hidden = layout control and reserve space but don't draw
        /// Collapsed = don't layout, generate empty rect and don't draw
        /// </summary>
        public VisibilityMode    Visibility
        {
            get { return m_visibility; }
            set { m_visibility = value; }
        }

        /// <summary>
        /// Panels should override this property to specify they reset the pivot offset to 0,0
        /// </summary>
        [HideInInspector]
        public bool ResetPivotRoot
        {
            get;
            set;
        }

        /// <summary>
        /// Default size for this control
        /// </summary>
        [HideInInspector]
        protected virtual Vector2 DefaultSize
        {
            get { return Vector2.zero; }
        }

        /// <summary>
        /// Contained children elements.               
        /// </summary>
        [HideInInspector]
        public      List<Control>     Children      { get; private set; }         

        /// <summary>
        /// 
        /// </summary>
        [HideInInspector]
        public      List<Decorator>   Decorators { get; private set; }

        /// <summary>
        /// Containing element if control is in a hierarchy.
        /// </summary>
        protected   Control           m_container;                               

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

        private     List< int >       m_pendingRemoval;
        private     Queue< Control >  m_pendingAddition;

        #region Other events

        /// <summary>
        /// Override this method to add functionality on update.
        /// </summary>
        protected virtual void OnUpdate() { }

        #endregion


        #region Internal Drawing Events

        /// <summary>
        /// Override this method to add functionality to an early draw pass.
        /// </summary>
        protected virtual void OnBeforeDraw() { }

        /// <summary>
        /// Override this method to add functionality to the main draw pass. 
        /// </summary>
        protected virtual void OnDraw() { }

        /// <summary>
        /// Override this method to add functionality to the layout pass.
        /// </summary>
        protected virtual void OnLayout() { }

        /// <summary>
        /// Override this method to add functionality to a late layout pass.
        /// </summary>
        protected virtual void OnAfterLayout() { }
        #endregion

        #region Internal System Events

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnContextClick( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDragExited( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDragPerform( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDragUpdated( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnExecuteCommand( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnIgnore( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnKeyDown( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnKeyUp( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLayout( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseDown( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseDrag( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseMove( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseUp( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRepaint( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnScrollWheel( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnUsed( Event e ) { }

        /// <summary>
        /// Override this to handle this event type.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnValidateCommand( Event e ) { }

        #endregion

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public Control()
        {
            Init( Vector2.zero, DefaultSize );
        }

        /// <summary>
        /// Constructor with position and size initializers.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
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

            m_pendingRemoval  = new List<int>();
            m_pendingAddition = new Queue<Control>();

#if UFORMS_DEBUG_RECTS
            AddDecorator( new BackgroundColor( new Color( Random.value, Random.value, Random.value ) ) );
#endif
        }


        /// <summary>
        /// Used internally to reference the application context to this control.
        /// </summary>
        /// <param name="app"></param>
        public void SetApplicationContext( UFormsApplication app )
        {
            if ( m_application != app )
            {
                m_application = app;

                foreach( Control child in Children )
                {
                    if ( child != null )
                    {
                        child.SetApplicationContext( app );
                    }
                }
            }
        }

        /// <summary>
        /// Adds a child control.
        /// </summary>
        /// <param name="child">Control objec to add.</param>
        /// <returns>Added control object.</returns>
        public Control AddChild( Control child )
        {
            if ( child.Dirty )
            {
                Dirty = true;
            }

            child.m_container = this;
            child.SetApplicationContext( m_application );

            m_pendingAddition.Enqueue( child );

            return child;
        }


        /// <summary>
        /// Removes a child control.
        /// </summary>
        /// <param name="child">Control object to remove.</param>
        /// <returns>Removed control object.</returns>
        public Control RemoveChild( Control child )
        {
            if ( child.m_container == this )
            {
                child.m_container = null;

                int index = Children.IndexOf( child );
                if ( index >= 0 )
                {
                    child = null;
                    m_pendingRemoval.Add( index );
                }
            }

            return child;
        }


        /// <summary>
        /// Adds a decorator to this control.
        /// </summary>
        /// <param name="decorator">Decorator object to add.</param>
        /// <returns>Added decorator object.</returns>
        public Decorator AddDecorator( Decorator decorator )
        {
            decorator.SetControl( this );

            Decorators.Add( decorator );

            return decorator;
        }


        /// <summary>
        /// Removes a decorator from this control.
        /// </summary>
        /// <param name="decorator">Decorator object to remove.</param>
        /// <returns>Removed decorator object.</returns>
        public Decorator RemoveDecorator( Decorator decorator )
        {
            if ( Decorators.Contains( decorator ) )
            {
                decorator.SetControl( null );

                Decorators.Remove( decorator );
            }

            return decorator;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="leftu"></param>
        /// <param name="topu"></param>
        /// <param name="rightu"></param>
        /// <param name="bottomu"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="wu"></param>
        /// <param name="hu"></param>
        /// <returns></returns>
        public Control SetSize( float x, float y, MetricsUnits wu = MetricsUnits.Pixel, MetricsUnits hu = MetricsUnits.Pixel )
        {
            return SetSize( new Vector2( x, y ), wu, hu );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="wu"></param>
        /// <param name="hu"></param>
        /// <returns></returns>
        public Control SetSize( Vector2 size, MetricsUnits wu = MetricsUnits.Pixel, MetricsUnits hu = MetricsUnits.Pixel )
        {
            Size        = size;
            WidthUnits  = wu;
            HeightUnits = hu;
            return this;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="wu"></param>
        /// <returns></returns>
        public Control SetWidth( float width, MetricsUnits wu = MetricsUnits.Pixel )
        {
            Size       = new Vector2( width, Size.y );
            WidthUnits = wu;

            return this;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="height"></param>
        /// <param name="hu"></param>
        /// <returns></returns>
        public Control SetHeight( float height, MetricsUnits hu = MetricsUnits.Pixel )
        {
            Size        = new Vector2( Size.x, height );
            HeightUnits = hu;

            return this;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xu"></param>
        /// <param name="yu"></param>
        /// <returns></returns>
        public Control SetPosition( float x, float y, MetricsUnits xu = MetricsUnits.Pixel, MetricsUnits yu = MetricsUnits.Pixel )
        {
            return SetPosition( new Vector2( x, y ), xu, yu );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="xu"></param>
        /// <param name="yu"></param>
        /// <returns></returns>
        public Control SetPosition( Vector2 position, MetricsUnits xu = MetricsUnits.Pixel, MetricsUnits yu = MetricsUnits.Pixel )
        {
            Position        = position;
            PositionXUnits  = xu;
            PositionYUnits  = yu;
            return this;
        }


        /// <summary>
        /// Tests if a given screen point is inside this control
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool PointInControl( Vector2 p )
        {
            return ScreenRect.Contains( ViewportToAbsolutePosition( p ) );
        }


        /// <summary>
        /// Transforms viewport position to absolute position?
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Vector2 ViewportToAbsolutePosition( Vector2 p )
        {
            return p + ViewportOffset;
        }


        /// <summary>
        /// Returns the content bounds rectangle without factoring the Size property
        /// </summary>
        /// <returns></returns>
        public Rect GetContentBounds()
        {
            Rect r = new Rect( 0, 0, 0, 0 );

            foreach ( Control child in Children )
            {
                if ( child != null )
                {
                    r.x = Mathf.Min( r.x, child.Position.x );
                    r.y = Mathf.Min( r.y, child.Position.y );
                    r.width = Mathf.Max( r.width, child.Visibility == VisibilityMode.Collapsed ? 0.0f : child.Position.x + child.Size.x );
                    r.height = Mathf.Max( r.height, child.Visibility == VisibilityMode.Collapsed ? 0.0f : child.Position.y + child.Size.y );
                }
            }

            return r;
        }


        /// <summary>
        /// Update method, used internally.
        /// </summary>
        public void Update()
        {
            if ( m_pendingRemoval.Count > 0 )
            {
                // Sort in descending order so removal does not affect indices on multile removes
                m_pendingRemoval.Sort( ( a, b ) => { return b.CompareTo( a ); } );

                foreach ( int index in m_pendingRemoval )
                {
                    Children.RemoveAt( index );
                }

                m_pendingRemoval.Clear();
            }

            while ( m_pendingAddition.Count > 0 )
            {
                Control child = m_pendingAddition.Dequeue();

                if ( child.Dirty )
                {
                    Dirty = true;
                }

                Children.Add( child );
            }

            OnUpdate();

            foreach( Control child in Children )
            {
                child.Update();
            }
        }


        /// <summary>
        /// Layout method, used internally.
        /// </summary>
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

            if ( m_container != null )
            {
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
            }
            else
            {
                ScreenRect = new Rect(
                    Position.x + MarginLeftTop.x,
                    Position.y + MarginLeftTop.y,
                    Size.x - MarginLeftTop.x - MarginRightBottom.x,
                    Size.y - MarginLeftTop.y - MarginRightBottom.y
                );
            }

            foreach ( Decorator decorator in Decorators )
            {
                decorator.Layout();
            }

            OnLayout();

            if ( m_visibility != VisibilityMode.Collapsed )
            {
                foreach( Control child in Children )
                {
                    if ( child != null )
                    {
                        child.Layout();
                    }
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

        /// <summary>
        /// Draw method, used internally.
        /// </summary>
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
                    if ( child != null )
                    {
                        child.Draw();
                    }
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


        /// <summary>
        /// Event processing method, used internally.
        /// </summary>
        /// <param name="e"></param>
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

                if ( child != null )
                {
                    child.ProcessEvents( e );
                }
            }
        }
    }
}