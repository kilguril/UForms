using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;

namespace UForms.Decorators
{
    /// <summary>
    /// This decorator creates a clipping area for the contained children, based on the controls bounding rectangle.
    /// </summary>
    public class ClipContent : Decorator
    {
        /// <summary>
        /// Implementation of the OnLayout step.
        /// </summary>
        protected override void OnLayout()
        {
            m_boundControl.ResetPivotRoot = true;
        }

        /// <summary>
        /// Implementation of the OnBeforeDraw step.
        /// </summary>
        protected override void OnBeforeDraw()
        {
            GUI.BeginGroup( m_boundControl.ScreenRect );
        }

        /// <summary>
        /// Implementation of the OnAfterDraw step.
        /// </summary>
        protected override void OnAfterDraw()
        {
            GUI.EndGroup();
        }
    }
}