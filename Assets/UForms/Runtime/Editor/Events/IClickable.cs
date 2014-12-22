using UnityEngine;
using System.Collections;

namespace UForms.Events
{
    /// <summary>
    /// Click event handler signature.
    /// </summary>
    /// <param name="sender">The object dispatching this event.</param>
    /// <param name="args">Click event arguments.</param>
    /// <param name="nativeEvent">The native Unity event object this event originated from. Can be null in certain cases such as buttons, make sure to null check before use.</param>
    public delegate void Click( IClickable sender, ClickEventArgs args, Event nativeEvent );

    /// <summary>
    /// Clickable element interface.
    /// </summary>
    public interface IClickable
    {
        /// <summary>
        /// Click event.
        /// </summary>
        event Click Clicked;

        /// <summary>
        /// Mouse down event.
        /// </summary>
        event Click MouseDown;

        /// <summary>
        /// Mouse up event.
        /// </summary>
        event Click MouseUp;
    }

    /// <summary>
    /// A nicer enumeration for commonly used mouse buttons.
    /// </summary>
    public enum MouseButton
    {
        /// <summary>
        /// Left mouse button.
        /// </summary>
        Left,

        /// <summary>
        /// Middle mouse button.
        /// </summary>
        Middle,

        /// <summary>
        /// Right mouse button.
        /// </summary>
        Right,

        /// <summary>
        /// Other unspecified mouse button. Use the raw button value to determine which in this case.
        /// </summary>
        Other
    }

    /// <summary>
    /// Click event arguments.
    /// </summary>
    public class ClickEventArgs
    {        
        /// <summary>
        /// The raw button value provided by Unity.
        /// </summary>
        public int          rawButton;

        /// <summary>
        /// A nicer enumeration for commonly used mouse buttons.
        /// </summary>
        public MouseButton  button;

        /// <summary>
        /// Constructor for <c>ClickEventArgs</c>.
        /// </summary>
        /// <param name="btn">Raw button value</param>
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