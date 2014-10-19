using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Application;
using UForms.Controls;
using UForms.Controls.Panels;
using UForms.Controls.Fields;

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
        //panel.AddChild( new Button( new Vector2( 100.0f, 100.0f ), new Vector2( 100.0f, 30.0f ), "Hello World!" ) );
        //panel.AddChild( new Button( new Vector2( 200.0f, 100.0f ), new Vector2( 100.0f, 30.0f ), "Hello World2" ) );
        //panel.AddChild( new Button( new Vector2( 300.0f, 100.0f ), new Vector2( 100.0f, 30.0f ), "Hello World3" ) );
        //panel.AddChild( new Button( new Vector2( 400.0f, 100.0f ), new Vector2( 100.0f, 30.0f ), "Hello World4" ) );

        //panel.Children[ 0 ].SetMargin( 10.0f, 10.0f, 10.0f, 0.0f );
        //panel.Children[ 1 ].SetMargin( 10.0f, 10.0f, 10.0f, 0.0f );
        //panel.Children[ 2 ].SetMargin( 10.0f, 10.0f, 10.0f, 0.0f );
        //panel.Children[ 3 ].SetMargin( 10.0f, 10.0f, 10.0f, 0.0f );

        //panel.Children[ 0 ].Enabled = false;

        //panel.AddChild( new Label( new Vector2( 100.0f, 100.0f ), new Vector2( 100.0f, 30.0f ), "I am a regular label" ) );
        //panel.AddChild( new Label( new Vector2( 100.0f, 100.0f ), new Vector2( 100.0f, 30.0f ), "I am selectable!!", true ) );

        panel.AddChild( new IntField( Vector2.zero, new Vector2( 100.0f, 50.0f ), 100 ) );
        panel.AddChild( new BoundsField( Vector2.zero, new Vector2( 300, 50.0f ), default( Bounds ), "MEOW" ) );
        panel.AddChild( new ColorField( Vector2.zero, new Vector2( 300, 50.0f ), default( Color ), "MEOW" ) );
        panel.AddChild( new FloatField( Vector2.zero, new Vector2( 300, 50.0f ), 0.0f, "RRR" ) );
        panel.AddChild( new LayerField( Vector2.zero, new Vector2( 300, 50.0f ), 0, "LayerField" ) );
        panel.AddChild( new MaskField( Vector2.zero, new Vector2( 300, 50.0f ), 0, new string[] {"TITS","BOOBS"} ) );
        panel.AddChild( new ObjectField( Vector2.zero, new Vector2( 300.0f, 30.0f ), typeof( Camera ), true, null, "Camera:" ) );
        panel.AddChild( new TagField( Vector2.zero, new Vector2( 300.0f, 30.0f ), "", "Tag-->" ) );
        panel.AddChild( new RectField( Vector2.zero, new Vector2( 300.0f, 30.0f ) ) );
        panel.AddChild( new LabelField( Vector2.zero, new Vector2( 300.0f, 30.0f ), "Hello From", "Label Field" ) );
        panel.AddChild( new TextField( Vector2.zero, new Vector2( 300.0f, 30.0f ), "Text Field", "TFLabel" ) );
        panel.AddChild( new Vector2Field( Vector2.zero, new Vector2( 100.0f, 100.0f ) ) );
        panel.AddChild( new Vector3Field( Vector2.zero, new Vector2( 100.0f, 100.0f ) ) );
        panel.AddChild( new Vector4Field( Vector2.zero, new Vector2( 100.0f, 100.0f ) ) );
        panel.AddChild( new EnumMaskField( Vector2.zero, new Vector2( 100.0f, 100.0f ), UForms.Controls.Panels.StackPanel.OverflowMode.Contain ) );
        AddControl( panel );
    }

    protected override void OnGUI()
    {
        base.OnGUI();

    }


}
