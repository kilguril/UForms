using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Attributes;

namespace UForms.Controls
{
    /// <summary>
    /// 
    /// </summary>

    [ExposeInDesigner( "Help Box", "General" )]
    public class HelpBox : Control
    {
        /// <summary>
        /// 
        /// </summary>
        public string           Text        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MessageType      Type        { get; set; }


        /// <summary>
        /// 
        /// </summary>
        protected override Vector2 DefaultSize {
            get { return new Vector2( 200.0f, 32.0f ); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        public HelpBox( string text = "", MessageType type = MessageType.None ) : base()
        {
            Text = text;
            Type = type;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        public HelpBox( Vector2 position, Vector2 size, string text = "", MessageType type = MessageType.None ) : base( position, size )
        {            
            Text = text;
            Type = type;
        }


        /// <summary>
        /// 
        /// </summary>
        protected override void OnDraw()
        {
            EditorGUI.HelpBox( ScreenRect, Text, Type );
        }
    }
}