using System;

namespace Adrenak.Shiain.NaughtyAttributes
{
    /// <summary>
    /// Make tags appear as tag popup fields 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class TagAttribute : DrawerAttribute
    {
    }
}
