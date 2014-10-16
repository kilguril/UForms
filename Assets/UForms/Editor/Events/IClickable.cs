using UnityEngine;
using System.Collections;

namespace UForms.Events
{
    public delegate void Click( IClickable sender, ClickEventArgs args, Event nativeEvent );

    public interface IClickable
    {
        event Click Clicked;
        event Click MouseDown;
        event Click MouseUp;
    }

    public enum MouseButton
    {
        Left,
        Middle,
        Right,
        Other
    }

    public class ClickEventArgs
    {        
        public int          rawButton;
        public MouseButton  button;

        public ClickEventArgs( int btn )
        {
            rawButton = btn;

            switch( btn )
            {
                case 0:     button = MouseButton.Left;      break;
                case 1:     button = MouseButton.Right;     break;
                case 2:     button = MouseButton.Middle;    break;
                default:    button = MouseButton.Other;     break;
            }
        }
    }
}