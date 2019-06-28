using System.Reflection;

namespace Adrenak.Shiain.NaughtyAttributes.Editor
{
    public abstract class FieldDrawer
    {
        public abstract void DrawField(UnityEngine.Object target, FieldInfo field);
    }
}
