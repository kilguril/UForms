using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UForms
{
    public class Control : Element
    {
        public List<Control> Children { get; protected set; }


        public Control( Rect bounds = new Rect() ) : base ( bounds )
        {
            Children = new List<Control>();
        }


        public virtual void AddChild( Control child )
        {
            if ( !Children.Contains( child ) )
            {
                child.Container = this;
                Children.Add( child );
            }
        }


        public virtual void RemoveChild( Control child )
        {
            if ( Children.Contains( child ) )
            {
                child.Container = null;
                Children.Remove( child );
            }
        }
        

        public override void Draw()
        {
            foreach( Control child in Children )
            {
                child.Draw();
            }
        }

        public override void ProcessEvents( Event e )
        {
            foreach( Control child in Children )
            {
                child.ProcessEvents( e );
                
                if ( e == null )
                {
                    break;
                }
            }

            base.ProcessEvents( e );
        }
    }
}