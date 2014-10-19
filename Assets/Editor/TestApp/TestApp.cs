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
        StackPanel panel = new StackPanel( Vector2.zero, Vector2.zero, StackPanel.StackMode.Vertical, StackPanel.OverflowMode.Contain );
        panel.AddChild( new Button( new Vector2( 100.0f, 100.0f ), new Vector2( 100.0f, 30.0f ), "Hello World!" ) );
        panel.AddChild( new Button( new Vector2( 200.0f, 100.0f ), new Vector2( 100.0f, 30.0f ), "Hello World2" ) );
        panel.AddChild( new Button( new Vector2( 300.0f, 100.0f ), new Vector2( 100.0f, 30.0f ), "Hello World3" ) );
        panel.AddChild( new Button( new Vector2( 400.0f, 100.0f ), new Vector2( 100.0f, 30.0f ), "Hello World4" ) );

        panel.Children[ 0 ].SetMargin( 10.0f, 10.0f, 10.0f, 0.0f );
        panel.Children[ 1 ].SetMargin( 10.0f, 10.0f, 10.0f, 0.0f );
        panel.Children[ 2 ].SetMargin( 10.0f, 10.0f, 10.0f, 0.0f );
        panel.Children[ 3 ].SetMargin( 10.0f, 10.0f, 10.0f, 0.0f );

        panel.Children[ 0 ].Enabled = false;

        AddControl( panel );        

        
    }

}
