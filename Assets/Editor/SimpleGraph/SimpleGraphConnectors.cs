using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;
using UForms.Decorators;

public class SimpleGraphConnectors : Control 
{
    public int Count
    {
        get
        {
            if ( Buttons == null )
            {
                return 0;
            }

            return Buttons.Count;
        }

        set
        {
            ResizeButtonCount( value );
        }
    }

    public List<Button> Buttons { get; private set; }

    private const float CONNECTOR_SIZE = 12.0f;
    private const float CONNECTOR_MARGIN = 2.0f;

    public SimpleGraphConnectors( int count ) : base()
    {
        AddDecorator( new FitContent( true, true, true, true ) );
        AddDecorator( new StackContent( StackContent.StackMode.Horizontal, StackContent.OverflowMode.Flow ) );

        Buttons = new List<Button>();

        ResizeButtonCount( count );

        for ( int i = 0; i < count; i++ )
        {
            
        }
    }

    private void ResizeButtonCount( int count )
    {
        while ( count > Count )
        {
            Button b = new Button( Vector2.zero, new Vector2( CONNECTOR_SIZE + CONNECTOR_MARGIN * 2, CONNECTOR_SIZE ) );
            b.SetMargin( CONNECTOR_MARGIN, 0.0f, CONNECTOR_MARGIN, 0.0f );

            Buttons.Add( b );
            AddChild( b );
        }

        while ( count < Count )
        {
            Button b = Buttons[ Buttons.Count - 1 ];
            Buttons.RemoveAt( Buttons.Count - 1 );
            RemoveChild( b );
        }
    }
}
