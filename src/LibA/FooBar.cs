using LibB;
using LibC;

namespace LibA
{
    /// <summary>
    /// This class is not registered in container, but references types from other assemblies.
    /// </summary>
    public class FooBar : Foo<Bar>
    {
    }
}