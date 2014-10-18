using UnityEngine;
using System.Collections;

namespace UForms.Events
{
    public delegate void Drag( IDraggable sender, DragEventArgs args, Event nativeEvent );

    public interface IDraggable
    {
        event Drag DragStarted;
        event Drag DragMoved;        
        event Drag DragEnded;
    }


    public class DragEventArgs
    {
        public Vector2      initialPosition;
        public Vector2      currentPosition;
        public Vector2      delta;


        public DragEventArgs( Vector2 initial, Vector2 current, Vector2 d )
        {
            initialPosition = initial;
            currentPosition = current;
            delta           = d;
        }
    }
}