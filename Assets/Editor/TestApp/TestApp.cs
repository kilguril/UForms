using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms;
using UForms.Views;

public class TestApp : EditorApp
{
    [MenuItem( "Test/TestApp") ]
    private static void Run()
    {
        EditorWindow.GetWindow<TestApp>();
    }

    public TestApp()
    {
        ActiveView = new SplitView( new TestView(), new TestView(), SplitView.SplitMode.Horizontal, 0.5f, false );
    }

    protected override void OnGUI()
    {
        base.OnGUI();
    }
}
