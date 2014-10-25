using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Application;
using UForms.Controls;
using UForms.Controls.Fields;
using UForms.Controls.Dropdowns;
using UForms.Controls.Sliders;
using UForms.Decorators;

public class TestApp : UFormsApplication
{
    [MenuItem( "Test/TestApp" )]
    private static void Run()
    {
        EditorWindow.GetWindow<TestApp>();        
    }

    protected override void OnInitialize()
    {
        Control c = new Control( Vector2.zero, new Vector2( 100.0f, 100.0f ) );
        Button b = new Button();
        b.SetPosition( 10.0f, 10.0f );

        c.AddChild( b );

        //c.AddChild( new Button() );
        //c.AddChild( new Button() );
        //c.AddChild( new Button() );

        c.SetPosition( 20.0f, 20.0f );
        c.AddDecorator( new FitContent( true, true, true, true ) );
        //c.AddDecorator( new ClipContent() );
        //c.AddDecorator( new Scrollbars( true, true, true ) );
        //c.AddDecorator( new StackContent( StackContent.StackMode.Vertical, StackContent.OverflowMode.Flow ) );

        AddControl( c );

        //ScrollPanel panel = new ScrollPanel( true, true, false );
        //panel.SetSize( 100.0f, 100.0f, Control.MetricsUnits.Percentage, Control.MetricsUnits.Percentage );

        //panel.AddChild( new Button().SetPosition( 0.0f, 0.0f ) );
        //panel.AddChild( new Button().SetPosition( 0.0f, 30.0f ) );
        //panel.AddChild( new Button().SetPosition( 0.0f, 60.0f ) );
        //panel.AddChild( new Button().SetPosition( 0.0f, 90.0f ) );
        //panel.AddChild( new Button().SetPosition( 0.0f, 120.0f ) );
        //panel.AddChild( new Button().SetPosition( 0.0f, 150.0f ) );
        //panel.AddChild( new Button().SetPosition( 0.0f, 180.0f ) );

        //AddControl( panel );

        //StackPanel root = new StackPanel( StackPanel.StackMode.Vertical );
        //root.SetSize( 100.0f, 100.0f, Control.MetricsUnits.Percentage, Control.MetricsUnits.Percentage );
        //root.HorizontalScrollbar = true;
        //root.VerticalScrollbar = true;

        //FoldPanel devicePanel = new FoldPanel( "Device:" );
        //devicePanel.AddChild( new LabelField( SystemInfo.deviceName, "Name:" ) );
        //devicePanel.AddChild( new LabelField( SystemInfo.deviceUniqueIdentifier, "UDID:" ) );
        //devicePanel.AddChild( new LabelField( SystemInfo.deviceModel, "Model:" ) );
        //devicePanel.AddChild( new LabelField( SystemInfo.deviceType.ToString(), "Type:" ) );
        //devicePanel.AddChild( new LabelField( SystemInfo.processorType, "Processor Type:" ) );
        //devicePanel.AddChild( new LabelField( SystemInfo.processorCount.ToString(), "Processor Count:" ) );
        //devicePanel.AddChild( new LabelField( string.Format( "{0} MB", SystemInfo.systemMemorySize ), "System Memory:" ) );
        //devicePanel.AddChild( new LabelField( SystemInfo.operatingSystem, "Operating System:" ) );

        //SetControlsStyle( devicePanel.Children );


        //FoldPanel featuresPanel = new FoldPanel( "Features:" );
        //featuresPanel.AddChild( new LabelField( SystemInfo.supportsVibration.ToString(), "Vibration:" ) );
        //featuresPanel.AddChild( new LabelField( SystemInfo.supportsGyroscope.ToString(), "Gyroscope:" ) );
        //featuresPanel.AddChild( new LabelField( SystemInfo.supportsAccelerometer.ToString(), "Accelerometer:" ) );
        //featuresPanel.AddChild( new LabelField( SystemInfo.supportsLocationService.ToString(), "Location Service:" ) );

        //SetControlsStyle( featuresPanel.Children );


        //FoldPanel graphicsPanel = new FoldPanel( "Graphics Device:" );
        //graphicsPanel.AddChild( new LabelField( SystemInfo.graphicsDeviceID.ToString(), "ID:" ) );
        //graphicsPanel.AddChild( new LabelField( SystemInfo.graphicsDeviceName, "Name:" ) );
        //graphicsPanel.AddChild( new LabelField( SystemInfo.graphicsDeviceVendorID.ToString(), "VendorID:" ) );
        //graphicsPanel.AddChild( new LabelField( SystemInfo.graphicsDeviceVendor, "Vendor:" ) );
        //graphicsPanel.AddChild( new LabelField( SystemInfo.graphicsDeviceVersion, "Version:" ) );
        //graphicsPanel.AddChild( new LabelField( string.Format( "{0} MB", SystemInfo.graphicsMemorySize ), "Memory:" ) );
        //graphicsPanel.AddChild( new LabelField( SystemInfo.graphicsPixelFillrate.ToString(), "Fillrate:" ) );
        //graphicsPanel.AddChild( new LabelField( SystemInfo.graphicsShaderLevel.ToString(), "Shader Level:" ) );

        //SetControlsStyle( graphicsPanel.Children );


        //FoldPanel gfeaturePanel = new FoldPanel( "Graphics Features:" );
        //gfeaturePanel.AddChild( new LabelField( SystemInfo.supportedRenderTargetCount.ToString(), "Render Target Count:" ) );
        //gfeaturePanel.AddChild( new LabelField( SystemInfo.supports3DTextures.ToString(), "3D Textures:" ) );
        //gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsComputeShaders.ToString(), "Compute Shaders:" ) );
        //gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsImageEffects.ToString(), "Image Effects:" ) );
        //gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsInstancing.ToString(), "Instancing:" ) );
        //gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsRenderTextures.ToString(), "Render Textures:" ) );
        //gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsRenderToCubemap.ToString(), "Render To Cubemap:" ) );
        //gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsShadows.ToString(), "Built-in Shdows:" ) );
        //gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsSparseTextures.ToString(), "Sparse Textures:" ) );
        //gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsStencil.ToString(), "Stencil:" ) );

        //SetControlsStyle( gfeaturePanel.Children );


        //FoldPanel texPanel = new FoldPanel( "Texture Support:" );
        //texPanel.AddChild( new LabelField( SystemInfo.npotSupport.ToString(), "Non Power of Two:" ) );

        //foreach ( RenderTextureFormat format in System.Enum.GetValues( typeof( RenderTextureFormat ) ) )
        //{
        //    texPanel.AddChild( new LabelField( SystemInfo.SupportsRenderTextureFormat( format ).ToString(), format.ToString() ) );
        //}

        //SetControlsStyle( texPanel.Children );


        //root.AddChild( devicePanel );
        //root.AddChild( featuresPanel );
        //root.AddChild( graphicsPanel );
        //root.AddChild( gfeaturePanel );
        //root.AddChild( texPanel );
        //AddControl( root );
    }

    void SetControlsStyle( List< Control > controls )
    {
        for ( int i = 1; i < controls.Count; i++ )
        {
            Control control = controls[ i ];

            control.MarginLeftTop = new Vector2( 32.0f, 0.0f );
            control.SetSize( 600.0f, 16.0f );
        }
    }

    protected override void OnGUI()
    {        
        base.OnGUI();
    }


}
