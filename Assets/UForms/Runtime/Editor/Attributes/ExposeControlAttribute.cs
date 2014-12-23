using System;

namespace UForms.Attributes
{
    [AttributeUsage( AttributeTargets.Class )]
    public class ExposeControlAttribute : Attribute
    {
        public readonly string      displayName;
        public readonly string      groupCategory;

        public ExposeControlAttribute( string name, string category )
        {
            displayName     = name;
            groupCategory   = category;
        }
    }
}