using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Application;
using UForms.Controls;
using UForms.Decorators;

using UForms.Designer;
using System;
using UForms.Controls.Fields;
using UForms.Controls.Dropdowns;
using UForms.Events;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;

namespace UForms.Designer
{
    public class UFormsDesigner : UFormsApplication 
    {
        private const float CONTROL_DISPLAY_HEIGHT = 22.0f;
        private const float TOP_MENU_SPACING       = 10.0f;
        private const float INSPECTOR_WIDTH        = 250.0f;
        private const float HIERARCHY_WIDTH        = 250.0f;
        private const float SIDE_MARGIN            = 8.0f;
        private const float RESIZE_HANDLE_SIZE     = 10.0f;

        [MenuItem( "UForms Designer/Control Designer" )]
        private static void Run()
        {
            EditorWindow.GetWindow<UFormsDesigner>();
        }

        private UFormsCodeGenerator m_generator;

        private DesignerTopMenu     m_menu;
        private Control             m_workarea;
        private Control             m_inspector;
        private Control             m_hierarchy;
        private Toolbox             m_toolbox;

        private Control             m_resizeHandle;
        private Control             m_selectedControl;
        private Control             m_root;

        private object              m_inspectorTarget;

        private bool                m_resizing;
        private bool                m_dragging;
        private bool                m_pixelSnap;

        private Dictionary< object, PropertyInfo >   m_inspectorFields;
        private Dictionary< Control, HierarchyItem > m_hierarchyItems;

        private Vector2             m_viewportOffset = new Vector2( HIERARCHY_WIDTH, CONTROL_DISPLAY_HEIGHT );


        public void AddChildDecorator( Type decoratorType )
        {
            if ( m_root != null && m_selectedControl != null )
            {
                Decorator decorator = ( Decorator )Activator.CreateInstance( decoratorType );
                m_selectedControl.AddDecorator( decorator );

                UpdateHierarchyDecoratorData( m_selectedControl );
            }
        }


        public void RemoveChildDecorator( Control control, Decorator decorator )
        {
            if ( control != null && decorator != null )
            {
                control.RemoveDecorator( decorator );
                UpdateHierarchyDecoratorData( control );
            }
        }


        public void AddChildControl( Type controlType )
        {
            if ( m_root != null )
            {
                Control control = ( Control )Activator.CreateInstance( controlType ); 
                m_root.AddChild( control );
                CreateHierarchyEntry( control, 1 );
            }
        }    
    

        public void RemoveChildControl( Control control )
        {
            if ( m_root != null )
            {
                RemoveHierarchyEntry( control );
                m_root.RemoveChild( control );
            }
        }


        public void UpdateHierarchyDecoratorData( Control control )
        {
            if ( m_hierarchyItems.ContainsKey( control ) )
            {
                m_hierarchyItems[ control ].UpdateDecoratorData( control );
            }
        }


        private void CreateHierarchyEntry( Control control, int level )
        {
            HierarchyItem item = new HierarchyItem( control, level );
            m_hierarchyItems.Add( control, item );

            item.BoundControlSelected += item_BoundControlSelected;
            item.BoundDecoratorlSelected += item_BoundDecoratorlSelected;

            m_hierarchy.AddChild( item );
        }


        private void RemoveHierarchyEntry( Control control )
        {
            if ( m_hierarchyItems.ContainsKey( control ) )
            {
                m_hierarchyItems[ control ].Release();
                m_hierarchyItems[ control ].BoundControlSelected -= item_BoundControlSelected;
                m_hierarchyItems[ control ].BoundDecoratorlSelected -= item_BoundDecoratorlSelected;

                m_hierarchy.RemoveChild( m_hierarchyItems[ control ] );
                m_hierarchyItems.Remove( control );
            }
        }


        void item_BoundDecoratorlSelected( Control control, Decorator decorator, int button )
        {
            if ( button == 1 )
            {
                RemoveChildDecorator( control, decorator );
            }
            else if ( button == 0 )
            {
                SetInspectorTarget( decorator );
            }
        }


        void item_BoundControlSelected( Control control )
        {
            SetSelectedControl( control );
            SetInspectorTarget( control );
        }


        protected override void OnInitialize()
        {
            m_generator = new UFormsCodeGenerator();

            title = "Control Designer";
            
            m_menu = new DesignerTopMenu();
            m_menu.SetSize( 100.0f, CONTROL_DISPLAY_HEIGHT, Control.MetricsUnits.Percentage, Control.MetricsUnits.Pixel );
            m_menu.MenuOptionSelected += HandleMenuOptionSelected;

            AddChild( m_menu );

            m_inspectorFields = new Dictionary<object, PropertyInfo>();
            m_hierarchyItems = new Dictionary<Control, HierarchyItem>();

            m_inspector = new Control();
            m_inspector.SetPosition( position.width - INSPECTOR_WIDTH , CONTROL_DISPLAY_HEIGHT + TOP_MENU_SPACING );
            m_inspector.SetWidth( INSPECTOR_WIDTH );
            m_inspector.SetMargin( 0.0f, 0.0f, SIDE_MARGIN, 0.0f );
            m_inspector.AddDecorator( new StackContent( StackContent.StackMode.Vertical, StackContent.OverflowMode.Flow ) );
            AddChild( m_inspector );

            m_hierarchy = new Control();
            m_hierarchy.SetPosition( 0.0f, CONTROL_DISPLAY_HEIGHT + TOP_MENU_SPACING );
            m_hierarchy.SetWidth( HIERARCHY_WIDTH );
            m_hierarchy.SetMargin( SIDE_MARGIN, 0.0f, 0.0f, 0.0f );
            m_hierarchy.AddDecorator( new StackContent( StackContent.StackMode.Vertical, StackContent.OverflowMode.Flow ) );
            AddChild( m_hierarchy );

            m_workarea = new Control();
            m_workarea.SetPosition( m_viewportOffset );
            m_workarea.AddDecorator( new ClipContent() );

            AddChild( m_workarea );

            m_resizeHandle = new Control();
            m_resizeHandle.SetSize( RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE );
            m_resizeHandle.AddDecorator( new BackgroundColor( Color.blue ) );
            m_resizeHandle.Visibility = Control.VisibilityMode.Hidden;

            AddChild( m_resizeHandle );

            SetSelectedControl( null );
            SetInspectorTarget( null );

            ShowToolbox();
        }


        protected override void Update()
        {
            if ( m_workarea != null )
            {
                m_workarea.SetSize( position.width - INSPECTOR_WIDTH - HIERARCHY_WIDTH, position.height - CONTROL_DISPLAY_HEIGHT );
            }

            if ( m_inspector != null )
            {
                m_inspector.SetPosition( position.width - INSPECTOR_WIDTH, CONTROL_DISPLAY_HEIGHT + TOP_MENU_SPACING );
                m_inspector.SetHeight( position.height - CONTROL_DISPLAY_HEIGHT - TOP_MENU_SPACING );
            }

            if ( m_hierarchy != null )
            {
                m_hierarchy.SetHeight( position.height - CONTROL_DISPLAY_HEIGHT - TOP_MENU_SPACING );
            }

            if ( m_resizeHandle != null && m_selectedControl != null )
            {
                float x = m_selectedControl.ScreenRect.x + m_selectedControl.Size.x - m_resizeHandle.Size.x / 2.0f + m_viewportOffset.x;
                float y = m_selectedControl.ScreenRect.y + m_selectedControl.Size.y - m_resizeHandle.Size.y / 2.0f + m_viewportOffset.y;

                m_resizeHandle.SetPosition( x, y );
            }

            // Ugliest hack possible, used to update the position field continiously as it is modifiable in the designer
            if ( m_inspectorFields != null )
            {
                if ( m_inspectorFields.Count > 0 && m_selectedControl != null )
                {
                    foreach ( KeyValuePair<object,PropertyInfo> kvp in m_inspectorFields )
                    {
                        if ( kvp.Value.Name == "Position" || kvp.Value.Name == "Size" )
                        {
                            ( kvp.Key as Vector2Field ).Value = ( Vector2 )kvp.Value.GetValue( m_selectedControl, null );                            
                        }
                    }
                }
            }

            base.Update();

            Repaint();
        }


        // Custom gui step for object picking
        protected override void OnGUI()
        {
            Event e = Event.current;

            if ( m_root != null )
            {                
                if ( e.type == EventType.mouseDown )
                {
                    if ( m_resizeHandle.PointInControl( e.mousePosition ) )
                    {
                        m_resizing = true;
                        m_dragging = false;

                    }
                    else if ( m_workarea.PointInControl( e.mousePosition ) )
                    {
                        m_resizing = false;

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
                                SetInspectorTarget( target );
                            }
                            else
                            {
                                SetSelectedControl( m_root );
                                SetInspectorTarget( m_root );
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
                                    SetInspectorTarget( m_root );
                                }

                                RemoveChildControl( target );
                                target = null;
                            }
                        }

                        Focus();
                    }
                    else
                    {
                        m_resizing = false;
                    }
                }

                if ( e.type == EventType.mouseDrag)
                {
                    if ( m_selectedControl != null && m_dragging )
                    {
                        m_selectedControl.Position += e.delta;
                        m_root.Dirty = true;
                    }
                    else if ( m_selectedControl != null && m_resizing )
                    {
                        m_selectedControl.Size += e.delta;
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
        }


        private void SaveControl()
        {
            if ( m_root != null )
            {                
                if ( m_generator.ValidateIDs( m_root ) )
                {
                    string path = "";
                    path = EditorUtility.SaveFilePanel( "Save Control", UnityEngine.Application.dataPath + "/Assets/", "", "cs" );

                    // Skip path validation for now because it's a serious pain in the ass
                    if ( !string.IsNullOrEmpty( path ) )
                    {
                        string layoutPath = m_generator.GenerateLayoutPath( path );
                        string layout     = m_generator.GenerateLayout( m_root );
                        string userCode   = m_generator.GenerateUseCode( m_root );

                        if ( File.Exists( layoutPath ) )
                        {
                            File.Delete( layoutPath );
                        }

                        File.WriteAllText( layoutPath, layout );

                        if ( !File.Exists( path ) )
                        {
                            File.WriteAllText( path, userCode );
                        }

                        AssetDatabase.Refresh();
                    }
                }
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
                    RemoveChildControl( child );                    
                }

                m_workarea.RemoveChild( m_root );
                RemoveHierarchyEntry( m_root );
            }

            m_root = new Control();

            m_root.SetSize( 100.0f, 100.0f );
            m_root.SetPosition( m_workarea.Size.x / 2.0f - 50.0f, m_workarea.Size.y / 2.0f - 50.0f );
            m_root.AddDecorator( new BackgroundColor( Color.gray ) );

            m_workarea.AddChild( m_root );
            SetSelectedControl( m_root );
            SetInspectorTarget( m_root );

            CreateHierarchyEntry( m_root, 0 );
        }


        private void TeardownInspectorDisplay()
        {
            if ( m_inspectorFields != null )
            {
                foreach ( Control field in m_inspectorFields.Keys )
                {
                    ( field as IEditable ).ValueChange -= Inspector_ValueChange;
                    m_inspector.RemoveChild( field );
                }

                m_inspectorFields.Clear();
            }
        }


        private void BuildInspectorDisplay( object control )
        {
            Type t = control.GetType();

            PropertyInfo[] props = t.GetProperties( BindingFlags.Public | BindingFlags.Instance );
            foreach ( PropertyInfo prop in props )
            {
                object[] attr = prop.GetCustomAttributes( typeof( HideInInspector ), false );

                if ( attr.Length == 0 )
                {
                    CreateField( prop, control );
                }
            }        
        }


        private void CreateField( PropertyInfo prop, object control )
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
                fieldControl = m_inspector.AddChild( new IntField( ( int )prop.GetValue( control, null ), displayName ) );
                lines = 1;
            }
            else if ( prop.PropertyType == typeof( float ) )
            {
                fieldControl = m_inspector.AddChild( new FloatField( ( float )prop.GetValue( control, null ), displayName ) );
                lines = 1;
            }
            else if ( prop.PropertyType == typeof( Vector2 ) )
            {
                fieldControl = m_inspector.AddChild( new Vector2Field( ( Vector2 )prop.GetValue( control, null ), displayName ) );
                lines = 2;
            }
            else if ( prop.PropertyType == typeof( Vector3 ) )
            {
                fieldControl = m_inspector.AddChild( new Vector3Field( ( Vector3 )prop.GetValue( control, null ), displayName ) );
                lines = 2;
            }
            else if ( prop.PropertyType == typeof( Vector4 ) )
            {
                fieldControl = m_inspector.AddChild( new Vector4Field( ( Vector4 )prop.GetValue( control, null ), displayName ) );
                lines = 2;
            }
            else if ( prop.PropertyType == typeof( string ) )
            {
                fieldControl = m_inspector.AddChild( new TextField( ( string )prop.GetValue( control, null ), displayName ) );
                lines = 1;
            }
            else if ( prop.PropertyType == typeof( Rect ) )
            {
                fieldControl = m_inspector.AddChild( new RectField( ( Rect )prop.GetValue( control, null ), displayName ) );
                lines = 3;
            }
            else if ( prop.PropertyType == typeof( Color ) )
            {
                fieldControl = m_inspector.AddChild( new ColorField( ( Color )prop.GetValue( control, null ), displayName ) );
                lines = 1;
            }
            else if ( prop.PropertyType == typeof( Bounds ) )
            {
                fieldControl = m_inspector.AddChild( new BoundsField( ( Bounds )prop.GetValue( control, null ), displayName ) );
                lines = 3;
            }
            else if ( prop.PropertyType.IsEnum )
            {
                fieldControl = m_inspector.AddChild( new EnumDropdown( ( System.Enum )prop.GetValue( control, null ), displayName ) );
                lines = 1;
            }
            else if ( prop.PropertyType == typeof( bool ) )
            {
                fieldControl = m_inspector.AddChild( new Toggle( displayName, ( bool )prop.GetValue( control, null ), false ) );
                lines = 1;
            }

            if ( fieldControl != null )
            {
                fieldControl.SetSize( 100.0f, 18.0f * lines, Control.MetricsUnits.Percentage, Control.MetricsUnits.Pixel );
                fieldControl.SetMargin( 5.0f, 0.0f, 5.0f, 0.0f );
                m_inspectorFields.Add( fieldControl, prop );

                ( fieldControl as IEditable ).ValueChange += Inspector_ValueChange;
            }
        }

        void Inspector_ValueChange( IEditable sender, EditEventArgs args, Event nativeEvent )
        {
            if ( sender is Control )
            {
                Control control = sender as Control;

                if ( m_inspectorFields.ContainsKey( control ) )
                {
                    m_inspectorFields[ control ].SetValue( m_inspectorTarget, args.newValue, null );
                }
            }
        }


        private void ShowToolbox()
        {
            if ( m_toolbox == null )
            {
                m_toolbox = EditorWindow.GetWindow<Toolbox>( "Toolbox" );
                m_toolbox.SetDesignerContext( this );
                Focus();
            }
            else
            {
                m_toolbox.Focus();
            }
        }


        private void SetSelectedControl( Control control )
        {
            m_selectedControl = control;

            if ( control != null )
            {
                    
                m_resizeHandle.Visibility = Control.VisibilityMode.Visible;
            }
            else
            {
                m_resizeHandle.Visibility = Control.VisibilityMode.Hidden;
            }
        }        


        private void SetInspectorTarget( object target )
        {            
            if ( m_inspectorFields != null )
            {
                TeardownInspectorDisplay();
            }

            if ( target != null )
            {
                BuildInspectorDisplay( target );
            }

            m_inspectorTarget = target;
        }


        void HandleMenuOptionSelected( DesignerTopMenu.MenuOption option )
        {
            switch ( option )
            {
                case DesignerTopMenu.MenuOption.ShowToolbox:
                    ShowToolbox();
                break;

                case DesignerTopMenu.MenuOption.New:
                    NewControl();
                break;

                case DesignerTopMenu.MenuOption.Save:
                    SaveControl();
                break;
            }
        }
    }
}