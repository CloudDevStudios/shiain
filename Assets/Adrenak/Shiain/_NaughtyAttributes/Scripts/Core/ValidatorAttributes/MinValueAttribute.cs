using System;

namespace Adrenak.Shiain.NaughtyAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class MinValueAttribute : ValidatorAttribute
    {
        public float MinValue { get; private set; }

        public MinValueAttribute(float minValue)
        {
            this.MinValue = minValue;
        }

        public MinValueAttribute(int minValue)
        {
            this.MinValue = minValue;
        }
    }
}
