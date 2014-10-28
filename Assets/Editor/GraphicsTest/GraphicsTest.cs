using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Application;
using UForms.Controls;
using UForms.Decorators;
using UForms.Graphics;

public class GraphicsTest : UFormsApplication 
{
    [MenuItem("Test/Graphics Test")]
    public static void Run()
    {
        EditorWindow.GetWindow<GraphicsTest>();                
    }

    protected override void OnInitialize()
    {
        Control control = new Control();
        control.SetSize( 100.0f, 100.0f, Control.MetricsUnits.Percentage, Control.MetricsUnits.Percentage );

        GraphicsCanvas canvas = (GraphicsCanvas)control.AddDecorator( new GraphicsCanvas() );

        Shape s = canvas.AddShape( new BezierCurve( Vector2.one * 10.0f, Vector2.one * 250.0f, Color.red, 3.0f ) );
        s.DrawEarly = true;

        canvas.AddShape( new Line( Vector2.zero, Vector2.one * 100.0f, Color.blue ) );

        AddChild( control );        
    }

    protected override void OnGUI()
    {

        base.OnGUI();        
    }
}
