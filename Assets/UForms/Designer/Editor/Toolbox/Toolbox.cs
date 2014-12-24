using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Application;
using UForms.Controls;
using UForms.Controls.Extended;
using UForms.Decorators;

using CachedControl = UForms.Designer.ToolboxControlCache.CachedControl;
using System;

namespace UForms.Designer
{
    public class Toolbox : UFormsApplication
    {
        private const float CONTROL_DISPLAY_HEIGHT = 22.0f;

        private ToolboxControlCache                     m_controlsCache;
        private Control                                 m_root;

        private Dictionary< Button, CachedControl >     m_buttonMapping;        // Maps button to underlying cached control infomation        

        private UFormsDesigner                          m_designer;


        public void SetDesignerContext( UFormsDesigner designer )
        {
            m_designer = designer;
        }

        protected override void OnInitialize()
        {
            title = "Toolbox";

            m_buttonMapping = new Dictionary<Button, CachedControl>();
            m_controlsCache = new ToolboxControlCache();

            m_root = new Control();
            m_root.AddDecorator( new StackContent( StackContent.StackMode.Vertical, StackContent.OverflowMode.Flow ) );
            m_root.AddDecorator( new Scrollbars( true, false, true ) );
            m_root.SetSize( 100.0f, 100.0f, Control.MetricsUnits.Percentage, Control.MetricsUnits.Percentage );

            // Create category foldouts, index them by name so we can assign our controls
            Dictionary< string, FoldoutList > foldouts = new Dictionary<string, FoldoutList>();

            foreach( string category in m_controlsCache.Categories )
            {
                FoldoutList foldout = new FoldoutList( category, 4.0f, true );
                foldout.SetWidth( 100.0f, Control.MetricsUnits.Percentage );
                m_root.AddChild( foldout );

                foldouts.Add( category, foldout );
            }

            foreach( CachedControl c in m_controlsCache.Controls )
            {
                Button button = new Button( c.name );
                button.SetSize( 100.0f, CONTROL_DISPLAY_HEIGHT, Control.MetricsUnits.Percentage, Control.MetricsUnits.Pixel );
                button.Clicked += HandleControlButtonClick;

                m_buttonMapping.Add( button, c );

                foldouts[ c.category ].AddItem( button );
            }

            AddChild( m_root );
        }


        void HandleControlButtonClick( Events.IClickable sender, Events.ClickEventArgs args, Event nativeEvent )
        {
            if ( sender is Button )
            {
                Button key = sender as Button;

                if ( m_buttonMapping.ContainsKey( key ) )
                {
                    Type type = m_buttonMapping[ key ].type;
                    
                    if ( m_buttonMapping[ key ].decorator )
                    {
                        m_designer.AddChildDecorator( type );
                    }
                    else
                    {
                        m_designer.AddChildControl( type );
                    }
                }
            }
        }
    }
}