using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using UForms.Controls;
using UForms.Controls.Fields;
using UForms.Decorators;


namespace UForms.Designer
{
    public class DecoratorItem : Control
    {
        public delegate void DecoratorSelected( Decorator decorator, int button );
        public event DecoratorSelected BoundDecoratorlSelected;

        protected override Vector2 DefaultSize
        {
            get
            {
                return new Vector2( 200.0f, 16.0f );
            }
        }

        private Label m_label;

        private Decorator m_target;


        private const float     LEVEL_INDENT = 12.0f;        

        public DecoratorItem( Decorator target ) : base()
        {
            m_label = new Label();
            m_label.SetWidth( 100.0f, MetricsUnits.Percentage );
            m_label.SetHeight( 16.0f );
            AddChild( m_label );

            SetWidth( 100.0f, MetricsUnits.Percentage );

            Bind( target );
        }


        public void Bind( Decorator control )
        {
            m_target = control;
            SetName();
        }


        public void Release()
        {
            m_target = null;
        }


        private void SetName()
        {
            string type    = m_target.GetType().ToString();

            int lastDot    = type.LastIndexOf( '.' );
            if ( lastDot >= 0 )
            {
                type = type.Substring( lastDot + 1, type.Length - lastDot - 1 );
            }

            string display = string.Format( ":: {0}", type );

            m_label.Text = display;
        }


        protected override void OnMouseDown( Event e )
        {
            if ( m_label.PointInControl( e.mousePosition ) )
            {
                if ( BoundDecoratorlSelected != null && m_target != null )
                {
                    BoundDecoratorlSelected( m_target, e.button );
                }

                e.Use();
            }            
        }
    }
}