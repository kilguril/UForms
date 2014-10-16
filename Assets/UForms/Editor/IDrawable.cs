using UnityEngine;

namespace UForms
{ 
    public interface IDrawable
    {
        Rect Bounds { get; set; }

        void Draw();
    }
}