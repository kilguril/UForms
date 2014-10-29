using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;
using UForms.Decorators;
using UForms.Events;

public class SimpleGraph : Control
{
    public List<SimpleGraphNode> Nodes { get; private set; }


    public SimpleGraph() : base()
    {
        Nodes = new List<SimpleGraphNode>();
    }


    public void AddNode( SimpleGraphNode node )
    {
        Nodes.Add( node );
        AddChild( node );

        node.DragMoved += OnNodeDragMoved;
    }


    public void RemoveNOde( SimpleGraphNode node )
    {
        Nodes.Remove( node );
        RemoveChild( node );

        node.DragMoved -= OnNodeDragMoved;
    }


    void OnNodeDragMoved( IDraggable sender, DragEventArgs args, Event nativeEvent )
    {
        if ( sender is SimpleGraphNode )
        {
            SimpleGraphNode node = sender as SimpleGraphNode;
            node.Position += args.delta;
        }
    }
}
