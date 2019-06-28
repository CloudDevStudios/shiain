using System;

namespace Adrenak.Shiain.NaughtyAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ReadOnlyAttribute : DrawerAttribute
    {
    }
}
