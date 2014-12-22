using UnityEngine;
using System.Collections;

namespace UForms.Events
{
    /// <summary>
    /// Drag event handler signature.
    /// </summary>
    /// <param name="sender">The object dispatching this event.</param>
    /// <param name="args">Drag event arguments.</param>
    /// <param name="nativeEvent">The native Unity event object this event originated from. Can be null in certain cases such as buttons, make sure to null check before use.</param>
    public delegate void Drag( IDraggable sender, DragEventArgs args, Event nativeEvent );

    /// <summary>
    /// Draggable element interface.
    /// </summary>
    public interface IDraggable
    {
        /// <summary>
        /// Drag started event.
        /// </summary>
        event Drag DragStarted;

        /// <summary>
        /// Drag moved event.
        /// </summary>
        event Drag DragMoved;
        
        /// <summary>
        /// Drag ended event.
        /// </summary>
        event Drag DragEnded;
    }

    /// <summary>
    /// Drag event arguments.
    /// </summary>
    public class DragEventArgs
    {
        /// <summary>
        /// The initial position of the drag action.
        /// </summary>
        public Vector2      initialPosition;

        /// <summary>
        /// The current position of the drag action.
        /// </summary>
        public Vector2      currentPosition;

        /// <summary>
        /// The last position delta of the drag action.
        /// </summary>
        public Vector2      delta;


        /// <summary>
        /// Constructor for <c>DragEventArgs</c>.
        /// </summary>
        /// <param name="initial">Initial drag position.</param>
        /// <param name="current">Current drag position.</param>
        /// <param name="d">Latest position delta.</param>
        public DragEventArgs( Vector2 initial, Vector2 current, Vector2 d )
        {
            initialPosition = initial;
            currentPosition = current;
            delta           = d;
        }
    }
}