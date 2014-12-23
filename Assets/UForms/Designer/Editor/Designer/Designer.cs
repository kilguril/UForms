using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Application;
using UForms.Controls;
using UForms.Decorators;

using UForms.Designer;
using System;

namespace UForms.Designer
{
    public class UFormsDesigner : UFormsApplication
    {
        private const float CONTROL_DISPLAY_HEIGHT = 22.0f;

        [MenuItem( "UForms Designer/Control Designer" )]
        private static void Run()
        {
            EditorWindow.GetWindow<UFormsDesigner>();
        }

        private DesignerTopMenu     m_menu;
        private Control             m_workarea;
        private Toolbox             m_toolbox;
        private Inspector           m_inspector;

        private Control             m_selectedControl;
        private Control             m_root;

        private bool                m_dragging;

        private Vector2             m_viewportOffset = new Vector2( 0.0f, CONTROL_DISPLAY_HEIGHT );

        public void AddChildControl( Type controlType )
        {
            if ( m_root != null )
            {
                m_root.AddChild( ( Control )Activator.CreateInstance( controlType ) );
            }
        }        


        protected override void OnInitialize()
        {            
            title = "UForms Designer";
            
            m_menu = new DesignerTopMenu();
            m_menu.SetSize( 100.0f, CONTROL_DISPLAY_HEIGHT, Control.MetricsUnits.Percentage, Control.MetricsUnits.Pixel );
            m_menu.MenuOptionSelected += HandleMenuOptionSelected;

            AddChild( m_menu );

            m_workarea = new Control();
            m_workarea.SetPosition( 0.0f, CONTROL_DISPLAY_HEIGHT );
            m_workarea.AddDecorator( new ClipContent() );

            AddChild( m_workarea );

            SetSelectedControl( null );

            ShowToolbox();
            ShowInspector();
        }


        protected override void Update()
        {
            if ( m_workarea != null )
            {
                m_workarea.SetSize( 100.0f, position.height - CONTROL_DISPLAY_HEIGHT, Control.MetricsUnits.Percentage, Control.MetricsUnits.Pixel );
            }

            base.Update();

            Repaint();
        }


        // Custom gui step for object picking
        protected override void OnGUI()
        {
            if ( m_root != null )
            {
                Event e = Event.current;

                if ( e.type == EventType.mouseDown )
                {
                    bool selected = false;
                    Control target = null;

                    for ( int i = m_root.Children.Count - 1; i >= 0; i-- )                        
                    {
                        if ( m_root.Children[ i ].PointInControl( e.mousePosition - m_viewportOffset ) )
                        {
                            selected = true;
                            target = m_root.Children[ i ];

                            m_dragging = true;

                            e.Use();
                            break;
                        }
                    }

                    if ( e.button == 0 )
                    {
                        m_dragging = true;

                        if ( selected )
                        {
                            SetSelectedControl( target );
                        }
                        else
                        {
                            SetSelectedControl( m_root );
                        }
                    }

                    if ( e.button == 1 )
                    {
                        m_dragging = false;

                        if ( target != null && target != m_root )
                        {
                            if ( m_selectedControl == target )
                            {
                                SetSelectedControl( m_root );
                            }

                            m_root.RemoveChild( target );
                            target = null;
                        }
                    }

                    Focus();
                }

                if ( e.type == EventType.mouseDrag)
                {
                    if ( m_selectedControl != null && m_dragging )
                    {
                        m_selectedControl.Position += e.delta;
                        m_root.Dirty = true;
                    }

                    Focus();
                }

                if ( e.type == EventType.mouseUp )
                {
                    m_dragging = false;
                }
            }

            base.OnGUI();
        }


        void OnDestroy()
        {
            if ( m_toolbox != null )
            {
                m_toolbox.Close();
            }

            if ( m_inspector != null )
            {
                m_inspector.Close();
            }
        }


        private void NewControl()
        {
            if ( m_root != null )
            {
                if ( !EditorUtility.DisplayDialog( "Confirm New Control", "Are you sure you would like to create a new control? Current layout will be discarded!", "Ok", "Cancel" ) )
                {
                    return;
                }

                foreach( Control child in m_root.Children )
                {
                    RemoveChild( child );
                }

                RemoveChild( m_root );

                if ( m_inspector != null )
                {
                    m_inspector.SetSelectedControl( null );
                }
            }

            m_root = new Control();

            m_root.SetSize( 100.0f, 100.0f );
            m_root.SetPosition( position.width / 2.0f - 50.0f, position.height / 2.0f - 50.0f );
            m_root.AddDecorator( new BackgroundColor( Color.gray ) );

            m_workarea.AddChild( m_root );
            SetSelectedControl( m_root );
        }


        private void ShowToolbox()
        {
            if ( m_toolbox == null )
            {
                m_toolbox = EditorWindow.GetWindow<Toolbox>( "Toolbox", typeof( Inspector ) );
                m_toolbox.SetDesignerContext( this );
                Focus();
            }
            else
            {
                m_toolbox.Focus();
            }
        }


        private void ShowInspector()
        {
            if ( m_inspector == null )
            {
                m_inspector = EditorWindow.GetWindow<Inspector>( "Inspector", typeof( Toolbox ) );
                m_inspector.SetSelectedControl( m_selectedControl );
                Focus();
            }
            else
            {
                m_inspector.Focus();
            }
        }


        private void SetSelectedControl( Control control )
        {
            if ( m_selectedControl != control )
            {
                m_selectedControl = control;

                if ( m_inspector != null )
                {
                    m_inspector.SetSelectedControl( m_selectedControl );
                }
            }
        }        


        void HandleMenuOptionSelected( DesignerTopMenu.MenuOption option )
        {
            switch ( option )
            {
                case DesignerTopMenu.MenuOption.ShowToolbox:
                    ShowToolbox();
                break;

                case DesignerTopMenu.MenuOption.ShowInspector:
                    ShowInspector();
                break;

                case DesignerTopMenu.MenuOption.New:
                    NewControl();
                break;
            }
        }
    }
}