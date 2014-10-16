using UnityEngine;
using System.Collections;

namespace UForms.Controls
{
    public class Label : Control
    {
        public string Text { get; set; }

        public Label( Rect bounds = new Rect(), string text = "") : base( bounds )
        {
            Bounds = bounds;
            Text = text;
        }

        public override void Draw()
        {
            GUI.Label( Bounds, Text );
        }
    }
}