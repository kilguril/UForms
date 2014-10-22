using UnityEngine;
using System.Collections;

namespace UForms.Controls.Panels
{
    public class ScrollPanel : Control
    {
        public bool FillContainerVertical           
        {
            get { return m_fillContainerVertical; }
            set { m_fillContainerVertical = value; }
        }

        public bool FillContainerHorizontal
        {
            get { return m_fillContainerHorizontal; }
            set { m_fillContainerHorizontal = value; }
        }

        public bool VerticalScrollbar       { get; set; }
        public bool HorizontalScrollbar     { get; set; }
        public bool HandleMouseWheel        { get; set; }
        
        private Vector2 m_scrollPosition;
        private Rect m_scrollableRect;

        private bool m_fillContainerHorizontal;
        private bool m_fillContainerVertical;
        private bool m_doScrollbars;

        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 100.0f, 100.0f ); }
        }

        // The size of Unity's built in scrollbar in screen units (used to offset from actual display area)
        private const float SCROLLBAR_SIZE = 16.0f;


        public ScrollPanel( bool verticalScroll = true, bool horizontalScroll = true, bool fillContainer = false, bool handleMouseWheel = true ) : base()
        {
            VerticalScrollbar = verticalScroll;
            HorizontalScrollbar = horizontalScroll;
            FillContainerVertical = fillContainer;
            FillContainerHorizontal = fillContainer;
            HandleMouseWheel = handleMouseWheel;
        }

        public ScrollPanel( Vector2 position, Vector2 size, bool verticalScroll = true, bool horizontalScroll = true, bool fillContainer = false, bool handleMouseWheel = true ) : base( position, size )
        {
            VerticalScrollbar       = verticalScroll;
            HorizontalScrollbar     = horizontalScroll;
            FillContainerVertical   = fillContainer;
            FillContainerHorizontal = fillContainer;
            HandleMouseWheel        = handleMouseWheel;
        }


        protected override void OnBeforeDraw()
        {
            m_doScrollbars = false;

            Rect content = GetContentBounds();

            if ( content.xMin < 0.0f || content.yMin < 0.0f || content.xMax > Size.x - MarginLeftTop.x - MarginRightBottom.x || content.yMax > Size.y - MarginLeftTop.y - MarginRightBottom.y )
            {
                m_doScrollbars = true;
            }

            if ( m_doScrollbars )
            {
                float w,h;
                w = ( HorizontalScrollbar ? content.width : Size.x - MarginLeftTop.x - MarginRightBottom.x - SCROLLBAR_SIZE );
                h = ( VerticalScrollbar ? content.height : Size.y - MarginLeftTop.y - MarginRightBottom.y - SCROLLBAR_SIZE );
                m_scrollableRect.Set( content.xMin, content.yMin, w, h );                

                m_scrollPosition = GUI.BeginScrollView( ScreenRect, m_scrollPosition, m_scrollableRect );
            }
        }


        protected override void OnDraw()
        {
            if ( m_doScrollbars )
            {
                GUI.EndScrollView( HandleMouseWheel );
            }
        }


        protected override void OnLayout()
        {
            if ( m_fillContainerVertical && m_container != null )
            {
                Position = new Vector2( Position.x, 0.0f );
                Size     = new Vector2( Size.x, m_container.Size.y );
            }

            if ( m_fillContainerHorizontal && m_container != null )
            {
                Position = new Vector2( 0.0f, Position.y );
                Size = new Vector2( m_container.Size.x, Size.y );
            }
        }
    }
}