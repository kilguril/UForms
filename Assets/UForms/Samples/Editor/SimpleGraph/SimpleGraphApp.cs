using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Application;
using UForms.Controls;
using UForms.Decorators;

public class SimpleGraphApp : UFormsApplication 
{
    [MenuItem( "UForms Samples/Simple Graph" ) ]
    private static void Run()
    {
        SimpleGraphApp app =EditorWindow.GetWindow<SimpleGraphApp>();
        app.title = "Simple Graph";
    }


    enum ContextActionType
    {
        CreateRoot,
        CreatePassthrough,
        CreateBranch,
        CreateOutput
    }

    private SimpleGraph             m_graph;
    private ContextMenuControl      m_contextMenu;

    private Vector2                 m_lastMousePosition;    // We'll use this to create nodes from context menu

    protected override void OnInitialize()
    {
        wantsMouseMove = true;

        m_graph = new SimpleGraph();
        m_graph.AddDecorator( new Scrollbars( true, true, true ) );
        m_graph.SetSize( 100.0f, 100.0f, Control.MetricsUnits.Percentage, Control.MetricsUnits.Percentage );

        m_contextMenu = new ContextMenuControl();
        m_contextMenu.Menu.AddItem( new GUIContent( "Create Root" ), false, CreateNode, ContextActionType.CreateRoot );
        m_contextMenu.Menu.AddItem( new GUIContent( "Create Passthrough" ), false, CreateNode, ContextActionType.CreatePassthrough );
        m_contextMenu.Menu.AddItem( new GUIContent( "Create Branch" ), false, CreateNode, ContextActionType.CreateBranch );
        m_contextMenu.Menu.AddItem( new GUIContent( "Create Output" ), false, CreateNode, ContextActionType.CreateOutput );

        Label helpLabel = new Label( "Simple graph sample:\n\nRight click to bring up new node menu.\nLeft click to create connections.\nRight click to break connections." );
        helpLabel.SetSize( 300.0f, 100.0f );
        helpLabel.SetMargin( 5.0f, 5.0f, 5.0f, 5.0f );

        AddChild( m_graph );
        AddChild( m_contextMenu );
        AddChild( helpLabel );
    }

    private void CreateNode( object arg )
    {        
        ContextActionType t = ( ContextActionType )arg;

        int     inputs = 0;
        int     outputs = 0;
        string  text = "";

        switch( t )
        {
            case ContextActionType.CreateRoot:            
                inputs = 0;
                outputs = 1;
                text = "Root";
            break;

            case ContextActionType.CreatePassthrough:
                inputs = 1;
                outputs = 1;
                text = "Passthrough";
            break;

            case ContextActionType.CreateBranch:
                inputs = 1;
                outputs = 2;
                text = "Branch";
            break;

            case ContextActionType.CreateOutput:
                inputs = 1;
                outputs = 0;
                text = "Output";
            break;
        }

        SimpleGraphNode node = new SimpleGraphNode( inputs, outputs, text );
        m_graph.AddNode( node );

        node.SetPosition( m_graph.ViewportToAbsolutePosition( m_lastMousePosition ) );
    }

    protected override void OnGUI()
    {
        m_lastMousePosition = Event.current.mousePosition;
        base.OnGUI();
    }
}
