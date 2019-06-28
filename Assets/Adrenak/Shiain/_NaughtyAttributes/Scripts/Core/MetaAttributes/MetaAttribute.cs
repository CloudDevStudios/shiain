using System;

namespace Adrenak.Shiain.NaughtyAttributes
{
    public abstract class MetaAttribute : NaughtyAttribute
    {
        public int Order { get; set; }
    }
}
