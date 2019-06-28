using UnityEditor;

namespace Adrenak.Shiain.NaughtyAttributes.Editor
{
    public abstract class PropertyValidator
    {
        public abstract void ValidateProperty(SerializedProperty property);
    }
}
