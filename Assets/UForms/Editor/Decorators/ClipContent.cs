using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;

namespace UForms.Decorators
{
    public class ClipContent : Decorator
    {
        protected override void OnLayout()
        {
            m_boundControl.ResetPivotRoot = true;
        }

        protected override void OnBeforeDraw()
        {
            GUI.BeginGroup( m_boundControl.ScreenRect );
        }

        protected override void OnAfterDraw()
        {
            GUI.EndGroup();
        }
    }
}