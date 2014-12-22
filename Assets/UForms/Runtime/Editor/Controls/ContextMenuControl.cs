using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Events;

namespace UForms.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class ContextMenuControl : Control
    {
        /// <summary>
        /// 
        /// </summary>
        public GenericMenu Menu { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Positionless { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ContextMenuControl(  ) : base()
        {
            Menu = new GenericMenu();
            SetSize( 100.0f, 100.0f, MetricsUnits.Percentage, MetricsUnits.Percentage );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnContextClick( Event e )
        {
            if ( Positionless || PointInControl( e.mousePosition ) )
            {
                Menu.ShowAsContext();
                e.Use();
            }
        }

    }
}