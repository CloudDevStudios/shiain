using System.Reflection;

namespace Adrenak.Shiain.NaughtyAttributes.Editor
{
    public abstract class MethodDrawer
    {
        public abstract void DrawMethod(UnityEngine.Object target, MethodInfo methodInfo);
    }
}
