﻿using System.Reflection;

namespace Adrenak.Shiain.NaughtyAttributes.Editor
{
    public abstract class NativePropertyDrawer
    {
        public abstract void DrawNativeProperty(UnityEngine.Object target, PropertyInfo property);
    }
}
