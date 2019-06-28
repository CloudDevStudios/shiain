using System;

namespace Adrenak.Shiain.NaughtyAttributes.Editor
{
    public interface IAttribute
    {
        Type TargetAttributeType { get; }
    }
}
