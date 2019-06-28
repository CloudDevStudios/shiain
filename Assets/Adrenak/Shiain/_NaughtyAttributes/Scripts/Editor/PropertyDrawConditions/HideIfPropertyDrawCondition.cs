using UnityEditor;

namespace Adrenak.Shiain.NaughtyAttributes.Editor
{
    [PropertyDrawCondition(typeof(HideIfAttribute))]
    public class HideIfPropertyDrawCondition : ShowIfPropertyDrawCondition
    {
    }
}
