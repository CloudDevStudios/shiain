﻿using UnityEngine;
using UnityEngine.UI;

namespace Adrenak.Shiain {
	[AddComponentMenu("UI/Effects/Text Gradient")]
	public class TextGradient : BaseMeshEffect {
		public Color m_color1 = Color.white;
		public Color m_color2 = Color.white;
		[Range(-180f, 180f)] public float m_angle = 0f;

		public override void ModifyMesh(VertexHelper vh) {
			if (enabled) {
				Rect rect = graphic.rectTransform.rect;
				Vector2 dir = GradientUtils.RotationDir(m_angle);
				GradientUtils.Matrix2x3 localPositionMatrix = GradientUtils.LocalPositionMatrix(new Rect(0f, 0f, 1f, 1f), dir);

				UIVertex vertex = default(UIVertex);
				for (int i = 0; i < vh.currentVertCount; i++) {

					vh.PopulateUIVertex(ref vertex, i);
					Vector2 position = GradientUtils.VerticePositions[i % 4];
					Vector2 localPosition = localPositionMatrix * position;
					vertex.color *= Color.Lerp(m_color2, m_color1, localPosition.y);
					vh.SetUIVertex(vertex, i);
				}
			}
		}
	}
}
