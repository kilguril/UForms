using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using System.CodeDom.Compiler;

using UForms.Controls;
using UForms.Controls.Fields;
using UForms.Decorators;
using System.Reflection;
using System;


namespace UForms.Designer
{
    public class UFormsCodeGenerator
    {
        private List< string > m_ids;        
        
        public bool ValidateIDs( Control control )
        {
            m_ids = new List<string>();

            if ( string.IsNullOrEmpty( control.Id ) )
            {
                Debug.LogError( "Root control ID is empty! Please assign an ID that will be used as the class name for the control. " );
                return false;
            }

            if ( !CodeGenerator.IsValidLanguageIndependentIdentifier( control.Id ) )
            {
                Debug.LogError( string.Format( "Invalid ID provided for root control, `{0}` is not a valid identifier", control.Id ) );
                return false;
            }

            foreach( Control child in control.Children )
            {
                if ( string.IsNullOrEmpty( child.Id ) )
                {
                    Debug.LogError( string.Format( "Control {0} does not have an id specified. Please assign an ID that will be used as the field name.", child ) );
                    return false;
                }

                if ( !CodeGenerator.IsValidLanguageIndependentIdentifier( child.Id ) )
                {
                    Debug.LogError( string.Format( "Invalid ID provided for control `{0}`, `{1}` is not a valid identifier", child, child.Id ) );
                    return false;
                }

                if ( m_ids.Contains( child.Id ) )
                {
                    Debug.LogError( string.Format( "Duplicate ID provided for control `{0}`, `{1}` is not unique", child, child.Id ) );
                    return false;
                }

                m_ids.Add( child.Id );
            }

            return true;
        }


        public string GenerateLayout( Control control )
        {
            string className      = control.Id;
            string timestamp      = string.Format( "{0} {1}", System.DateTime.Now.ToShortDateString(), System.DateTime.Now.ToShortTimeString());
            string fields         = "\t";
            string initialization = "";

            initialization = string.Format( "{0}{1}\n", initialization, GenerateInitializer( control, true ) );

            foreach( Control child in control.Children )
            {
                fields = string.Format( "{0}private {1} {2};{3}\t", fields, child.GetType(), child.Id, System.Environment.NewLine );
                initialization = string.Format( "{0}{1}\n\n", initialization, GenerateInitializer( child, false ) );
            }

            return string.Format( UFormsDesignerTemplates.TEMPLATE_LAYOUT, className, fields, initialization, timestamp );
        }


        private string GenerateInitializer( Control control, bool isThis )
        {
            string output = "";

            string id   = isThis ? "this" : control.Id;
            string type = control.GetType().ToString();

            if ( !isThis )
            {
                output = string.Format( "\n\t\t{0} = new {1}();\n", id, type );
            }

            output = string.Format( "{0}{1}", output, GenerateFieldInitializers( control, id ) );

            Control c = (Control)Activator.CreateInstance( control.GetType() );

            int decoratorIndex = 0;
            int count = 0;
            int builtinDecoratos = c.Decorators.Count;

            foreach( Decorator decorator in control.Decorators )
            {
                count++;

                if ( count <= builtinDecoratos )
                {
                    continue; 
                }

                string decoratorId = string.Format( "{0}_d{1}", id, decoratorIndex );

                output = string.Format( "{0}\n\n\t\t{1} {2} = new {1}();", output, decorator.GetType(), decoratorId );
                output = string.Format( "{0}{1}", output, GenerateFieldInitializers( decorator, decoratorId ) );
                output = string.Format( "{0}\n\t\t{1}.AddDecorator({2});", output, id, decoratorId );

                decoratorIndex++;
            }

            if ( !isThis )
            {
                output = string.Format( "{0}\n\n\t\tAddChild({1});", output, id );
            }

            return output;
        }

        private string GenerateFieldInitializers( object obj, string owner )
        {
            string output = "";

            Type t = obj.GetType();
            PropertyInfo[] props = t.GetProperties( BindingFlags.Public | BindingFlags.Instance );
            foreach ( PropertyInfo prop in props )
            {
                object[] attr = prop.GetCustomAttributes( typeof( HideInInspector ), false );

                if ( attr.Length == 0 )
                {
                    output = string.Format("{0}{1}\t\t{2}.{3} = {4};", output, System.Environment.NewLine, owner, prop.Name, GenerateField( prop, obj ));                     
                }
            }

            return output;
        }


        private string GenerateField( PropertyInfo prop, object obj )
        {
            if ( prop.PropertyType == typeof( int ) )
            {
                int val = ( int )prop.GetValue( obj, null );
                return string.Format( "{0}", val );
            }
            else if ( prop.PropertyType == typeof( float ) )
            {
                float val = ( float )prop.GetValue( obj, null );
                return string.Format( "{0}f", val );
            }
            else if ( prop.PropertyType == typeof( Vector2 ) )
            {
                Vector2 val = ( Vector2 )prop.GetValue( obj, null );
                return string.Format( "new Vector2({0}f,{1}f)", val.x, val.y );
            }
            else if ( prop.PropertyType == typeof( Vector3 ) )
            {
                Vector3 val = ( Vector3 )prop.GetValue( obj, null );
                return string.Format( "new Vector2({0}f,{1}f,{2}f)", val.x, val.y, val.z );
            }
            else if ( prop.PropertyType == typeof( Vector4 ) )
            {
                Vector4 val = ( Vector4 )prop.GetValue( obj, null );
                return string.Format( "new Vector2({0}f,{1}f,{2}f,{3}f)", val.x, val.y, val.z, val.w );
            }
            else if ( prop.PropertyType == typeof( string ) )
            {
                string val = ( string )prop.GetValue( obj, null );
                return string.Format("\"{0}\"", val );
            }
            else if ( prop.PropertyType == typeof( Rect ) )
            {                
                Rect val = ( Rect )prop.GetValue( obj, null );
                return string.Format( "new Rect({0}f,{1}f,{2}f,{3}f)", val.xMin, val.yMin, val.width, val.height );
            }
            else if ( prop.PropertyType == typeof( Color ) )
            {
                Color val = ( Color )prop.GetValue( obj, null );                
                return string.Format( "new Color({0}f,{1}f,{2}f,{3}f)", val.r, val.g, val.b, val.a );
            }
            else if ( prop.PropertyType == typeof( Bounds ) )
            {
                Bounds val = ( Bounds )prop.GetValue( obj, null );
                Vector3 center = val.center;
                Vector3 size   = val.size;
                return string.Format( "new Bounds( new Vector3({0}f,{1}f,{2}f), new Vector3({3}f,{4}f,{5}f))", center.x,center.y,center.z,size.x,size.y,size.z );
            }
            else if ( prop.PropertyType.IsEnum )
            {
                Enum e = ( Enum )prop.GetValue( obj, null );
                return string.Format( "{0}.{1}", e.GetType().ToString().Replace('+','.'), e );
            }
            else if ( prop.PropertyType == typeof( bool ) )
            {
                bool val = ( bool )prop.GetValue( obj, null );
                return val.ToString().ToLowerInvariant();
            }

            // Best case effort for unsupported types
            return prop.GetValue( obj, null ).ToString();
        }

        public string GenerateUseCode( Control control )
        {
            string className      = control.Id;
            return string.Format( UFormsDesignerTemplates.TEMPLATE_USER, className );
        }


        public string GenerateLayoutPath( string path )
        {
            int lastDot = path.LastIndexOf( '.' );
            return path.Insert( lastDot, ".layout");
        }
    }
}