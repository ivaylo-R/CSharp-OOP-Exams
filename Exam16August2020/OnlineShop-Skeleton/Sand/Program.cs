using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Sand
{
    class Program
    {
        static void Main(string[] args)
        {
            Type[] iLoadTypes = (from t in Assembly.GetCallingAssembly().GetExportedTypes()
                                 where !t.IsInterface && !t.IsAbstract
                                 where typeof(IComponent).IsAssignableFrom(t)
                                 select t).ToArray();

        }
    }
}
