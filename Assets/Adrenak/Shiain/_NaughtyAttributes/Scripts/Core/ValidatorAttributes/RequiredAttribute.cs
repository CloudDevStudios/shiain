using System;

namespace Adrenak.Shiain.NaughtyAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RequiredAttribute : ValidatorAttribute
    {
        public string Message { get; private set; }

        public RequiredAttribute(string message = null)
        {
            this.Message = message;
        }
    }
}
