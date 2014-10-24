using UnityEngine;
using System.Collections;

namespace UForms.Controls.Panels
{
    public class ScrollPanel : Control
    {
        public bool VerticalScrollbar       { get; set; }
        public bool HorizontalScrollbar     { get; set; }
        public bool HandleMouseWheel        { get; set; }
        
        private Vector2 m_scrollPosition;
        private Rect m_scrollableRect;
        private bool m_doScrollbars;

        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 100.0f, 100.0f ); }
        }

        protected override bool ResetPivotRoot
        {
            get { return m_doScrollbars; }
        }

        // The size of Unity's built in scrollbar in screen units (used to offset from actual display area)
        private const float SCROLLBAR_SIZE = 16.0f;


        public ScrollPanel( bool verticalScroll = true, bool horizontalScroll = true, bool handleMouseWheel = true ) : base()
        {
            VerticalScrollbar = verticalScroll;
            HorizontalScrollbar = horizontalScroll;
            HandleMouseWheel = handleMouseWheel;
        }

        public ScrollPanel( Vector2 position, Vector2 size, bool verticalScroll = true, bool horizontalScroll = true, bool handleMouseWheel = true ) : base( position, size )
        {
            VerticalScrollbar       = verticalScroll;
            HorizontalScrollbar     = horizontalScroll;
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
    }
}