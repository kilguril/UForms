using UnityEngine;
using UnityEditor;

using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UForms.Application;
using UForms.Controls;
using UForms.Controls.Extended;
using UForms.Controls.Fields;
using UForms.Decorators;
using UForms.Controls.Dropdowns;
using System.Text.RegularExpressions;
using UForms.Events;

namespace UForms.Designer
{
    public class Inspector : UFormsApplication
    {
        private Control m_selectedControl;
        private Control m_root;

        private Dictionary< Control, PropertyInfo > m_propertyFields;

        private bool    m_isInit;


        protected override void Update()
        {
            base.Update();

            // Ugliest hack possible, used to update the position field continiously as it is modifiable in the designer
            if ( m_propertyFields != null )
            {
                if ( m_propertyFields.Count > 0 && m_selectedControl != null )
                {
                    foreach ( KeyValuePair<Control,PropertyInfo> kvp in m_propertyFields )
                    {
                        if ( kvp.Value.Name == "Position" )
                        {
                            ( kvp.Key as Vector2Field ).Value = ( Vector2 )kvp.Value.GetValue( m_selectedControl, null );
                            break;
                        }
                    }

                    Repaint();
                }
            }
        }

        public void SetSelectedControl( Control control )
        {
            if ( m_selectedControl != control )
            {
                TeardownDisplay();
            }

            if ( control != null )
            {
                BuildDisplay( control );
            }

            m_selectedControl = control;
        }


        protected override void OnInitialize()
        {
            if ( !m_isInit )
            {
                Init();
            }

            AddChild( m_root );
        }


        private void Init()
        {
            m_propertyFields = new Dictionary<Control, PropertyInfo>();

            title = "Inspector";

            m_root = new Control();
            m_root.SetSize( 100.0f, 100.0f, Control.MetricsUnits.Percentage, Control.MetricsUnits.Percentage );

            m_root.AddDecorator( new Scrollbars( true, false, true ) );
            m_root.AddDecorator( new StackContent( StackContent.StackMode.Vertical, StackContent.OverflowMode.Flow ) );

            m_isInit = true;
        }


        private void TeardownDisplay()
        {
            if ( m_propertyFields != null )
            {
                foreach ( Control field in m_propertyFields.Keys )
                {
                    ( field as IEditable ).ValueChange -= Inspector_ValueChange;
                    m_root.RemoveChild( field );
                }

                m_propertyFields.Clear();
            }
        }


        private void BuildDisplay( Control control )
        {
            if ( !m_isInit )
            {
                Init();
            }

            Type t = control.GetType();

            PropertyInfo[] props = t.GetProperties( BindingFlags.Public | BindingFlags.Instance );
            foreach( PropertyInfo prop in props )
            {                
                object[] attr = prop.GetCustomAttributes( typeof( HideInInspector ), false );

                if ( attr.Length == 0 )
                {
                    CreateField( prop, control );                    
                }
            }

            Focus();
        }


        private void CreateField( PropertyInfo prop, Control control )
        {
            string displayName = prop.Name;

            // Remove special characters
            string[] classFrags = displayName.Split( '.' );
            if ( classFrags.Length != 0 )
            {
                displayName = classFrags[ classFrags.Length - 1 ];
            }

            string[] specialFrags = displayName.Split( '+' );
            if ( specialFrags.Length != 0 )
            {
                displayName = specialFrags[ specialFrags.Length - 1 ];
            }

            // Add spacing between capitals
            displayName = Regex.Replace( displayName, "([a-z])([A-Z])", "$1 $2" );

            Control fieldControl = null;
            int     lines        = 0;

            if ( prop.PropertyType == typeof( int ) )
            {
                fieldControl = m_root.AddChild( new IntField( (int)prop.GetValue( control, null ), displayName ) );                
                lines = 1;
            }
            else if ( prop.PropertyType == typeof( float ) )
            {
                fieldControl = m_root.AddChild( new FloatField( (float)prop.GetValue( control, null ), displayName ) );
                lines = 1;
            }
            else if ( prop.PropertyType == typeof( Vector2 ) )
            {
                fieldControl = m_root.AddChild( new Vector2Field( (Vector2)prop.GetValue( control, null ), displayName ) );
                lines = 2;
            }
            else if ( prop.PropertyType == typeof( Vector3 ) )
            {
                fieldControl = m_root.AddChild( new Vector3Field( (Vector3)prop.GetValue( control, null ), displayName ) );
                lines = 2;
            }
            else if ( prop.PropertyType == typeof( Vector4 ) )
            {
                fieldControl = m_root.AddChild( new Vector4Field( (Vector4)prop.GetValue( control, null ), displayName ) );
                lines = 2;
            }
            else if ( prop.PropertyType == typeof( string ) )
            {
                fieldControl = m_root.AddChild( new TextField( (string)prop.GetValue( control, null ), displayName ) );
                lines = 1;
            }
            else if ( prop.PropertyType == typeof( Rect ) )
            {
                fieldControl = m_root.AddChild( new RectField( (Rect)prop.GetValue( control, null ), displayName ) );
                lines = 3;
            }
            else if ( prop.PropertyType == typeof( Color ) )
            {
                fieldControl = m_root.AddChild( new ColorField( (Color)prop.GetValue( control, null ), displayName ) );
                lines = 1;
            }
            else if ( prop.PropertyType == typeof( Bounds ) )
            {
                fieldControl = m_root.AddChild( new BoundsField( (Bounds)prop.GetValue( control, null ), displayName ) );
                lines = 3;
            }
            else if ( prop.PropertyType.IsEnum )
            {
                fieldControl = m_root.AddChild( new EnumDropdown( ( System.Enum )prop.GetValue( control, null ), displayName ) );
                lines = 1;
            }
            else if ( prop.PropertyType == typeof( bool ) )
            {
                fieldControl = m_root.AddChild( new Toggle( displayName, (bool)prop.GetValue( control, null ), false ) );
                lines = 1;
            }

            if ( fieldControl != null )
            {
                fieldControl.SetSize( 100.0f, 18.0f * lines, Control.MetricsUnits.Percentage, Control.MetricsUnits.Pixel );
                fieldControl.SetMargin( 5.0f, 0.0f, 5.0f, 0.0f );
                m_propertyFields.Add( fieldControl, prop );

                ( fieldControl as IEditable ).ValueChange += Inspector_ValueChange;
            }
        }

        void Inspector_ValueChange( IEditable sender, EditEventArgs args, Event nativeEvent )
        {
            if ( sender is Control )
            {
                Control control = sender as Control;

                if ( m_propertyFields.ContainsKey( control ) )
                {
                    m_propertyFields[ control ].SetValue( m_selectedControl, args.newValue, null );
                }
            }
        }
    }
}