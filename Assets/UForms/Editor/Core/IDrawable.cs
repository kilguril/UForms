using UnityEngine;

namespace UForms.Core
{
    public interface IDrawable
    {
        Rect ScreenRect { get; }     // Screen rect used for drawing
        void Draw();                  
    }
}