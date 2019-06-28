using System;

namespace Adrenak.Shiain.NaughtyAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class BoxGroupAttribute : GroupAttribute
    {
        public BoxGroupAttribute(string name = "")
            : base(name)
        {
        }
    }
}
