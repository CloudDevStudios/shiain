﻿#if !UNITY_2018_3_OR_NEWER
using UnityEngine;
using UnityEditor;

namespace Adrenak.Shiain.UIShapesKit {
	[CustomPropertyDrawer(typeof(MinAttribute))]
	public class MinDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			MinAttribute attribute = (MinAttribute)base.attribute;

			switch (property.propertyType) {
				case SerializedPropertyType.Integer:
					int valueI = EditorGUI.IntField(position, label, property.intValue);
					property.intValue = Mathf.Max(valueI, attribute.minInt);
					break;
				case SerializedPropertyType.Float:
					float valueF = EditorGUI.FloatField(position, label, property.floatValue);
					property.floatValue = Mathf.Max(valueF, attribute.minFloat);
					break;
			}
		}
	}
}
#endif