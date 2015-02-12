using System;

namespace LibA
{
    public class ComponentRegistrationAttribute : Attribute
    {
        public ComponentRegistrationAttribute(string scope)
        {
            Scope = scope;
        }

        public string Scope { get; set; }
    }
}