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
            get { return m_mode; }
            set { m_mode = value; }
        }

        public OverflowMode Overflow
        {
            get { return m_overflow; }
            set { m_overflow = value; }
        }

        private OverflowMode m_overflow;
        private StackMode    m_mode;


        public StackPanel( StackMode mode = StackMode.Horizontal, OverflowMode overflow = OverflowMode.Flow )
            : base()
        {
            VerticalScrollbar = true;
            HorizontalScrollbar = true;
            HandleMouseWheel = true;
            Mode = mode;
            Overflow = overflow;
        }


        public StackPanel( Vector2 position, Vector2 size, StackMode mode = StackMode.Horizontal, OverflowMode overflow = OverflowMode.Flow )
            : base( position, size )
        {
            VerticalScrollbar = true;
            HorizontalScrollbar = true;
            HandleMouseWheel = true;
            Mode = mode;
            Overflow = overflow;
        }

        protected override void OnAfterLayout()
        {
            if ( Overflow == OverflowMode.Flow )
            {
                LayoutWithOverflow();
            }
            else
            {
                LayoutContained();
            }
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
                    offset += child.Bounds.width;

                    break;

                    case StackMode.Vertical:

                    child.Position = new Vector2( 0.0f, offset );
                    offset += child.Bounds.height;

                    break;
                }
            }
        }


        private void LayoutContained()
        {
            List< Control > group = new List<Control>();
            float offset    = 0.0f;
            float offset2   = 0.0f;

            foreach ( Control child in Children )
            {
                switch ( m_mode )
                {
                    case StackMode.Horizontal:

                    if ( offset + child.Bounds.width >= Size.x )
                    {
                        offset2 += LayoutContainedCloseGroup( group, offset2 );
                        offset = 0.0f;
                    }

                    offset += child.Bounds.width;
                    group.Add( child );

                    break;

                    case StackMode.Vertical:

                    if ( offset + child.Bounds.height >= Size.y )
                    {
                        offset2 += LayoutContainedCloseGroup( group, offset2 );
                        offset = 0.0f;

                    }

                    offset += child.Bounds.height;
                    group.Add( child );

                    break;
                }
            }

            LayoutContainedCloseGroup( group, offset2 );
        }


        private float LayoutContainedCloseGroup( List<Control> group, float offset )
        {
            float groupSize   = 0.0f;
            float localOffset = 0.0f;

            while ( group.Count > 0 )
            {
                Control item = group[ 0 ];
                group.RemoveAt( 0 );

                switch ( m_mode )
                {
                    case StackMode.Horizontal:

                    item.Position = new Vector2( localOffset, offset );
                    localOffset += item.Bounds.width;

                    if ( item.Bounds.height > groupSize )
                    {
                        groupSize = item.Bounds.height;
                    }
                    break;

                    case StackMode.Vertical:

                    item.Position = new Vector2( offset, localOffset );
                    localOffset += item.Bounds.height;

                    if ( item.Bounds.width > groupSize )
                    {
                        groupSize = item.Bounds.width;
                    }
                    break;
                }

            }

            return groupSize;
        }
    }
}