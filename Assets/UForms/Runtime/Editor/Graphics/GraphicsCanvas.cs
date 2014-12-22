using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Decorators;

namespace UForms.Graphics
{
    /// <summary>
    /// A decorator used for managing a display list and rendering of shapes.
    /// </summary>
    public class GraphicsCanvas : Decorator
    {
        private static int SortZIndex( Shape a, Shape b )
        {
            return a.ZIndex.CompareTo( b.ZIndex );
        }

        /// <summary>
        /// A list of all shapes on this canvas.
        /// </summary>
        public List<Shape> Shapes { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public GraphicsCanvas() : base()
        {
            Shapes = new List<Shape>();
        }

        /// <summary>
        /// Adds a shape to the display list.
        /// </summary>
        /// <param name="shape">Shape object to add.</param>
        /// <returns>The shape object added.</returns>
        public Shape AddShape( Shape shape )
        {
            Shapes.Add( shape );
            return shape;
        }

        /// <summary>
        /// Removes a shape from the display list.
        /// </summary>
        /// <param name="shape">Shape object to remove.</param>
        /// <returns>The shape object removed.</returns>
        public Shape RemoveShape( Shape shape )
        {
            Shapes.Remove( shape );
            return shape;
        }

        /// <summary>
        /// Implementation of the OnBeforeDraw step.
        /// </summary>
        protected override void OnBeforeDraw()
        {
            Shapes.Sort( SortZIndex );

            foreach( Shape shape in Shapes )
            {
                if ( shape.DrawEarly )
                {
                    shape.Draw();
                }
            }
        }

        /// <summary>
        /// Implementation of the OnAfterDraw step.
        /// </summary>
        protected override void OnAfterDraw()
        {
            foreach ( Shape shape in Shapes )
            {
                if ( !shape.DrawEarly )
                {
                    shape.Draw();
                }
            }
        }
    }
}