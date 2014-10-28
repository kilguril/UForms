using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Decorators;

namespace UForms.Graphics
{
    public class GraphicsCanvas : Decorator
    {
        private static int SortZIndex( Shape a, Shape b )
        {
            return a.ZIndex.CompareTo( b.ZIndex );
        }

        public List<Shape> Shapes { get; private set; }

        public GraphicsCanvas() : base()
        {
            Shapes = new List<Shape>();
        }

        
        public Shape AddShape( Shape shape )
        {
            Shapes.Add( shape );
            return shape;
        }


        public Shape RemoveShape( Shape shape )
        {
            Shapes.Remove( shape );
            return shape;
        }


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