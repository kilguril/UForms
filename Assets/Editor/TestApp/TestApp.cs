using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Application;
using UForms.Controls;
using UForms.Controls.Panels;
using UForms.Controls.Fields;
using UForms.Controls.Dropdowns;
using UForms.Controls.Sliders;

public class TestApp : UFormsApplication
{
    [MenuItem( "Test/TestApp" )]
    private static void Run()
    {
        EditorWindow.GetWindow<TestApp>();        
    }

    protected override void OnInitialize()
    {
        StackPanel root = new StackPanel( Vector2.zero, new Vector2( 600.0f, 0.0f ), StackPanel.StackMode.Vertical );
        root.FillContainerVertical   = true;
        root.FillContainerHorizontal = false;
        root.HorizontalScrollbar     = true;
        root.VerticalScrollbar       = true;

        FoldPanel devicePanel = new FoldPanel( "Device:" );
        devicePanel.FillContainerHorizontal = true;

        devicePanel.AddChild( new LabelField( SystemInfo.deviceName,                  "Name:" ) );
        devicePanel.AddChild( new LabelField( SystemInfo.deviceUniqueIdentifier,      "UDID:" ) );
        devicePanel.AddChild( new LabelField( SystemInfo.deviceModel,                 "Model:" ) );
        devicePanel.AddChild( new LabelField( SystemInfo.deviceType.ToString(),       "Type:" ) );
        devicePanel.AddChild( new LabelField( SystemInfo.processorType,               "Processor Type:") );
        devicePanel.AddChild( new LabelField( SystemInfo.processorCount.ToString(),   "Processor Count:" ) );
        devicePanel.AddChild( new LabelField( string.Format("{0} MB", SystemInfo.systemMemorySize), "System Memory:" ) );
        devicePanel.AddChild( new LabelField( SystemInfo.operatingSystem,             "Operating System:" ) );
        
        SetControlsStyle( devicePanel.Children );


        FoldPanel featuresPanel = new FoldPanel( "Features:" );
        featuresPanel.FillContainerHorizontal = true;

        featuresPanel.AddChild( new LabelField( SystemInfo.supportsVibration.ToString(), "Vibration:" ) );
        featuresPanel.AddChild( new LabelField( SystemInfo.supportsGyroscope.ToString(), "Gyroscope:" ) );
        featuresPanel.AddChild( new LabelField( SystemInfo.supportsAccelerometer.ToString(), "Accelerometer:" ) );
        featuresPanel.AddChild( new LabelField( SystemInfo.supportsLocationService.ToString(), "Location Service:" ) );

        SetControlsStyle( featuresPanel.Children );


        FoldPanel graphicsPanel = new FoldPanel( "Graphics Device:" );
        graphicsPanel.FillContainerHorizontal = true;

        graphicsPanel.AddChild( new LabelField( SystemInfo.graphicsDeviceID.ToString(), "ID:" ) );
        graphicsPanel.AddChild( new LabelField( SystemInfo.graphicsDeviceName, "Name:" ) );
        graphicsPanel.AddChild( new LabelField( SystemInfo.graphicsDeviceVendorID.ToString(), "VendorID:" ) );
        graphicsPanel.AddChild( new LabelField( SystemInfo.graphicsDeviceVendor, "Vendor:" ) );
        graphicsPanel.AddChild( new LabelField( SystemInfo.graphicsDeviceVersion, "Version:" ) );
        graphicsPanel.AddChild( new LabelField( string.Format( "{0} MB", SystemInfo.graphicsMemorySize ), "Memory:" ) );
        graphicsPanel.AddChild( new LabelField( SystemInfo.graphicsPixelFillrate.ToString(), "Fillrate:" ) );
        graphicsPanel.AddChild( new LabelField( SystemInfo.graphicsShaderLevel.ToString(), "Shader Level:" ) );

        SetControlsStyle( graphicsPanel.Children );


        FoldPanel gfeaturePanel = new FoldPanel( "Graphics Features:" );
        gfeaturePanel.FillContainerHorizontal = true;

        gfeaturePanel.AddChild( new LabelField( SystemInfo.supportedRenderTargetCount.ToString(), "Render Target Count:") );
        gfeaturePanel.AddChild( new LabelField( SystemInfo.supports3DTextures.ToString(), "3D Textures:" ) );
        gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsComputeShaders.ToString(), "Compute Shaders:" ) );
        gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsImageEffects.ToString(), "Image Effects:" ) );
        gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsInstancing.ToString(), "Instancing:" ) );
        gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsRenderTextures.ToString(), "Render Textures:" ) );
        gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsRenderToCubemap.ToString(), "Render To Cubemap:" ) );
        gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsShadows.ToString(), "Built-in Shdows:" ) );
        gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsSparseTextures.ToString(), "Sparse Textures:" ) );
        gfeaturePanel.AddChild( new LabelField( SystemInfo.supportsStencil.ToString(), "Stencil:" ) );

        SetControlsStyle( gfeaturePanel.Children );


        FoldPanel texPanel = new FoldPanel( "Texture Support:" );
        texPanel.FillContainerHorizontal = true;

        texPanel.AddChild( new LabelField( SystemInfo.npotSupport.ToString(), "Non Power of Two:" ) );        

        foreach( RenderTextureFormat format in System.Enum.GetValues( typeof( RenderTextureFormat ) ) )
        {
            texPanel.AddChild( new LabelField( SystemInfo.SupportsRenderTextureFormat( format ).ToString(), format.ToString() ) );
        }

        SetControlsStyle( texPanel.Children );


        root.AddChild( devicePanel );
        root.AddChild( featuresPanel );
        root.AddChild( graphicsPanel );
        root.AddChild( gfeaturePanel );
        root.AddChild( texPanel );
        AddControl( root );
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
