using UnityEngine;
using UnityEngine.UI;

namespace Adrenak.Shiain {
	[AddComponentMenu("UI/Effects/Text Corners Gradient")]
	public class TextCornersGradient : BaseMeshEffect {
		public Color m_topLeftColor = Color.white;
		public Color m_topRightColor = Color.white;
		public Color m_bottomRightColor = Color.white;
		public Color m_bottomLeftColor = Color.white;

		public override void ModifyMesh(VertexHelper vh) {
			if (enabled) {
				Rect rect = graphic.rectTransform.rect;

				UIVertex vertex = default(UIVertex);
				for (int i = 0; i < vh.currentVertCount; i++) {
					vh.PopulateUIVertex(ref vertex, i);
					Vector2 normalizedPosition = GradientUtils.VerticePositions[i % 4];
					vertex.color *= GradientUtils.Bilerp(m_bottomLeftColor, m_bottomRightColor, m_topLeftColor, m_topRightColor, normalizedPosition);
					vh.SetUIVertex(vertex, i);
				}
			}
		}
	}
}
