using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using UForms.Controls;
using UForms.Controls.Fields;
using UForms.Decorators;


namespace UForms.Designer
{
    public class DesignerTopMenu : Control
    {
        public delegate void OptionSelected( MenuOption option );
        public event OptionSelected MenuOptionSelected;

        private static readonly Dictionary< string, MenuOption > OPTIONS = new Dictionary<string, MenuOption>()
        {
            { "New",             MenuOption.New             },
            { "Load",            MenuOption.Load            },
            { "Save",            MenuOption.Save            },
            { "Show Toolbox",    MenuOption.ShowToolbox     },
        };


        public enum MenuOption
        {
            New,
            Load,
            Save,
            ShowToolbox,
        }


        private Dictionary< Button, MenuOption > m_menuOptions;

        public DesignerTopMenu()
        {
            AddDecorator( new StackContent( StackContent.StackMode.Horizontal, StackContent.OverflowMode.Flow ) );

            m_menuOptions = new Dictionary<Button, MenuOption>();

            foreach( string key in OPTIONS.Keys )
            {
                Button button = new Button( key );
                button.SetHeight( 95.0f, MetricsUnits.Percentage );
                button.Clicked += HandleOptionSelected;

                m_menuOptions.Add( button, OPTIONS[ key ] );
                AddChild( button );
            }
        }

        void HandleOptionSelected( Events.IClickable sender, Events.ClickEventArgs args, Event nativeEvent )
        {
            if ( sender is Button )
            {
                Button button = sender as Button;

                if ( args.button == Events.MouseButton.Left && m_menuOptions.ContainsKey( button ) )
                {
                    if ( MenuOptionSelected != null )
                    {
                        MenuOptionSelected( m_menuOptions[ button ] );
                    }
                }
            }            
        }
    }
}