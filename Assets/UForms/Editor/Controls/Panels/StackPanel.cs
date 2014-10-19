using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UForms.Controls.Panels
{
    public class StackPanel : ScrollPanel
    {
        public enum StackMode
        {
            Vertical,
            Horizontal
        }

        public enum OverflowMode
        {
            Flow,
            Contain
        }

        public StackMode Mode
        {
            get { return m_mode;}
            set { m_mode = value; SetDirty(); }
        }

        public OverflowMode Overflow
        {
            get { return m_overflow; }
            set { m_overflow = value; SetDirty(); }
        }

        private OverflowMode m_overflow;
        private StackMode    m_mode;

        public StackPanel( Vector2 position, Vector2 size, StackMode mode = StackMode.Horizontal, OverflowMode overflow = OverflowMode.Flow ) : base( position, size )
        {
            VerticalScrollbar       = true;
            HorizontalScrollbar     = true;
            FillContainerVertical   = true;
            FillContainerHorizontal = true;
            HandleMouseWheel        = true;
            Mode                    = mode;
            Overflow                = overflow;
        }

        protected override void OnLayout()
        {
            if( Overflow == OverflowMode.Flow )
            {
                LayoutWithOverflow();
            }
            else
            {
                LayoutContained();
            }
            
            base.OnLayout();
        }


        private void LayoutWithOverflow()
        {
            float offset = 0.0f;

            foreach ( Control child in Children )
            {
                switch ( m_mode )
                {
                    case StackMode.Horizontal:

                    child.Position = new Vector2( offset, 0.0f );
                    offset += child.ScreenSize.x;

                    break;

                    case StackMode.Vertical:

                    child.Position = new Vector2( 0.0f, offset );
                    offset += child.ScreenSize.y;

                    break;
                }
            }
        }


        private void LayoutContained()
        {
            List< Control > group = new List<Control>();
            float offset    = 0.0f;
            float offset2   = 0.0f;

            foreach( Control child in Children )
            {
                switch( m_mode )
                {
                    case StackMode.Horizontal:

                        if ( offset + child.ScreenSize.x >= Size.x )
                        {
                            offset2 += LayoutContainedCloseGroup( group, offset2 );
                            offset = 0.0f;
                        }

                        offset += child.ScreenSize.x;
                        group.Add( child );

                    break;

                    case StackMode.Vertical:

                        if ( offset + child.ScreenSize.y >= Size.y )
                        {
                            offset2 += LayoutContainedCloseGroup( group, offset2 );
                            offset = 0.0f;
                            
                        }
                        
                        offset += child.ScreenSize.y;
                        group.Add( child );

                    break;
                }
            }

            LayoutContainedCloseGroup( group, offset2 );
        }

        
        private float LayoutContainedCloseGroup( List< Control > group, float offset )
        {
            float groupSize   = 0.0f;
            float localOffset = 0.0f;

            while ( group.Count > 0 )
            {
                Control item = group[ 0 ];
                group.RemoveAt( 0 );

                switch( m_mode )
                {
                    case StackMode.Horizontal:
                    
                        item.Position = new Vector2( localOffset, offset );
                        localOffset += item.ScreenSize.x;

                        if ( item.ScreenSize.y > groupSize )
                        {
                            groupSize = item.ScreenSize.y;
                        }
                    break;

                    case StackMode.Vertical:

                        item.Position = new Vector2( offset, localOffset );
                        localOffset += item.ScreenSize.y;

                        if ( item.ScreenSize.x > groupSize )
                        {
                            groupSize = item.ScreenSize.x;
                        }
                    break;
                }

            }

            return groupSize;
        }
    }
}