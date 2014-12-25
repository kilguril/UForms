using UnityEngine;
using System.Collections;

namespace UForms.Designer
{
    public class UFormsDesignerTemplates
    {
    
    
// {0} - className
// {1} - fields
// {2} - initialization
// {3} - timestamp
        public const string TEMPLATE_LAYOUT = 
@"
using UnityEngine;
using UForms.Attributes;

[ExposeControl(""{0}"", ""User"")]
public partial class {0} : UForms.Controls.Control
{{
    // This file was auto generated on {3}
    // DO NOT MODIFY MANUALLY!    

{1}
    public {0}() : base()
    {{
{2}        

        InitializeUser();
    }}
}}
";

        public const string TEMPLATE_USER   = 
@"
// Use this file to define interactions and additional information. This file will not get regenerated when modifying control.

public partial class {0} : UForms.Controls.Control
{{
    // Initialization method, called after all the children have been created
    void InitializeUser()
    {{
        
    }}
}}
";

    }
}