using UnityEditor;

namespace Adrenak.Shiain.NaughtyAttributes.Editor
{
    public abstract class PropertyMeta
    {
        public abstract void ApplyPropertyMeta(SerializedProperty property, MetaAttribute metaAttribute);
    }
}
