using UnityEngine;
using System.Collections;

namespace UForms
{
    public class View : Element
    {
        public Control Root { get; private set; }

        public bool VerticalScrollbar { get; set; }
        public bool HorizontalScrollbar { get; set; }
        
        public Rect ContentBounds { get; protected set; }

        // Current scrollbar position if applicable
        private Vector2 m_scrollPosition;
        private Rect m_scrollableRect;


        public View( Rect bounds = new Rect() ) : base( bounds )
        {
            Root = new Control( bounds );
            Root.Container = this;

            RecalculateBounds();
        }


        public override void Draw()
        {
            bool doScrollbars = false;

            if ( ContentBounds.xMin < Bounds.xMin || ContentBounds.yMin < Bounds.yMin || ContentBounds.xMax > Bounds.xMax || ContentBounds.yMax > Bounds.yMax )
            {
                doScrollbars = true;
            }

            if ( doScrollbars )
            {
                float w, h;
                w = ( HorizontalScrollbar ? ContentBounds.width : Bounds.width - Constants.SCROLLBAR_SIZE );
                h = ( VerticalScrollbar ? ContentBounds.height : Bounds.height - Constants.SCROLLBAR_SIZE );
                m_scrollableRect.Set( ContentBounds.xMin, ContentBounds.yMin, w, h );

                m_scrollPosition = GUI.BeginScrollView( Bounds, m_scrollPosition, m_scrollableRect );
            }

            if ( Root != null )
            {
                Root.Draw();
            }

            if ( doScrollbars )
            {
                GUI.EndScrollView();
            }
        }


        public virtual void AddControl( Control control )
        {
            Root.AddChild( control );            

            // When adding children, we only need to extend the bounds of based on the metrics of the added child
            if ( control.Bounds.xMin < ContentBounds.xMin )
            {
                ContentBounds = new Rect( control.Bounds.xMin, ContentBounds.yMin, ContentBounds.width, ContentBounds.height );
            }                        

            if ( control.Bounds.yMin < ContentBounds.yMin )
            {
                ContentBounds = new Rect( ContentBounds.xMin, control.Bounds.yMin, ContentBounds.width, ContentBounds.height );
            }

            if ( control.Bounds.xMax > ContentBounds.xMax )
            {
                ContentBounds = new Rect( ContentBounds.xMin, ContentBounds.yMin, ContentBounds.width + ( control.Bounds.xMax - ContentBounds.xMax ), ContentBounds.height );
            }

            if ( control.Bounds.yMax > ContentBounds.yMax )
            {
                ContentBounds = new Rect( ContentBounds.xMin, ContentBounds.yMin, ContentBounds.width, ContentBounds.height + ( control.Bounds.yMax - ContentBounds.yMax ) );
            }
        }


        public virtual void RemoveControl( Control control )
        {
            Root.RemoveChild( control );
            
            // When removing children, we need to test if the child is fully contained within the rect without overlapping the edges, otherwise, we need to recalculate the rect
            RecalculateBounds();
        }


        private void RecalculateBounds()
        {

        }

        public override void ProcessEvents( Event e )
        {
            base.ProcessEvents( e );

            if ( e != null && Root != null )
            {
                Root.ProcessEvents( e );
            }
        }
    }
}