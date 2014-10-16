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

        public Button( Rect bounds = new Rect(), string text = "") : base( bounds )
        {
            Bounds = bounds;
            Text = text;
        }

        public override void Draw()
        {
            if ( GUI.Button( Bounds, Text ) )
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
            if ( ScreenBounds.Contains( e.mousePosition ) )
            {
                if ( MouseDown != null )
                {
                    MouseDown( this, new ClickEventArgs( e.button ), e );
                }
            }
        }

        protected override void OnMouseUp( Event e )
        {
            if ( ScreenBounds.Contains( e.mousePosition ) )
            {
                if ( MouseUp != null )
                {
                    MouseUp( this, new ClickEventArgs( e.button ), e );
                }
            }
        }
    }
}