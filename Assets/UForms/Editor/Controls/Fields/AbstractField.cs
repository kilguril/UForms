using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls.Fields
{
    public abstract class AbstractField<T> : Control
    {
        public string       Label { get; set; }

        public T            Value
        {
            get { return m_cachedValue; }
            set { m_cachedValue = value; }
        }

        protected T         m_cachedValue;
        protected Rect      m_fieldRect;

        public AbstractField( Vector2 position, Vector2 size, T value = default(T), string label = "" ) : base( position, size )
        {
            Label         = label;
            m_cachedValue = value;
        }

        protected abstract T DrawAndUpdateValue();

        // We need to implement a custom equality evaluator per control as the field data can be both value and reference types.
        // Additionally, since we're wrapping built in Unity controls, using nullable value types is not an option.        
        protected abstract bool TestValueEquality( T oldval, T newval );

        protected override void OnLayout()
        {
            m_fieldRect.Set(
                ScreenPosition.x + MarginLeftTop.x,
                ScreenPosition.y + MarginLeftTop.y,
                Size.x,
                Size.y
            );
        }

        protected override void OnDraw()
        {
            // TODO :: Maybe cache old value and ModifiedEventArgs will contain old val, new val(?)
            T newval = DrawAndUpdateValue();
            bool changed = false;
            
            if ( !TestValueEquality( m_cachedValue, newval ) )            
            {
                changed = true;
            }

            m_cachedValue = newval;

            // We will only notify of a value change after we assigned the new value as the event will pass a refenrence to the sender and persumably we'd expect sender.Value to hold an up to date value.
            if ( changed )
            {
                // TODO :: Notify
            }
        }
    }
}