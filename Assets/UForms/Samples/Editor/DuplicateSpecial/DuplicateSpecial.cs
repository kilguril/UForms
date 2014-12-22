using UnityEngine;
using UnityEditor;

using System.Collections;

using UForms.Application;
using UForms.Controls;
using UForms.Controls.Fields;
using UForms.Controls.Dropdowns;
using UForms.Decorators;

public class DuplicateSpecial : UFormsApplication 
{
    [MenuItem( "UForms Samples/Duplicate Special" )]
    private static void Run()
    {
        EditorWindow.GetWindow<DuplicateSpecial>( true, "Duplicate Special", true );        
    }    


    private Control             m_root;

    private ObjectField         m_original;
    private Vector3Field        m_pivot;
    private TransformControl    m_transform;
    private IntField            m_count;
    private EnumDropdown        m_space;

    private Button              m_duplicate;


    protected override void OnInitialize()
    {
        Vector2 winSize = new Vector3( 312.0f, 265.0f );
        maxSize = winSize;
        minSize = winSize;        

        autoRepaintOnSceneChange = true;

        m_root = new Control();
        m_root.SetSize( 100.0f, 100.0f, Control.MetricsUnits.Percentage, Control.MetricsUnits.Percentage );
        m_root.AddDecorator( new StackContent() );

        AddChild( m_root );

        // Input object field
        m_original = new ObjectField( typeof( GameObject ), true, null, "Original" );
        m_original.SetHeight( 26.0f, Control.MetricsUnits.Pixel );
        m_original.SetWidth( 100.0f, Control.MetricsUnits.Percentage );
        m_original.SetMargin( 5.0f, 5.0f, 5.0f, 5.0f );
        m_root.AddChild( m_original );

        // Rotation pivot point
        m_pivot = new Vector3Field( Vector3.zero, "Pivot:" );
        m_pivot.SetHeight( 40.0f, Control.MetricsUnits.Pixel );
        m_pivot.SetWidth( 100.0f, Control.MetricsUnits.Percentage );
        m_pivot.SetMargin( 5.0f, 5.0f, 5.0f, 5.0f );
        m_root.AddChild( m_pivot );

        // Transform control
        m_transform = new TransformControl();
        m_transform.SetWidth( 100.0f, Control.MetricsUnits.Percentage );
        m_transform.SetMargin( 5.0f, 5.0f, 5.0f, 5.0f );
        m_root.AddChild( m_transform );

        // Count field
        m_count = new IntField( 1, "Duplicate Count:" );
        m_count.SetHeight( 26.0f, Control.MetricsUnits.Pixel );
        m_count.SetWidth( 100.0f, Control.MetricsUnits.Percentage );
        m_count.SetMargin( 5.0f, 5.0f, 5.0f, 5.0f );
        m_root.AddChild( m_count );

        // Space field
        m_space = new EnumDropdown( Space.World, "Space:" );
        m_space.SetHeight( 26.0f, Control.MetricsUnits.Pixel );
        m_space.SetWidth( 100.0f, Control.MetricsUnits.Percentage );
        m_space.SetMargin( 5.0f, 5.0f, 5.0f, 5.0f );
        m_root.AddChild( m_space );

        // Duplicate button
        m_duplicate = new Button( "Duplicate" );
        m_duplicate.SetWidth( 100.0f, Control.MetricsUnits.Percentage );
        m_duplicate.SetMargin( 5.0f, 5.0f, 5.0f, 5.0f );
        m_duplicate.Enabled = false;
        m_root.AddChild( m_duplicate );

        // Events
        m_original.ValueChange += m_original_ValueChange;
        m_count.ValueChange += m_count_ValueChange;
        m_duplicate.Clicked += m_duplicate_Clicked;

        SceneView.onSceneGUIDelegate += SceneViewGUI;
    }


    void OnDestroy()
    {
        SceneView.onSceneGUIDelegate -= SceneViewGUI;
    }


    void SceneViewGUI( SceneView scene )
    {
        m_pivot.Value = Handles.DoPositionHandle( m_pivot.Value, Quaternion.Euler( Vector3.up ) );
    }



    void m_count_ValueChange( UForms.Events.IEditable sender, UForms.Events.EditEventArgs args, Event nativeEvent )
    {
        if ( m_count.Value <= 0 )
        {
            m_count.Value = 1;
        }
    }    


    void m_original_ValueChange( UForms.Events.IEditable sender, UForms.Events.EditEventArgs args, Event nativeEvent )
    {
        if ( m_original.Value != null )
        {
            // Only allow scene objects
            if ( EditorUtility.IsPersistent( m_original.Value ) )
            {                
                m_original.Value = null;
            }
        }

        m_duplicate.Enabled = ( m_original.Value != null );
    }


    void m_duplicate_Clicked( UForms.Events.IClickable sender, UForms.Events.ClickEventArgs args, Event nativeEvent )
    {
        Duplicate( m_original.Value as GameObject, m_count.Value, m_transform.Translate, m_transform.Rotate, m_transform.Scale, (Space)m_space.Value );
    }


    private void Duplicate( GameObject original, int count, Vector3 translate, Vector3 rotate, Vector3 scale, Space space )
    {
        if ( original == null )
        {
            return;
        }

        Vector3 t, r, s;
        Vector3 pivot;
        Transform parent;

        parent = original.transform.parent;

        if ( space == Space.World )
        {
            original.transform.parent = null;
        }

        t = original.transform.localPosition;
        r = original.transform.localRotation.eulerAngles;
        s = original.transform.localScale;

        pivot = original.transform.position - m_pivot.Value;

        for ( int i = 0; i < count; i++ )
        {
            pivot += translate;
            t += translate;
            r += rotate;
            s += scale;                        

            GameObject obj = Instantiate( original ) as GameObject;

            obj.transform.parent = parent;

            obj.transform.localPosition = t;            
            obj.transform.localScale = s;

            obj.transform.RotateAround( obj.transform.position - pivot, space == Space.World ? Vector3.up : obj.transform.up, r.y );
            obj.transform.RotateAround( obj.transform.position - pivot, space == Space.World ? Vector3.right : obj.transform.right, r.x );
            obj.transform.RotateAround( obj.transform.position - pivot, space == Space.World ? Vector3.forward : obj.transform.forward, r.z );

            obj.name = original.name;
        }

        original.transform.parent = parent;
    }
}
