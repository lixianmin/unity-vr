
/********************************************************************
created:    2022-08-27
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using System;
using System.Collections.Generic;
using Unicorn.AutoCode;
using UnityEditor;

namespace Unicorn.Kit
{
    public class MakeAutoCode
    {
        internal void _WriteKitFactory(string fullPath)
        {
            using (_writer = new CodeWriter(fullPath))
            {
                _WriteFileHead();

                using (CodeScope.CreateCSharpScope(_writer))
                {
                    _writer.WriteLine("public class _KitFactory");

                    using (CodeScope.CreateCSharpScope(_writer))
                    {
                        _WriteMetaFactory_GetLookupTableByType();
                    }
                }
            }
            
            AssetDatabase.Refresh();
        }
        
        private void _WriteMetaFactory_GetLookupTableByType ()
        {
            _writer.WriteLine("private static Hashtable _GetLookupTableByName ()");

            using (CodeScope.CreateCSharpScope(_writer))
            {
                var subTypes = System.Linq.Enumerable.ToList(CollectSubTypes(typeof(KitBase)));
                var count = subTypes.Count;
                _writer.WriteLine("return new Hashtable({0})", count.ToString());

                using (CodeScope.Create(_writer, "{\n", "};\n"))
                {
                    foreach (var subType in subTypes)
                    {
                        var typeName = subType.GetTypeNameEx();
                        _writer.WriteLine("{{ \"{0}\", (Func<KitBase>)(() => new {1}()) }},", typeName, typeName);
                    }
                }
            }
        }
        
        private void _WriteFileHead ()
        {
            _writer.WriteLine("using System;");
            _writer.WriteLine("using System.Collections;");

            _writer.WriteLine();
            _writer.WriteLine("namespace Unicorn");
        }
        
        internal static IEnumerable<Type> CollectSubTypes(Type baseType)
        {
            foreach (var assembly in TypeTools.GetCustomAssemblies())
            {
                foreach (var type in assembly.GetExportedTypes())
                {
                    if (type.IsSubclassOf(baseType))
                    {
                        yield return type;
                    }
                }
            }
        }

        private CodeWriter _writer;
    }
}