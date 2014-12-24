using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using UForms.Controls;
using UForms.Controls.Fields;
using UForms.Decorators;


namespace UForms.Designer
{
    public class HierarchyItem : Control
    {
        public delegate void ControlSelected( Control control );
        public event ControlSelected BoundControlSelected;

        public delegate void DecoratorSelected( Control control, Decorator decorator, int button );
        public event DecoratorSelected BoundDecoratorlSelected;

        protected override Vector2 DefaultSize
        {
            get
            {
                return new Vector2( 200.0f, 16.0f );
            }
        }

        private Control m_target;

        private Label m_label;
        private int   m_depth;


        private const float     LEVEL_INDENT = 12.0f;
        private const float     LINE_HEIGHT  = 16.0f;

        public HierarchyItem( Control target, int depth ) : base()
        {
            m_depth = depth;

            m_label = new Label();
            m_label.SetWidth( 100.0f, MetricsUnits.Percentage );
            m_label.SetPosition( m_depth * LEVEL_INDENT, 0.0f );
            m_label.SetHeight( LINE_HEIGHT );
            AddChild( m_label );

            SetWidth( 100.0f, MetricsUnits.Percentage );

            Bind( target );
        }


        public void UpdateDecoratorData( Control control )
        {
            foreach( Control child in Children )
            {
                if ( child is DecoratorItem )
                {
                    DecoratorItem item = child as DecoratorItem;
                    item.BoundDecoratorlSelected -= item_BoundDecoratorlSelected;
                    item.Release();
                    RemoveChild( item );
                }
            }


            int i = 0;
            
            foreach ( Decorator d in control.Decorators )
            {
                DecoratorItem item = new DecoratorItem( d );
                AddChild( item );

                item.SetPosition( LEVEL_INDENT * ( m_depth + 1 ), ( i + 1 ) * LINE_HEIGHT );
                i++;

                item.BoundDecoratorlSelected += item_BoundDecoratorlSelected;
            }

            SetHeight( ( i + 1 ) * LINE_HEIGHT );
        }

        void item_BoundDecoratorlSelected( Decorator decorator, int button )
        {
            if ( BoundDecoratorlSelected != null )
            {
                BoundDecoratorlSelected( m_target, decorator, button );
            }
        }


        public void Bind( Control control )
        {
            m_target = control;
            SetName();
            UpdateDecoratorData( control );
        }


        public void Release()
        {
            foreach ( Control child in Children )
            {
                if ( child is DecoratorItem )
                {
                    DecoratorItem item = child as DecoratorItem;
                    item.BoundDecoratorlSelected -= item_BoundDecoratorlSelected;
                    item.Release();
                    RemoveChild( item );
                }
            }

            m_target = null;
        }


        private void SetName()
        {
            string id      = string.IsNullOrEmpty( m_target.Id ) ? "MISSING ID" : m_target.Id;
            string type    = m_target.GetType().ToString();

            int lastDot    = type.LastIndexOf( '.' );
            if ( lastDot >= 0 )
            {
                type = type.Substring( lastDot + 1, type.Length - lastDot - 1 );
            }

            string display = string.Format( "{0} [ {1} ]", type, id );

            m_label.Text = display;
        }


        protected override void OnBeforeDraw()
        {
            if ( m_target != null )
            {
                SetName();
            }
        }


        protected override void OnMouseDown( Event e )
        {
            if ( m_label.PointInControl( e.mousePosition ) )
            {
                if ( BoundControlSelected != null && m_target != null )
                {
                    BoundControlSelected( m_target );
                }

                e.Use();
            }
        }
    }
}