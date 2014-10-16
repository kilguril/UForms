using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace UForms.Views
{
    public class SplitView : View
    {
        public enum SplitMode
        {
            Vertical,
            Horizontal
        }

        public View ViewA
        {
            get { return m_viewA; }
            set { if ( m_viewA != null ) { m_viewA.Container = null; } m_viewA = value; m_viewA.Container = this; RecalculateSubRects(); }
        }
        public View ViewB
        {
            get { return m_viewB; }
            set { if ( m_viewB != null ) { m_viewB.Container = null; } m_viewB = value; m_viewB.Container = this; RecalculateSubRects(); }
        }
        public float SplitPosition 
        {
            get { return m_splitPosition; }
            set { m_splitPosition = Mathf.Clamp01( value ); RecalculateSubRects(); } 
        }

        public bool Resizable { get; set; }

        public SplitMode Mode
        {
            get { return m_splitMode; }
            set { m_splitMode = value; RecalculateSubRects(); }
        }

        private View m_viewA;
        private View m_viewB;        

        private float m_splitPosition;

        private SplitMode m_splitMode;

        private bool m_isDragging;

        private const float     RESIZE_HANDLE_SIZE = 5.0f;
        private const float     RESIZE_MIN_VALUE   = 0.1f;
        private const float     RESIZE_MAX_VALUE   = 0.9f;

        public SplitView( Rect bounds = new Rect() ) : base( bounds )
        {
            SplitPosition = 0.5f;
            Mode = SplitMode.Horizontal;
            Resizable = true;

            m_isDragging = false;
        }

        public SplitView( View viewA, View viewB, SplitMode mode = SplitMode.Horizontal, float splitPosition = 0.5f, bool resizable = true, Rect bounds = new Rect() ) : base( bounds )
        {
            SplitPosition = splitPosition;
            Mode = mode;
            Resizable = resizable;

            ViewA = viewA;
            ViewB = viewB;

            m_isDragging = false;
        }
        
        protected override void OnBoundsModified()
        {
            RecalculateSubRects();
        }

        public override void Draw()
        {            
            if ( m_viewA != null )
            {
                m_viewA.Draw();
            }

            if ( m_viewB != null )
            {
                m_viewB.Draw();
            }                        
        }


        public override void AddControl( Control control )
        {
            if ( ViewA != null )
            {
                ViewA.AddControl( control );
            }
            else
            {
                if ( ViewB != null )
                {
                    ViewB.AddControl( control );
                }
            }
        }


        public override void RemoveControl( Control control )
        {
            if ( ViewA != null )
            {
                ViewA.RemoveControl( control );
            }

            if ( ViewB != null )
            {
                ViewB.RemoveControl( control );
            }
        }

        private void RecalculateSubRects()
        {
            if ( m_viewA != null && m_viewB != null )
            {
                switch( m_splitMode )
                {
                    case SplitMode.Horizontal:
                    m_viewA.HorizontalScrollbar = false;
                    m_viewB.HorizontalScrollbar = false;

                    float w = Bounds.width * m_splitPosition;

                    m_viewA.Bounds = new Rect(
                        Bounds.xMin,
                        Bounds.yMin,
                        w,
                        Bounds.height
                    );                    

                    m_viewB.Bounds = new Rect(
                        Bounds.xMin + w,
                        Bounds.yMin,
                        Bounds.width - w,
                        Bounds.height
                    );
                    break;

                    case SplitMode.Vertical:
                    m_viewA.VerticalScrollbar = false;
                    m_viewB.VerticalScrollbar = false;

                    float h = Bounds.height * m_splitPosition;

                    m_viewA.Bounds = new Rect(
                        Bounds.xMin,
                        Bounds.yMin,
                        Bounds.width,
                        h
                    );

                    m_viewB.Bounds = new Rect(
                        Bounds.xMin,
                        Bounds.yMin + h,
                        Bounds.width,
                        Bounds.height - h
                    );
                    break;
                }
            }
            else
            {
                if ( m_viewA != null )
                {
                    m_viewA.Bounds = Bounds;
                }

                if ( m_viewB != null )
                {
                    m_viewB.Bounds = Bounds;
                }
            }
        }

        public override void ProcessEvents( Event e )
        {
            base.ProcessEvents( e );

            if ( ViewA != null && e != null )
            {
                ViewA.ProcessEvents( e );
            }

            if ( ViewB != null && e != null )
            {
                ViewB.ProcessEvents( e );
            }
        }


        protected override void OnMouseDown( Event e )
        {
            if ( Resizable )
            {
                if ( PointInResizeHandle( e.mousePosition ) )
                {
                    m_isDragging = true;
                    e.Use();
                }
            }

            base.OnDragPerform( e );
        }
        

        protected override void OnMouseDrag( Event e )
        {
            if ( m_isDragging )
            {
                float factor = 0.0f;

                switch( Mode )
                {
                    case SplitMode.Horizontal:  factor = e.delta.x / Bounds.width;  break;
                    case SplitMode.Vertical:    factor = e.delta.y / Bounds.height; break;
                }

                SplitPosition = Mathf.Clamp( SplitPosition + factor, RESIZE_MIN_VALUE, RESIZE_MAX_VALUE );

                e.Use();
            }

            base.OnDragUpdated( e );
        }


        protected override void OnMouseUp( Event e )
        {
            if ( m_isDragging )
            {
                m_isDragging = false;
                e.Use();
            }

            base.OnDragExited( e );
        }


        private bool PointInResizeHandle( Vector2 point )
        {
            Rect r;

            switch ( Mode )
            {
                default:
                case SplitMode.Horizontal:
                    r = new Rect(
                        ScreenBounds.xMin + Bounds.width * SplitPosition - RESIZE_HANDLE_SIZE,
                        ScreenBounds.yMin,
                        RESIZE_HANDLE_SIZE * 2,
                        Bounds.height
                    );
                break;

                case SplitMode.Vertical:
                    r = new Rect(
                        ScreenBounds.xMin,
                        ScreenBounds.yMin + Bounds.height * SplitPosition - RESIZE_HANDLE_SIZE,
                        Bounds.width,
                        RESIZE_HANDLE_SIZE * 2
                    );
                break;
            }

            return r.Contains( point );
        }
    }
}