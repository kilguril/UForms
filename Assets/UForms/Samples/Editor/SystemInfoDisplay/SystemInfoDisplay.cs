using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Application;
using UForms.Controls;
using UForms.Controls.Fields;
using UForms.Controls.Extended;
using UForms.Decorators;
using UForms.Events;

public class SystemInfoDisplay : UFormsApplication 
{    
    [MenuItem("UForms Samples/Display System Info")]
    private static void Run()
    {
        EditorWindow.GetWindow< SystemInfoDisplay >( true, "System Info", true );
    }


    private static string UDID = SystemInfo.deviceUniqueIdentifier;
    private const float LIST_INDENTATION_PIXELS = 16.0f;


    protected override void OnInitialize()
    {
        // Create a container control that will stack several foldouts with categorized system information
        // Control will fill 100% of our viewport, and will create vertical scrollbars if necesary
        Control sysinfo = new Control();
        sysinfo.AddDecorator( new StackContent( StackContent.StackMode.Vertical, StackContent.OverflowMode.Flow ) );
        sysinfo.AddDecorator( new Scrollbars( true, false, true ) );
        sysinfo.SetSize( 100.0f, 100.0f, Control.MetricsUnits.Percentage, Control.MetricsUnits.Percentage );

        // Create a system information foldout list that will categorize our system information into general and feature categories
        // Don't forget to set width to 100% of the container width
        FoldoutList system = new FoldoutList( "System", LIST_INDENTATION_PIXELS, true );
        system.SetWidth( 100.0f, Control.MetricsUnits.Percentage );

        // Create a new foldout list to contain general system infromation and populate it with data
        // Child foldouts will stretch horizontally to fill the container
        FoldoutList systemGeneral = new FoldoutList( "General", LIST_INDENTATION_PIXELS, true );
        systemGeneral.SetWidth( 100.0f, Control.MetricsUnits.Percentage );
        systemGeneral.AddItem( new LabelField( SystemInfo.deviceName, "Device Name:" ) );
        
        // Trying to access SystemInfo.deviceUniqueIdentifier from OnGUI seems to made Unity bleed internally, so we pre-cache it
        systemGeneral.AddItem( new LabelField( UDID, "UDID:" ) );
        
        systemGeneral.AddItem( new LabelField( SystemInfo.deviceModel, "Model:" ) );
        systemGeneral.AddItem( new LabelField( SystemInfo.deviceType.ToString(), "Type:" ) );
        systemGeneral.AddItem( new LabelField( SystemInfo.processorType, "Processor Type:" ) );
        systemGeneral.AddItem( new LabelField( SystemInfo.processorCount.ToString(), "Processor Count:" ) );
        systemGeneral.AddItem( new LabelField( string.Format( "{0} MB", SystemInfo.systemMemorySize ), "System Memory:" ) );
        systemGeneral.AddItem( new LabelField( SystemInfo.operatingSystem, "Operating System:" ) );

        // Second list for system features
        FoldoutList systemFeatures = new FoldoutList( "Features", LIST_INDENTATION_PIXELS, true );
        systemFeatures.SetWidth( 100.0f, Control.MetricsUnits.Percentage );

        systemFeatures.AddItem( new LabelField( SystemInfo.supportsVibration.ToString(), "Vibration:" ) );
        systemFeatures.AddItem( new LabelField( SystemInfo.supportsGyroscope.ToString(), "Gyroscope:" ) );
        systemFeatures.AddItem( new LabelField( SystemInfo.supportsAccelerometer.ToString(), "Accelerometer:" ) );
        systemFeatures.AddItem( new LabelField( SystemInfo.supportsLocationService.ToString(), "Location Service:" ) );

        // Add both category lists to the system list
        system.AddItem( systemGeneral );
        system.AddItem( systemFeatures );


        // Now recreate the previous structure for graphics information with 3 subcategories for general, features and texture support
        FoldoutList graphics = new FoldoutList( "Graphics Device", LIST_INDENTATION_PIXELS, true );
        graphics.SetWidth( 100.0f, Control.MetricsUnits.Percentage );

        FoldoutList graphicsGeneral = new FoldoutList( "General", LIST_INDENTATION_PIXELS, true );
        graphicsGeneral.SetWidth( 100.0f, Control.MetricsUnits.Percentage );
        graphicsGeneral.AddItem( new LabelField( SystemInfo.graphicsDeviceID.ToString(), "ID:" ) );
        graphicsGeneral.AddItem( new LabelField( SystemInfo.graphicsDeviceName, "Name:" ) );
        graphicsGeneral.AddItem( new LabelField( SystemInfo.graphicsDeviceVendorID.ToString(), "VendorID:" ) );
        graphicsGeneral.AddItem( new LabelField( SystemInfo.graphicsDeviceVendor, "Vendor:" ) );
        graphicsGeneral.AddItem( new LabelField( SystemInfo.graphicsDeviceVersion, "Version:" ) );
        graphicsGeneral.AddItem( new LabelField( string.Format( "{0} MB", SystemInfo.graphicsMemorySize ), "Memory:" ) );
        graphicsGeneral.AddItem( new LabelField( SystemInfo.graphicsPixelFillrate.ToString(), "Fillrate:" ) );
        graphicsGeneral.AddItem( new LabelField( SystemInfo.graphicsShaderLevel.ToString(), "Shader Level:" ) );

        FoldoutList graphicsFeatures = new FoldoutList( "Features", LIST_INDENTATION_PIXELS, true );
        graphicsFeatures.SetWidth( 100.0f, Control.MetricsUnits.Percentage );
        graphicsFeatures.AddItem( new LabelField( SystemInfo.supportedRenderTargetCount.ToString(), "Render Target Count:" ) );
        graphicsFeatures.AddItem( new LabelField( SystemInfo.supports3DTextures.ToString(), "3D Textures:" ) );
        graphicsFeatures.AddItem( new LabelField( SystemInfo.supportsComputeShaders.ToString(), "Compute Shaders:" ) );
        graphicsFeatures.AddItem( new LabelField( SystemInfo.supportsImageEffects.ToString(), "Image Effects:" ) );
        graphicsFeatures.AddItem( new LabelField( SystemInfo.supportsInstancing.ToString(), "Instancing:" ) );
        graphicsFeatures.AddItem( new LabelField( SystemInfo.supportsRenderTextures.ToString(), "Render Textures:" ) );
        graphicsFeatures.AddItem( new LabelField( SystemInfo.supportsRenderToCubemap.ToString(), "Render To Cubemap:" ) );
        graphicsFeatures.AddItem( new LabelField( SystemInfo.supportsShadows.ToString(), "Built-in Shdows:" ) );
        graphicsFeatures.AddItem( new LabelField( SystemInfo.supportsSparseTextures.ToString(), "Sparse Textures:" ) );
        graphicsFeatures.AddItem( new LabelField( SystemInfo.supportsStencil.ToString(), "Stencil:" ) );

        FoldoutList graphicsTextures = new FoldoutList( "Texture Support", LIST_INDENTATION_PIXELS, true );
        graphicsTextures.SetWidth( 100.0f, Control.MetricsUnits.Percentage );
        graphicsTextures.AddItem( new LabelField( SystemInfo.npotSupport.ToString(), "Non Power of Two:" ) );

        foreach ( RenderTextureFormat format in System.Enum.GetValues( typeof( RenderTextureFormat ) ) )
        {
            graphicsTextures.AddItem( new LabelField( SystemInfo.SupportsRenderTextureFormat( format ).ToString(), format.ToString() ) );
        }


        graphics.AddItem( graphicsGeneral );
        graphics.AddItem( graphicsFeatures );
        graphics.AddItem( graphicsTextures );

        // Add top level lists to our container element
        sysinfo.AddChild( system );
        sysinfo.AddChild( graphics );

        // Attach parent container
        AddChild( sysinfo );
    }
}
