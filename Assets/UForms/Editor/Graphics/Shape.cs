using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Core;
using UForms.Decorators;

namespace UForms.Graphics
{
    public abstract class Shape : IDrawable
    {        
        public int ZIndex { get; set; }

        public Color DrawColor { get; set; }

        public bool DrawEarly { get; set; }        

        public Rect ScreenRect
        {
            get         { return m_rect; }
            private set { m_rect = value; }
        }

        protected Rect m_rect;

        public abstract void Draw();
    }
}