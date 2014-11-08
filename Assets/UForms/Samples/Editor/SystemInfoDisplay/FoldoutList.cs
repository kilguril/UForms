using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Application;
using UForms.Controls;
using UForms.Decorators;
using UForms.Events;

public class FoldoutList : Control 
{
    public Foldout     m_foldout;
    public Control     m_content;

    private float       m_indentationAmount;

    public FoldoutList( string title, float indentation, bool unfolded ) : base()
    {
        m_indentationAmount = indentation;

        m_foldout = new Foldout( title, unfolded );
        m_foldout.SetWidth( 100.0f, MetricsUnits.Percentage );

        m_content = new Control();
        m_content.SetWidth( 100.0f, MetricsUnits.Percentage );
        m_content.AddDecorator( new StackContent( StackContent.StackMode.Vertical, StackContent.OverflowMode.Flow ) );
        m_content.AddDecorator( new FitContent( false, true, false, true ) );

        AddChild( m_foldout );
        AddChild( m_content );

        AddDecorator( new StackContent( StackContent.StackMode.Vertical, StackContent.OverflowMode.Flow ) );
        AddDecorator( new FitContent( false, true, false, true ) );

        m_foldout.ValueChange += FoldoutValueChange;
        SetFoldState( unfolded );
    }


    public void AddItem( Control item )
    {
        item.SetMargin( m_indentationAmount, 0.0f, 0.0f, 0.0f, MetricsUnits.Pixel );
        item.SetWidth( 100.0f, MetricsUnits.Percentage );
        m_content.AddChild( item );
    }

    void FoldoutValueChange( IEditable sender, EditEventArgs args, Event nativeEvent )
    {
        SetFoldState( ( bool )args.newValue );
    }


    private void SetFoldState( bool state )
    {
        m_content.Visibility = ( state ? VisibilityMode.Visible : VisibilityMode.Collapsed );
        Dirty = true;
    }
}
