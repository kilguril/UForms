using UnityEngine;
using System.Collections;

using UForms.Events;

namespace UForms.Controls
{
    public class Button : Control, IClickable
    {
        public event Click Clicked;
        public event Click MouseDown;
        public event Click MouseUp;

        public string Text { get; set; }


        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 100.0f, 30.0f ); }
        }


        public Button( string text = "" ) : base()
        {
            Text = text;
        }


        public Button( Vector2 position, Vector2 size, string text = "") : base( position, size )
        {            
            Text = text;
        }


        protected override void OnDraw()
        {
            if ( GUI.Button( ScreenRect, Text ) )
            {
                int button = 0;

                if ( Event.current != null )
                {
                    button = Event.current.button;
                }

                if ( Clicked != null )
                {
                    Clicked( this, new ClickEventArgs( button ), Event.current );
                }
            }
        }


        protected override void OnMouseDown( Event e )
        {
            if ( ScreenRect.Contains( e.mousePosition ) )
            {
                if ( MouseDown != null )
                {
                    MouseDown( this, new ClickEventArgs( e.button ), e );
                }
            }
        }


        protected override void OnMouseUp( Event e )
        {
            if ( ScreenRect.Contains( e.mousePosition ) )
            {
                if ( MouseUp != null )
                {
                    MouseUp( this, new ClickEventArgs( e.button ), e );
                }
            }
        }
    }
}