using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Application;
using UForms.Controls;
using UForms.Controls.Panels;

public class TestApp : UFormsApplication
{
    [MenuItem( "Test/TestApp" )]
    private static void Run()
    {
        EditorWindow.GetWindow<TestApp>();
    }


    protected override void OnInitialize()
    {
        ScrollPanel panel = new ScrollPanel( Vector2.zero, Vector2.zero, true, true, true );
        panel.AddChild( new Button( new Vector2( 100.0f, 100.0f ), new Vector2( 100.0f, 30.0f ), "Hello World!" ) );

        AddControl( panel );
    }

}
