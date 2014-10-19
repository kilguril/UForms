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
        StackPanel panel = new StackPanel( StackPanel.StackMode.Vertical );

        panel.AddChild( new BoundsField() );
        panel.AddChild( new ColorField() );
        panel.AddChild( new EnumMaskField( UForms.Controls.Panels.StackPanel.OverflowMode.Contain ) );
        panel.AddChild( new FloatField() );
        panel.AddChild( new IntField() );
        panel.AddChild( new LabelField( "aa", "bb" ) );
        panel.AddChild( new LayerField() );
        panel.AddChild( new MaskField() );
        panel.AddChild( new ObjectField( typeof( Object ) ) );
        panel.AddChild( new RectField() );
        panel.AddChild( new TagField() );
        panel.AddChild( new TextField() );
        panel.AddChild( new Vector2Field() );
        panel.AddChild( new Vector3Field() );
        panel.AddChild( new Vector4Field() );

        AddControl( panel );
    }

    protected override void OnGUI()
    {
        base.OnGUI();

    }


}
