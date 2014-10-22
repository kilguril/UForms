using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Events;

namespace UForms.Controls.Fields
{
    public abstract class AbstractField<T> : Control, IEditable
    {
        public event Edit ValueChange;

        public string       Label { get; set; }

        public T            Value
        {
            get { return m_cachedValue; }
            set { m_cachedValue = value; }
        }

        // Should we use the backing field to detect value changes and dispatch events.        
        protected abstract bool UseBackingFieldChangeDetection { get; }

        protected T         m_cachedValue;

        public AbstractField( T value = default(T), string label = "" ) : base()
        {
            Label = label;
            m_cachedValue = value;
        }


        public AbstractField( Vector2 position, Vector2 size, T value = default(T), string label = "" ) : base( position, size )
        {
            Label         = label;
            m_cachedValue = value;
        }

        protected abstract T DrawAndUpdateValue();

        // We need to implement a custom equality evaluator per control as the field data can be both value and reference types.
        // Additionally, since we're wrapping built in Unity controls, using nullable value types is not an option.        
        protected abstract bool TestValueEquality( T oldval, T newval );

        protected override void OnDraw()
        {
            bool changed = false;

            T oldval = m_cachedValue;

            if ( !UseBackingFieldChangeDetection )
            {
                EditorGUI.BeginChangeCheck();
            }
            
            T newval = DrawAndUpdateValue();

            if ( UseBackingFieldChangeDetection )
            {
                if ( !TestValueEquality( m_cachedValue, newval ) )
                {
                    changed = true;
                }
            }
            else
            {
                changed = EditorGUI.EndChangeCheck();
            }
           
            // We will only notify of a value change after we assigned the new value as the event will pass a refenrence to the sender and persumably we'd expect sender.Value to hold an up to date value.
            if ( changed )
            {
                m_cachedValue = newval;

                if ( ValueChange != null )
                {
                    EditEventArgs args = null;

                    if ( UseBackingFieldChangeDetection )
                    {
                        args = new EditEventArgs( oldval, newval, true );
                    }
                    else
                    {
                        args = new EditEventArgs( newval, newval, false );
                    }

                    ValueChange( this, args, null );
                }
            }
        }
        
    }
}