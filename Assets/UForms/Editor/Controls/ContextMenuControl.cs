using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Events;

namespace UForms.Controls
{
    public class ContextMenuControl : Control
    {
        public GenericMenu Menu { get; private set; }

        public ContextMenuControl(  ) : base()
        {
            Menu = new GenericMenu();
            SetSize( 100.0f, 100.0f, MetricsUnits.Percentage, MetricsUnits.Percentage );
        }


        protected override void OnContextClick( Event e )
        {
            if ( ScreenRect.Contains( e.mousePosition ) )
            {
                Menu.ShowAsContext();
                e.Use();
            }
        }

    }
}