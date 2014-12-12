UForms 
======
**_A UI Framework for Unity3D Editor Applications_**


## About

UForms is a UI framework for creating in editor applications for the Unity3D editor. It's main focus is to improve the existing workflow for editor application development by providing a clear and concise UI toolkit, reducing the amount of code required to define the interface and user interactions of the application and allowing to put more emphasis on what truly matters - the functional parts.

Additionally, it strives to modularize the concept of UI elements. If you've implemented a combo box control two times, that's one time too many. For the time being, there are well accepted conventions when in comes to window based interfaces, and there is no sensible reason to reinvent the wheel every single time.

I've discovered the need for such solution when I started working on several editor extensions for Unity3D. With every line of functional code I'd add to the application, the UI part would seem to grow by three lines. I hope a solution such as this can bring a new perspective to the decision process when considering extending the editor. How much will it cost to create a bare minimum functional UI should be a marginal consideration when weighting the pros and cons of creating tools that aim to improve our workflow.


## License

UForms is available under the MIT license.
See **LICENSE.md** for more information.


## Design Philosophy

UForms sets some lofty goals. This section overviews the design philosophy behind UForms which aims to meet these goals.

* **Object Oriented** - UI elements in UForms are objects and strive to be as self contained as possible. Each element is responsible for managing it's own internal state.

* **Event Driven** - UI elements communicate relevant information through events. Developers only need to handle the relevant events they find interesting and useful for their application.

* **Implementation Over Invention** - UForms tries not to invent anything. In fact, it tries to closely mimic some of the design patterns found in WinForms / WPF, hoping to leverege the power of familiarity for C# developers and the compactness of user code required to create interactions in these frameworks.

* **Sensible Defaults** - UForms tries to further compact the amount of declarations required by creating a set of sensible defaults. While there are many aspects a UI control can be customized in, they should be useful out-of-the-box without the need to manually declare every single bit of information.


## Should I Use UForms

If you are developing a UI driven editor extension for Unity, you should definitely give it a try.

However, there are some consideration which needs to be taken in some cases, where UForms might not give any real benefit, especially considering this framework is still an early work in progress, with it's own set of problems, and is subject to change to some extent.

* **My application has no UI** - I think this one is a simple case. If you don't plan on having any UI (for example custom build processes or actions launched via context menus) there really isn't any benefit you can gain from using a UI framework.

* **My application has minimal UI components** - if your application is centered around 3 buttons and a label, there really isn't any sense in adding unnecessary complexity. How much is enough UI to justify using a framework? well, that's something very subjective - for me the breakdown is the point where I feel the attention put on UI work is a resource drain.

* **I have a rigid deadline** - again, matter of personal preference. I wouldn't pick up anything experimental where there is no time for experimenting.
 
* **My core bussiness is my editor application** - simply put, if your core business is your application, you probably have more available resources to allocate into tailoring the perfect solution for your needs. When using an external framework there's always the inherent risk of it not fitting perfectly to your needs, and even with open source software, sometimes the effort required to adapt to a specific use case is much higher than solving that use case from the grounds up. 

## Getting Started

Following is a series of quick getting started examples. You can find more complete samples in the project folder under _UForms/Samples_.

### Creating an Application

To set up a basic application, follow these steps:

1. Include the `UForms.Application` and `UnityEditor` namespaces.

2. Define a class deriving from `UFormsApplication`.
 
3. Make sure your source file is located in an `/Editor/` folder.

4. Use the `OnInitialize()` method to set up initial values.

5. Launch it using Unity's `EditorWindow.GetWindow<T>` method. Most straight-forward way is by adding a menu item to do that.
 
The most basic setup should look something along these lines:

```c#
using UnityEditor;
using UForms.Application;

public class FooApplication  : UFormsApplication
{
    [MenuItem("Foo/Bar")]
    private static void Run()
    {
        EditorWindow.GetWindow< FooApplication >();
    }

    protected override void OnInitialize()
    {
        title = "Foo";
    }
}
```

### Hello, Form!

The following example shows the process of adding controls to the application.

1. Include the `UForms.Controls` namespace.

2. Construct a new control, in case of a label it would look like this: `Label label = new Label("Label Content")`.

3. Add the control to the display list using `AddChild( label )`;

Additionally, some controls will raise events you might want to react to. This is as simple as registering an event handler:

```c#
  // Handle the button click event
  m_button.Clicked += m_button_Clicked;
```

And implementing the handler:
```c#
  // Button click event handler
  void m_button_Clicked( IClickable sender, ClickEventArgs args, Event nativeEvent )
  {
    Debug.Log("Click!");  
  }
```

Putting all of this into practice creates a truly impressing Hell World application:

```c#
using UnityEngine;
using UnityEditor;

using UForms.Application;
using UForms.Controls;
using UForms.Events;

public class HelloApplication  : UFormsApplication
{
    [MenuItem("Hello/Form")]
    private static void Run()
    {
        EditorWindow.GetWindow< HelloApplication >();
    }


    private Label  m_label;
    private Button m_button;

    protected override void OnInitialize()
    {
        title = "Hello";

        // Initialize the label
        m_label = new Label( "Click the button for hello!" );
        m_label.SetPosition( 10.0f, 10.0f );
        m_label.SetSize( 200.0f, 20.0f );

        // Add the label to the display list
        AddChild( m_label );        

        // Initialize the button
        m_button = new Button( "The Button" );
        m_button.SetPosition( 10.0f, 40.0f );
        m_button.SetSize( 100.0f, 20.0f );

        // Add the button to the display list
        AddChild( m_button );

        // Handle the button click event
        m_button.Clicked += m_button_Clicked;
    }

    // Button click event handler
    void m_button_Clicked( IClickable sender, ClickEventArgs args, Event nativeEvent )
    {
        if ( args.button == MouseButton.Right )
        {
            EditorUtility.DisplayDialog( "Have You No Manners?", "It is rude to righ click a button!", "Ok..." );
        }
        else
        {
            m_label.Text = "Hello!";
        }        
    }
}
```

### Creating a Custom Control

There are two methods of creating custom controls.

The first, which I won't get into is creating a whole new control by implementing it's `OnDraw()` method. This is not very useful as UForms already wraps _almost_ all of the editor's built in controls.

The second method is by aggregating several controls and functionality to create a specialized, re-usable control. For example a Transform control which is basically a collection of three Vector3 fields. The process of creating a new control is pretty straight-forward. All controls have display hierarchies and can contain other controls underneath them. The process is fairly similar to the previous example, only instead of creating an application you would create a control which will then be used in the application.

```c#
using UnityEngine;

using System.Collections;

using UForms.Controls;
using UForms.Controls.Fields;
using UForms.Decorators;

public class TransformControl : Control
{
    public Vector3 Translate 
    { 
        get { return m_translate.Value; }
        set { m_translate.Value = value; } 
    }

    public Vector3 Rotate
    {
        get { return m_rotate.Value; }
        set { m_rotate.Value = value; }
    }


    public Vector3 Scale
    {
        get { return m_scale.Value; }
        set { m_scale.Value = value; }
    }

    private Vector3Field    m_translate;
    private Vector3Field    m_rotate;
    private Vector3Field    m_scale;

    protected override Vector2 DefaultSize
    {
        get { return new Vector2( 200.0f, 114.0f ); }
    }

    public TransformControl()
    {
        AddDecorator( new StackContent( StackContent.StackMode.Vertical, StackContent.OverflowMode.Flow ) );

        m_translate = new Vector3Field( Vector3.zero, "Translate" );
        m_rotate = new Vector3Field( Vector3.zero, "Rotate" );
        m_scale = new Vector3Field( Vector3.zero, "Scale" );

        m_translate.SetWidth( 100.0f, MetricsUnits.Percentage );
        m_rotate.SetWidth( 100.0f, MetricsUnits.Percentage );
        m_scale.SetWidth( 100.0f, MetricsUnits.Percentage );

        AddChild( m_translate );
        AddChild( m_rotate );
        AddChild( m_scale );
    }
}
```

## Roadmap

While UForms is a side project, and more of a means than an end, there are some key features that are still missing which I would love to see implemented at some point. While there is no clear time table for any of the mentioned below, this list touches most of the issues that need to be addressed at some point.

* Add GUIContent support, either to existing controls or by introducing GUIContent enabled counterparts.
* Add more control over visual aspects, currently the ability to style controls is very limited.
* Add an option to set global styling for things such as text color and margins.
* Rethink the whole decorator model for controls, current decorator seem to break more often than they work properly.
* Improve layouting.
