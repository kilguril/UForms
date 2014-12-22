using UnityEngine;
using System.Collections;

namespace UForms.Events
{
    /// <summary>
    /// Edit event handler signature.
    /// </summary>
    /// <param name="sender">The object dispatching this event.</param>
    /// <param name="args">Edit event arguments.</param>
    /// <param name="nativeEvent">The native Unity event object this event originated from. Can be null in certain cases such as buttons, make sure to null check before use.</param>
    public delegate void Edit( IEditable sender, EditEventArgs args, Event nativeEvent );

    /// <summary>
    /// Editable object interface.
    /// An editable object is an object representing a value which can be edited by the user.
    /// </summary>
    public interface IEditable
    {
        /// <summary>
        /// Value changed event. Dispatched when the object's value changes.
        /// </summary>
        event Edit ValueChange;        
    }

    /// <summary>
    /// Edit event arguments.
    /// </summary>
    public class EditEventArgs
    {        
        /// <summary>
        /// Previous value (the value changed from) as an object.
        /// </summary>
        public object       oldValue;

        /// <summary>
        /// Current value (the value changed to) as an object.
        /// </summary>
        public object       newValue;

        /// <summary>
        /// Describes if the element supports the <c>oldValue</c> field. Not all elements can provide the previous value, in which case this value will be set to <c>false</c>.
        /// </summary>
        public bool         oldValueSupported;

        /// <summary>
        /// Constructor for <c>EditEventArgs</c>
        /// </summary>
        /// <param name="oldVal">Old value. If not supported should be set to something sensible such as null.</param>
        /// <param name="newVal">Current value.</param>
        /// <param name="oldValSupported">Is the oldValue field supported?</param>
        public EditEventArgs( object oldVal, object newVal, bool oldValSupported )
        {
            oldValue          = oldVal;
            newValue          = newVal;
            oldValueSupported = oldValSupported;
        }
    }
}