using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UForms.Attributes;
using UForms.Controls;

using UnityEngine;

namespace UForms.Designer
{
    // Caches all UForms Controls flagged with ExposeInDesigner attribute in current executing assembly (which should be Project.CSharp.Editor)
    public class ToolboxControlCache
    {
        public struct CachedControl
        {
            public Type     type;
            public string   name;
            public string   category;
        }


        public List<string>         Categories { get { return m_categories; } }
        public List<CachedControl>  Controls { get { return m_cachedControls; } }


        private List< CachedControl > m_cachedControls;
        private List< string >        m_categories;


        public ToolboxControlCache()
        {
            m_cachedControls = new List<CachedControl>();
            m_categories     = new List<string>();

            CacheControls();
        }


        private void CacheControls()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Type[] types = asm.GetTypes();

            foreach ( Type t in types )
            {
                if ( t.IsSubclassOf( typeof( Control ) ) )
                {
                    object[] attribs = t.GetCustomAttributes( typeof( ExposeControlAttribute ), false );
                    
                    if ( attribs.Length > 0 )
                    {
                        CacheType( t, (ExposeControlAttribute)attribs[0] );
                    }
                }
            }
        }


        private void CacheType( Type type, ExposeControlAttribute attrib )
        {
            CachedControl control;

            control.type     = type;
            control.name     = attrib.displayName;
            control.category = attrib.groupCategory;

            m_cachedControls.Add( control );

            if ( !m_categories.Contains( control.category ) )
            {
                m_categories.Add( control.category );
            }
        }
    }
}