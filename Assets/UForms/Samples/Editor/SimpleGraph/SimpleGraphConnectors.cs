using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;
using UForms.Decorators;
using UForms.Events;

public class SimpleGraphConnectors : Control 
{
    public delegate void ConnectorClicked( int index, MouseButton mouseButton );
    public event ConnectorClicked OnConnectorClicked;

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
    }


    public Vector2 GetConnectionPoint( int index )
    {
        if ( index >= 0 && index < Buttons.Count )
        {
            return  new Vector2( Buttons[ index ].ScreenRect.xMin, Buttons[ index ].ScreenRect.yMin ) + ( Buttons[ index ].Size / 2.0f );
        }

        return Vector2.zero;
    }


    private void ResizeButtonCount( int count )
    {
        while ( count > Count )
        {
            Button b = new Button( Vector2.zero, new Vector2( CONNECTOR_SIZE + CONNECTOR_MARGIN * 2, CONNECTOR_SIZE ) );
            b.SetMargin( CONNECTOR_MARGIN, 0.0f, CONNECTOR_MARGIN, 0.0f );

            Buttons.Add( b );
            AddChild( b );

            b.Clicked += ConnectorButtonClicked;
        }

        while ( count < Count )
        {
            Button b = Buttons[ Buttons.Count - 1 ];
            Buttons.RemoveAt( Buttons.Count - 1 );
            RemoveChild( b );

            b.Clicked -= ConnectorButtonClicked;
        }
    }

    void ConnectorButtonClicked( UForms.Events.IClickable sender, UForms.Events.ClickEventArgs args, Event nativeEvent )
    {
        Button b = sender as Button;

        if ( b != null )
        {
            int index = Buttons.IndexOf( b );

            if ( index >= 0 )
            {
                if ( OnConnectorClicked != null )
                {
                    OnConnectorClicked( index, args.button );
                }
            }
        }
    }
}
