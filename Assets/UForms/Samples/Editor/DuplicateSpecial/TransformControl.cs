using UnityEngine;

using System.Collections;

using UForms.Controls;
using UForms.Controls.Fields;
using UForms.Decorators;

public class TransformControl : Control
{
    public Vector3 Translate 
    { 
        get { return m_translate.Value; }
        set { m_translate.Value = value; } 
    }

    public Vector3 Rotate
    {
        get { return m_rotate.Value; }
        set { m_rotate.Value = value; }
    }


    public Vector3 Scale
    {
        get { return m_scale.Value; }
        set { m_scale.Value = value; }
    }

    private Vector3Field    m_translate;
    private Vector3Field    m_rotate;
    private Vector3Field    m_scale;

    protected override Vector2 DefaultSize
    {
        get { return new Vector2( 200.0f, 114.0f ); }
    }

    public TransformControl()
    {
        AddDecorator( new StackContent( StackContent.StackMode.Vertical, StackContent.OverflowMode.Flow ) );

        m_translate = new Vector3Field( Vector3.zero, "Translate" );
        m_rotate = new Vector3Field( Vector3.zero, "Rotate" );
        m_scale = new Vector3Field( Vector3.zero, "Scale" );

        m_translate.SetWidth( 100.0f, MetricsUnits.Percentage );
        m_rotate.SetWidth( 100.0f, MetricsUnits.Percentage );
        m_scale.SetWidth( 100.0f, MetricsUnits.Percentage );

        AddChild( m_translate );
        AddChild( m_rotate );
        AddChild( m_scale );
    }
}
