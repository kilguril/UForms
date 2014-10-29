using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;
using UForms.Decorators;
using UForms.Events;

public class SimpleGraphNode : Control, IDraggable
{
    public event Drag DragStarted;
    public event Drag DragMoved;
    public event Drag DragEnded;

    public SimpleGraphConnectors Inputs { get; private set; }
    public SimpleGraphConnectors Outputs { get; private set; }

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

        Inputs  = new SimpleGraphConnectors( inputs );
        Outputs = new SimpleGraphConnectors( outputs );

        AddChild( Inputs );
        AddChild( Outputs );

        m_isDragging = false;
    }
    

    protected override void OnAfterLayout()
    {
        Inputs.SetPosition( new Vector2( ( Size.x - Inputs.Size.x ) / 2.0f, 0.0f ) );
        Outputs.SetPosition( new Vector2( ( Size.x - Outputs.Size.x ) / 2.0f, Size.y - Outputs.Size.y ) );
    }


    protected override void OnDraw()
    {
        Rect r      = ScreenRect;
        r.y         = r.y + Inputs.Size.y;
        r.height    = r.height - Inputs.Size.y - Outputs.Size.y;

        GUI.Box( r, Text );
    }


    protected override void OnMouseDown( Event e )
    {
        base.OnMouseDown( e );

        if ( e != null && ScreenRect.Contains( e.mousePosition ) )
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
