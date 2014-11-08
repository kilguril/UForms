using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;
using UForms.Decorators;
using UForms.Graphics;
using UForms.Events;


public class SimpleGraphConnection
{
    public SimpleGraphNode     from;
    public int                 fromIndex;
    public SimpleGraphNode     to;
    public int                 toIndex;

    public BezierCurve         line;
}


public class SimpleGraph : Control
{
    public List<SimpleGraphNode> Nodes { get; private set; }
  
    private GraphicsCanvas          m_canvas;
    private SimpleGraphConnection   m_currentConnection;

    private List< SimpleGraphConnection > m_connections;

    public SimpleGraph() : base()
    {
        Nodes = new List<SimpleGraphNode>();
        m_connections = new List<SimpleGraphConnection>();
        m_canvas = (GraphicsCanvas)AddDecorator( new GraphicsCanvas() );
    }


    public void AddNode( SimpleGraphNode node )
    {
        Nodes.Add( node );
        AddChild( node );

        ContextMenuControl nodeContext = ( ContextMenuControl )node.AddChild( new ContextMenuControl() );
        nodeContext.Menu.AddItem( new GUIContent( "Remove" ), false, RemoveNodeContext, node );

        node.ConnectRequest += OnConnectRequest;
        node.BreakRequest += OnBreakRequest;
        node.DragMoved += OnNodeDragMoved;
    }


    protected override void OnMouseMove( Event e )
    {
        UpdateLines( ViewportToAbsolutePosition( e.mousePosition ) );
    }


    protected override void OnMouseDrag( Event e )
    {        
        UpdateLines( ViewportToAbsolutePosition( e.mousePosition ) );
    }


    protected override void OnMouseDown( Event e )
    {
        if ( m_currentConnection != null )
        {
            if ( e.button == 1 )
            {
                BreakCurrentConnection();
                e.Use();
            }
        }
    }


    void OnBreakRequest( SimpleGraphNode node, int connectorIndex, bool origin )
    {
        BreakCurrentConnection();
        BreakConnection( node, connectorIndex, origin );
    }


    void OnConnectRequest( SimpleGraphNode node, int connectorIndex, bool origin )
    {
        if ( m_currentConnection == null )
        {
            NewConnection( node, connectorIndex, origin );
        }
        else
        {
            UpdateConnection( node, connectorIndex, origin );
        }
    }


    private void BreakConnection( SimpleGraphNode node, int connectorIndex, bool origin )
    {
        for ( int i = m_connections.Count - 1; i >= 0; i-- )
        {
            if ( ( origin && m_connections[ i ].from == node && m_connections[ i ].fromIndex == connectorIndex )
            || ( !origin && m_connections[ i ].to == node && m_connections[ i ].toIndex == connectorIndex ) )
            {
                m_canvas.RemoveShape( m_connections[ i ].line );
                m_connections.RemoveAt( i );
            }
        }
    }


    private void BreakCurrentConnection()
    {
        if ( m_currentConnection != null )
        {
            m_canvas.RemoveShape( m_currentConnection.line );
            m_currentConnection = null;
        }
    }


    private void NewConnection( SimpleGraphNode node, int connectorIndex, bool origin )
    {
        m_currentConnection = new SimpleGraphConnection();

        if ( origin )
        {
            m_currentConnection.from = node;
            m_currentConnection.fromIndex = connectorIndex;
        }
        else
        {
            m_currentConnection.to = node;
            m_currentConnection.toIndex = connectorIndex;
        }

        m_currentConnection.line = new BezierCurve( Vector2.zero, Vector2.zero, Color.red );
        m_currentConnection.line.Tangents = BezierCurve.TangentMode.AutoY;
        m_canvas.AddShape( m_currentConnection.line );
    }


    private void UpdateConnection( SimpleGraphNode node, int connectorIndex, bool origin )
    {
        if ( origin )
        {
            m_currentConnection.from = node;
            m_currentConnection.fromIndex = connectorIndex;

            if ( m_currentConnection.from == m_currentConnection.to )
            {
                m_currentConnection.to = null;
            }
        }
        else
        {
            m_currentConnection.to = node;
            m_currentConnection.toIndex = connectorIndex;

            if ( m_currentConnection.from == m_currentConnection.to )
            {
                m_currentConnection.from = null;
            }
        }

        if ( m_currentConnection.from != null && m_currentConnection.to != null )
        {
            BreakConnection( m_currentConnection.from, m_currentConnection.fromIndex, true );
            BreakConnection( m_currentConnection.to, m_currentConnection.toIndex, false );

            m_connections.Add( m_currentConnection );
            m_currentConnection = null;
        }
    }


    private void UpdateLines( Vector2 mousePos )
    {
        if ( m_currentConnection != null )
        {
            BezierCurve line = m_currentConnection.line;

            line.From = m_currentConnection.from == null ? mousePos : m_currentConnection.from.OutputConnectors.GetConnectionPoint( m_currentConnection.fromIndex );
            line.To   = m_currentConnection.to   == null ? mousePos : m_currentConnection.to.InputConnectors.GetConnectionPoint( m_currentConnection.toIndex );
        }

        foreach( SimpleGraphConnection conn in m_connections )
        {
            BezierCurve line = conn.line;

            line.From = conn.from == null ? mousePos : conn.from.OutputConnectors.GetConnectionPoint( conn.fromIndex );
            line.To = conn.to == null ? mousePos : conn.to.InputConnectors.GetConnectionPoint( conn.toIndex );
        }

        Dirty = true;
    }


    public void RemoveNode( SimpleGraphNode node )
    {
        Nodes.Remove( node );
        RemoveChild( node );
        
        node.DragMoved -= OnNodeDragMoved;
    }


    void OnNodeDragMoved( IDraggable sender, DragEventArgs args, Event nativeEvent )
    {
        if ( sender is SimpleGraphNode )
        {
            SimpleGraphNode node = sender as SimpleGraphNode;
            node.Position += args.delta;
        }
    }


    private void RemoveNodeContext( object arg )
    {
        SimpleGraphNode node = arg as SimpleGraphNode;

        if ( node != null )
        {
            RemoveNode( node );
        }
    }
}
