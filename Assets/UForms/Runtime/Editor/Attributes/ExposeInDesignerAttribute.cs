using System;

namespace UForms.Attributes
{
    [AttributeUsage( AttributeTargets.Class )]
    public class ExposeInDesignerAttribute : Attribute
    {
        public readonly string      displayName;
        public readonly string      groupCategory;

        public ExposeInDesignerAttribute( string name, string category )
        {
            displayName     = name;
            groupCategory   = category;
        }
    }
}