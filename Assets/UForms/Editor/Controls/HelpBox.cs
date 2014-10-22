using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls
{
    public class HelpBox : Control
    {
        public string           Text        { get; set; }
        public MessageType      Type        { get; set; }


        protected override Vector2 DefaultSize {
            get { return new Vector2( 200.0f, 32.0f ); }
        }

        public HelpBox( string text = "", MessageType type = MessageType.None ) : base()
        {
            Text = text;
            Type = type;
        }


        public HelpBox( Vector2 position, Vector2 size, string text = "", MessageType type = MessageType.None ) : base( position, size )
        {            
            Text = text;
            Type = type;
        }


        protected override void OnDraw()
        {
            EditorGUI.HelpBox( ScreenRect, Text, Type );
        }
    }
}