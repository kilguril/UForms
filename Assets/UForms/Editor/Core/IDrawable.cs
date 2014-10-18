using UnityEngine;

namespace UForms.Core
{
    public interface IDrawable
    {
        Rect Bounds { get; }     // Bounds metrics used for layouting and drawing

        void Draw();                  
    }
}