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
        panel.AddChild( new TextField( "", "", true ) );
        panel.AddChild( new Vector2Field() );
        panel.AddChild( new Vector3Field() );
        panel.AddChild( new Vector4Field() );

        panel.AddChild( new CurveField( Color.green, new Rect()) );

        panel.AddChild( new MultiFloatField(
            new float[] { 0.0f,0.0f,10.0f,12.0f },
            new string[] { "A","B","C","D"},
            "MultiFloat"
        ) );

        //GameObject go = new GameObject();
        //SerializedObject so = new SerializedObject( go );
        //SerializedProperty sp = so.GetIterator();

        //sp.Next( true );
        //SerializedProperty sp1 = sp.Copy();

        //sp.Next( true );
        //SerializedProperty sp2 = sp.Copy();

        //panel.AddChild( new PropertyField( sp1, sp1.name ) );
        //panel.AddChild( new PropertyField( sp2, sp2.name ) );

        foreach( Control c in panel.Children )
        {
            if ( c is UForms.Events.IEditable )
            {
                UForms.Events.IEditable e = c as UForms.Events.IEditable;
                e.ValueChange += field_ValueChange;
            }
        }

        AddControl( panel );
    }

    void field_ValueChange( UForms.Events.IEditable sender, UForms.Events.EditEventArgs args, Event nativeEvent )
    {
        Debug.Log( string.Format( "{0} Value CHANGED!!!", sender ) );
        //Debug.Log( ( Bounds )args.oldValue + " ----> " + ( Bounds )args.newValue );
    }

    protected override void OnGUI()
    {
        base.OnGUI();

    }


}
