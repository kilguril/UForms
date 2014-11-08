using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;
using UForms.Decorators;
using UForms.Events;

public class SimpleGraphNode : Control, IDraggable
{
    public delegate void Connect( SimpleGraphNode node, int connectorIndex, bool origin );

    public event Connect ConnectRequest;
    public event Connect BreakRequest;

    public event Drag DragStarted;
    public event Drag DragMoved;
    public event Drag DragEnded;

    public SimpleGraphConnectors        InputConnectors { get; private set; }
    public SimpleGraphConnectors        OutputConnectors { get; private set; }

    public string Text { get; set; }

    protected override Vector2 DefaultSize
    {
        get
        {
            return new Vector2( 100.0f, 45.0f );
        }
    }


    private bool        m_isDragging;
    private Vector2     m_dragStartPosition;
    private Vector2     m_dragCurrentPosition;


    public SimpleGraphNode( int inputs, int outputs, string text )
    {
        Text = text;

        InputConnectors  = new SimpleGraphConnectors( inputs );
        OutputConnectors = new SimpleGraphConnectors( outputs );

        InputConnectors.OnConnectorClicked += OnInputClicked;
        OutputConnectors.OnConnectorClicked += OnOutputClicked;

        AddChild( InputConnectors );
        AddChild( OutputConnectors );

        m_isDragging = false;
    }

    void OnOutputClicked( int index, MouseButton mouseButton )
    {
        if ( mouseButton == MouseButton.Left )
        {
            if ( ConnectRequest != null )
            {
                ConnectRequest( this, index, true );
            }
        }
        else
        { 
            if ( BreakRequest != null )
            {
                BreakRequest( this, index, true );
            }
        }
    }

    void OnInputClicked( int index, MouseButton mouseButton )
    {
        if ( mouseButton == MouseButton.Left )
        {
            if ( ConnectRequest != null )
            {
                ConnectRequest( this, index, false );
            }
        }
        else
        {
            if ( BreakRequest != null )
            {
                BreakRequest( this, index, false );
            }
        }
    }
    

    protected override void OnAfterLayout()
    {
        InputConnectors.SetPosition( new Vector2( ( Size.x - InputConnectors.Size.x ) / 2.0f, 0.0f ) );
        OutputConnectors.SetPosition( new Vector2( ( Size.x - OutputConnectors.Size.x ) / 2.0f, Size.y - OutputConnectors.Size.y ) );
    }


    protected override void OnDraw()
    {
        Rect r      = ScreenRect;
        r.y         = r.y + InputConnectors.Size.y;
        r.height    = r.height - InputConnectors.Size.y - OutputConnectors.Size.y;

        GUI.Box( r, Text );
    }


    protected override void OnMouseDown( Event e )
    {
        base.OnMouseDown( e );

        if ( e != null && PointInControl( e.mousePosition ) && e.button == 0 )
        {
            m_isDragging = true;
            m_dragStartPosition = Bounds.position;
            m_dragCurrentPosition = m_dragStartPosition;

            if ( DragStarted != null )
            {
                DragStarted( this, new DragEventArgs( m_dragStartPosition, m_dragCurrentPosition, Vector2.zero ), e );
            }

            e.Use();
        }
    }


    protected override void OnMouseDrag( Event e )
    {
        base.OnMouseDrag( e );

        if ( m_isDragging )
        {
            m_dragCurrentPosition += e.delta;

            if ( DragMoved != null )
            {
                DragMoved( this, new DragEventArgs( m_dragStartPosition, m_dragCurrentPosition, e.delta ), e );
            }

            e.Use();
        }
    }


    protected override void OnMouseUp( Event e )
    {
        base.OnMouseUp( e );

        if ( m_isDragging )
        {
            m_isDragging = false;

            if ( DragEnded != null )
            {
                DragEnded( this, new DragEventArgs( m_dragStartPosition, m_dragCurrentPosition, Vector2.zero ), e );
            }

            e.Use();
        }
    }
}
