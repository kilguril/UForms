using UnityEngine;

namespace UForms.Core
{
    /// <summary>
    /// Provides a uniform interface for all drawable elements.
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// The element's screen rect, used for rendering.
        /// </summary>
        Rect ScreenRect { get; }    

        /// <summary>
        /// Draw method used to render the element.
        /// </summary>
        void Draw();                  
    }
}