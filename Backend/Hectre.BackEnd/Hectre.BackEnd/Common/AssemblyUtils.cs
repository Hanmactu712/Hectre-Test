using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hectre.BackEnd.Common
{
    /// <summary>
    /// util class contains some methods to get Type from provided assembly
    /// </summary>
    public static class AssemblyUtils
    {
        public static Type[] GetTypesInNameSpace(Assembly assembly, string nameSpace)
        {
            if (assembly == null) return null;

            return
                !string.IsNullOrEmpty(nameSpace) ?
                assembly.GetTypes()
                    .Where(t => t.Namespace != null && t.Namespace.Contains(nameSpace, StringComparison.Ordinal))
                    .ToArray()
                : assembly.GetTypes()
                    .ToArray();
        }

        public static Type[] GetTypesDerivedFromType(Assembly assembly, Type type, string nameSpace = "")
        {
            if (assembly == null || type == null) return null;

            return
                !string.IsNullOrEmpty(nameSpace)
                    ? assembly.GetTypes()
                        .Where(t => t.Namespace != null && t.Namespace.Contains(nameSpace, StringComparison.Ordinal))
                        .Where(type.IsAssignableFrom)
                        .ToArray()
                    : assembly.GetTypes()
                        .Where(type.IsAssignableFrom)
                        .ToArray();
        }

        public static Type[] GetTypesFromAssemblyPath(string assemblyPath, string nameSpace)
        {
            if (!File.Exists(assemblyPath)) return new Type[] { };

            Assembly asm = Assembly.LoadFrom(assemblyPath);

            return GetTypesInNameSpace(asm, nameSpace);
        }

        public static Type[] GetTypesDerivedFromType(string assemblyPath, Type type)
        {
            if (!File.Exists(assemblyPath)) return new Type[] { };

            Assembly asm = Assembly.LoadFrom(assemblyPath);

            return GetTypesDerivedFromType(asm, type);
        }
    }
}
