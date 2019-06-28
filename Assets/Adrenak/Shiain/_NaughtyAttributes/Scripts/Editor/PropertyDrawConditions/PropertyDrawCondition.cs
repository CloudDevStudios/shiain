using UnityEditor;

namespace Adrenak.Shiain.NaughtyAttributes.Editor
{
    public abstract class PropertyDrawCondition
    {
        public abstract bool CanDrawProperty(SerializedProperty property);
    }
}
