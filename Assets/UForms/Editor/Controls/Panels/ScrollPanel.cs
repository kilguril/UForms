using UnityEngine;
using System.Collections;

namespace UForms.Controls.Panels
{
    public class ScrollPanel : Control
    {
        public bool FillContainer           
        {
            get { return m_fillContainer; }
            set { m_fillContainer = value; SetDirty(); }
        }

        public bool VerticalScrollbar       { get; set; }
        public bool HorizontalScrollbar     { get; set; }
        public bool HandleMouseWheel        { get; set; }

        private Vector2 m_scrollPosition;
        private Rect m_scrollableRect;

        private Rect m_displayRect;

        private bool m_fillContainer;
        private bool m_doScrollbars;

        // The size of Unity's built in scrollbar in screen units (used to offset from actual display area)
        private const float SCROLLBAR_SIZE = 16.0f;

        public ScrollPanel( Vector2 position, Vector2 size, bool verticalScroll = true, bool horizontalScroll = true, bool fillContainer = false, bool handleMouseWheel = true ) : base( position, size )
        {
            VerticalScrollbar   = verticalScroll;
            HorizontalScrollbar = horizontalScroll;
            FillContainer       = fillContainer;
            HandleMouseWheel    = handleMouseWheel;
        }


        protected override void OnBeforeDraw()
        {
            m_doScrollbars = false;

            // Since the content bounds are evaluated during the layout step, which is invoked after BeforeDraw and before Draw, the current value will be out of sync
            // That's why we need to evaluate the expected bounds using GetContentBounds().
            // Note this is an expensive call and should not be used outside of OnBeforeDraw as in OnDraw the value will be in sync AND more reliable as it will be evaluated after the actual layout step
            Rect contentBounds = GetContentBounds(); 

            if ( contentBounds.xMin < Position.x || contentBounds.yMin < Position.y || contentBounds.xMax > Size.x || contentBounds.yMax > Size.y )
            {
                m_doScrollbars = true;
            }

            if ( m_doScrollbars )
            {
                float w,h;
                w = ( HorizontalScrollbar ? contentBounds.width : Size.x - SCROLLBAR_SIZE );
                h = ( VerticalScrollbar ? contentBounds.height : Size.y - SCROLLBAR_SIZE );
                m_scrollableRect.Set( Bounds.xMin, Bounds.yMin, w, h );                

                m_scrollPosition = GUI.BeginScrollView( m_displayRect, m_scrollPosition, m_scrollableRect );
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
            if ( m_fillContainer )
            {
                if ( m_container != null )
                {
                    Position = Vector2.zero;
                    Size     = m_container.Size;                    
                }
            }

            m_displayRect.Set(
                ScreenPosition.x,
                ScreenPosition.y,
                Size.x,
                Size.y
            );
        }
    }
}