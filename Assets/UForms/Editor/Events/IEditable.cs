using UnityEngine;
using System.Collections;

namespace UForms.Events
{
    public delegate void Edit( IEditable sender, EditEventArgs args, Event nativeEvent );

    public interface IEditable
    {
        event Edit ValueChange;        
    }

    public class EditEventArgs
    {        
        public object       oldValue;
        public object       newValue;
        public bool         oldValueSupported;

        public EditEventArgs( object oldVal, object newVal, bool oldValSupported )
        {
            oldValue          = oldVal;
            newValue          = newVal;
            oldValueSupported = oldValSupported;
        }
    }
}