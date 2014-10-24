using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;
using UForms.Controls.Fields;

namespace UForms.Controls.Panels
{
    public class FoldPanel : StackPanel
    {
        public bool Unfolded
        {
            get { return m_foldoutHeader.Value; }
            set { m_foldoutHeader.Value = value; }
        }

        private Foldout                                 m_foldoutHeader;
        private Dictionary< Control, VisibilityMode >   m_cachedVisibility;


        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        public FoldPanel( string label = "", bool unfolded = true, bool toggleOnClick = true )
            : base()
        {
            Initialize( label, unfolded, toggleOnClick );

        }

        public FoldPanel( Vector2 position, Vector2 size, string label = "", bool unfolded = true, bool toggleOnClick = true )
            : base( position, size )
        {
            Initialize( label, unfolded, toggleOnClick );
        }

        private void Initialize( string label, bool unfolded, bool toggleOnClick )
        {
            m_cachedVisibility = new Dictionary<Control, VisibilityMode>();

            Mode = StackMode.Vertical;
            HandleMouseWheel = false;
            HorizontalScrollbar = false;
            VerticalScrollbar = false;

            m_foldoutHeader = new Foldout( label, unfolded, toggleOnClick );
            AddChild( m_foldoutHeader );

            Unfolded = unfolded;
            m_foldoutHeader.ValueChange += FoldoutValueChange;

            Unfold( unfolded );
        }

        protected override void OnAfterLayout()
        {
            base.OnAfterLayout();

            Rect content = GetContentBounds();

            if ( Size.y != content.height )
            {
                Size = new Vector2( Size.x, GetContentBounds().height );
                Dirty = true;
            }            
        }

        void FoldoutValueChange( Events.IEditable sender, Events.EditEventArgs args, Event nativeEvent )
        {
            Unfold( ( bool )args.newValue );
        }

        void PruneCache()
        {
            // Prune visibility cache - hacky
            List< Control > toRemove = new List<Control>();
            foreach ( KeyValuePair< Control, VisibilityMode > kvp in m_cachedVisibility )
            {
                if ( !Children.Contains( kvp.Key ) )
                {
                    toRemove.Add( kvp.Key );
                }
            }

            foreach ( Control item in toRemove )
            {
                m_cachedVisibility.Remove( item );
            }

            toRemove.Clear();
            toRemove = null;
        }

        void Unfold( bool state )
        {
            foreach ( Control child in Children )
            {
                if ( child != m_foldoutHeader )
                {
                    if ( state )
                    {
                        if ( m_cachedVisibility.ContainsKey( child ) )
                        {
                            child.Visibility = m_cachedVisibility[ child ];
                        }
                        else
                        {
                            child.Visibility = VisibilityMode.Visible;
                        }
                    }
                    else
                    {
                        if ( m_cachedVisibility.ContainsKey( child ) )
                        {
                            m_cachedVisibility[ child ] = child.Visibility;
                        }
                        else
                        {
                            m_cachedVisibility.Add( child, child.Visibility );
                        }

                        child.Visibility = VisibilityMode.Collapsed;
                    }
                }
            }
        }
    }
}