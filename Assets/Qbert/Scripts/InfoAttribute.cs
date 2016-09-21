using System;

namespace Scripts
{
    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class InfoAttribute : Attribute
    {
        public string textInfo;
        public InfoAttribute(string text)
        {
            this.textInfo = text;
        }
    }
}
