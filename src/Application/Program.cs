using System;
using System.Linq;
using System.Reflection;
using LibA;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintDomainAssemblies();

            Console.WriteLine("Press 1 to use reflection, press 2 to use T4 or press any other key to exit...");

            var keyInfo = Console.ReadKey();
            Console.CursorLeft = 0;

            switch (keyInfo.KeyChar)
            {
                case '1':
                    UseReflection();
                    break;
                case '2':
                    UseT4();
                    break;
            }

            PrintDomainAssemblies();
        }

        static IContainer UseReflection()
        {
            var types = typeof(IMagicWand).Assembly.GetTypes()
                .Where(type => type.GetCustomAttribute<ComponentRegistrationAttribute>() != null)
                .ToList();

            var registry = new Registry();
            foreach (var type in types)
            {
                registry.For(type).Singleton().Use(type);
            }

            return new Container(registry);
        }

        static IContainer UseT4()
        {
            return new Container(new LibA.ComponentRegistry());
        }

        static void PrintDomainAssemblies()
        {
            Console.WriteLine("List of loaded assemblies:");

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.GlobalAssemblyCache)
                    continue;

                Console.WriteLine(assembly.GetName().Name);
            }

            Console.WriteLine();
        }
    }
}
