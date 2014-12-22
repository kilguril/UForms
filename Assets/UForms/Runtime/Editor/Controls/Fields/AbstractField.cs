using UnityEngine;
using UnityEditor;
using System.Collections;

using UForms.Events;

namespace UForms.Controls.Fields
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractField<T> : Control, IEditable
    {
        /// <summary>
        /// 
        /// </summary>
        public event Edit ValueChange;

        /// <summary>
        /// 
        /// </summary>
        public string       Label { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public T            Value
        {
            get { return m_cachedValue; }
            set { m_cachedValue = value; }
        }
        
        /// <summary>
        /// Should we use the backing field to detect value changes and dispatch events.        
        /// </summary>
        protected abstract bool UseBackingFieldChangeDetection { get; }

        /// <summary>
        /// 
        /// </summary>
        protected T         m_cachedValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public AbstractField( T value = default(T), string label = "" ) : base()
        {
            Label = label;
            m_cachedValue = value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public AbstractField( Vector2 position, Vector2 size, T value = default(T), string label = "" ) : base( position, size )
        {
            Label         = label;
            m_cachedValue = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract T DrawAndUpdateValue();


        /// <summary>
        /// We need to implement a custom equality evaluator per control as the field data can be both value and reference types.
        /// Additionally, since we're wrapping built in Unity controls, using nullable value types is not an option.        
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
        protected abstract bool TestValueEquality( T oldval, T newval );

        /// <summary>
        /// 
        /// </summary>
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