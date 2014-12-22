using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Core;
using UForms.Decorators;

namespace UForms.Graphics
{
    /// <summary>
    /// EARLY IMPLEMENTATION 
    /// Abstract base class for abstract graphics elements which are not controls and are rendered using a <c>GraphicsCanvas</c>.
    /// </summary>
    public abstract class Shape : IDrawable
    {        
        /// <summary>
        /// Shape z-index. Shapes with higher z-index will be rendered on top of shapes with a lower value.
        /// </summary>
        public int ZIndex { get; set; }

        /// <summary>
        /// The line color for the shape.
        /// </summary>
        public Color DrawColor { get; set; }

        /// <summary>
        /// Specifies wether a shape should be rendered before or after controls. If set to true the shape will be rendererd before rendering controls. It's important to note that z-index based sorting is applied only to shapes rendered on the same pass.
        /// </summary>
        public bool DrawEarly { get; set; }        

        /// <summary>
        /// The shape's bounding rectangle.
        /// </summary>
        public Rect ScreenRect
        {
            get         { return m_rect; }
            private set { m_rect = value; }
        }

        /// <summary>
        /// The shape's bounding rectangle.
        /// </summary>
        protected Rect m_rect;

        /// <summary>
        /// Draw method used to render the shape.
        /// </summary>
        public abstract void Draw();
    }
}