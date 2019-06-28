using System;

namespace Adrenak.Shiain.NaughtyAttributes.Editor
{
    public class PropertyValidatorAttribute : BaseAttribute
    {
        public PropertyValidatorAttribute(Type targetAttributeType) : base(targetAttributeType)
        {
        }
    }
}
