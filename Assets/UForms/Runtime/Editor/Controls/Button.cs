using UnityEngine;
using System.Collections;

using UForms.Events;
using UForms.Attributes;

namespace UForms.Controls
{
    /// <summary>
    /// 
    /// </summary>
    
    [ExposeControl("Button", "General")]
    public class Button : Control, IClickable
    {
        /// <summary>
        /// 
        /// </summary>
        public event Click Clicked;

        /// <summary>
        /// 
        /// </summary>
        public event Click MouseDown;

        /// <summary>
        /// 
        /// </summary>
        public event Click MouseUp;

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override Vector2 DefaultSize
        {
            get { return new Vector2( 100.0f, 30.0f ); }
        }

        public Button() : base()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public Button( string text ) : base()
        {
            Text = text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="text"></param>
        public Button( Vector2 position, Vector2 size, string text = "") : base( position, size )
        {            
            Text = text;
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown( Event e )
        {
            if ( PointInControl( e.mousePosition ) )
            {
                if ( MouseDown != null )
                {
                    MouseDown( this, new ClickEventArgs( e.button ), e );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp( Event e )
        {
            if ( PointInControl( e.mousePosition ) )
            {
                if ( MouseUp != null )
                {
                    MouseUp( this, new ClickEventArgs( e.button ), e );
                }
            }
        }
    }
}