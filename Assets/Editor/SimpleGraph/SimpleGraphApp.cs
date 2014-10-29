using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Application;
using UForms.Controls;
using UForms.Decorators;

public class SimpleGraphApp : UFormsApplication 
{
    [MenuItem( "Test/Simple Graph App" ) ]
    private static void Run()
    {
        EditorWindow.GetWindow< SimpleGraphApp >();
    }

    private SimpleGraph     m_graph;

    protected override void OnInitialize()
    {
        m_graph = new SimpleGraph();
        m_graph.AddDecorator( new Scrollbars( true, true, true ) );
        m_graph.SetSize( 100.0f, 100.0f, Control.MetricsUnits.Percentage, Control.MetricsUnits.Percentage );

        AddChild( m_graph );

        m_graph.AddNode( new SimpleGraphNode( 0, 1, "Root Node" ) );
        m_graph.AddNode( new SimpleGraphNode( 1, 1, "Passthrough Node" ) );
        m_graph.AddNode( new SimpleGraphNode( 1, 4, "Branch Node" ) );
        m_graph.AddNode( new SimpleGraphNode( 1, 0, "Output Node" ) );
    }
}
