using UnityEngine;

namespace Adrenak.Shiain {
	public class Expandable : MonoBehaviour {
		public enum State {
			Expanding,
			Shrinking
		}

		public float rate = 8f;
		public State state = State.Shrinking;

		[SerializeField] Vector2 m_MinSize;
		[SerializeField] Vector2 m_MaxSize;

		Vector2 minCenter;
		Vector2 maxCenter;

		Vector2 minMin;
		Vector2 minMax;
		Vector2 maxMin;
		Vector2 maxMax;

		RectTransform m_RT;
		RectTransform RT {
			get {
				if (m_RT == null)
					m_RT = GetComponent<RectTransform>();
				return m_RT;
			}
		}

		public void Toggle() {
			if (state == State.Expanding)
				state = State.Shrinking;
			else
				state = State.Expanding;
		}

		void OnValidate() {
			var minCenter = maxCenter = new Vector2 {
				x = (RT.offsetMin.x + RT.offsetMax.x) / 2,
				y = (RT.offsetMin.y + RT.offsetMax.y) / 2,
			};

			var minMin = new Vector2(minCenter.x - m_MinSize.x * 0.5f, minCenter.y - m_MinSize.y * 0.5f);
			var minMax = new Vector2(minCenter.x + m_MinSize.x * 0.5f, minCenter.y + m_MinSize.y * 0.5f);

			var maxMin = new Vector2(maxCenter.x - m_MaxSize.x * 0.5f, maxCenter.y - m_MaxSize.y * 0.5f);
			var maxMax = new Vector2(maxCenter.x + m_MaxSize.x * 0.5f, maxCenter.y + m_MaxSize.y * 0.5f);

			if (state == State.Expanding) {
				RT.offsetMin = maxMin;
				RT.offsetMax = maxMax;
			}
			else if(state == State.Shrinking) {
				RT.offsetMin = minMin;
				RT.offsetMax = minMax;
			}
		}

		void Update() {
			if (state == State.Expanding)
				InterpolateExpantion();
			else if (state == State.Shrinking)
				InterpolateShrinking();
		}

		void InterpolateExpantion() {
			var maxCenter = new Vector2 {
				x = (RT.offsetMin.x + RT.offsetMax.x) / 2,
				y = (RT.offsetMin.y + RT.offsetMax.y) / 2,
			};
			var maxMin = new Vector2(maxCenter.x - m_MaxSize.x * 0.5f, maxCenter.y - m_MaxSize.y * 0.5f);
			var maxMax = new Vector2(maxCenter.x + m_MaxSize.x * 0.5f, maxCenter.y + m_MaxSize.y * 0.5f);

			RT.offsetMin = Vector2.Lerp(RT.offsetMin, maxMin, Time.deltaTime * rate);
			RT.offsetMax = Vector2.Lerp(RT.offsetMax, maxMax, Time.deltaTime * rate);
			gameObject.SetActive(false);
			gameObject.SetActive(true);

			if ((RT.offsetMin - maxMin).magnitude < maxMin.magnitude * .005f && (RT.offsetMax - maxMax).magnitude < maxMax.magnitude * .005f) {
				RT.offsetMin = maxMin;
				RT.offsetMax = maxMax;
				gameObject.SetActive(false);
				gameObject.SetActive(true);
			}
		}

		void InterpolateShrinking() {
			var minCenter = new Vector2 {
				x = (RT.offsetMin.x + RT.offsetMax.x) / 2,
				y = (RT.offsetMin.y + RT.offsetMax.y) / 2,
			};
			var minMin = new Vector2(minCenter.x - m_MinSize.x * 0.5f, minCenter.y - m_MinSize.y * 0.5f);
			var minMax = new Vector2(minCenter.x + m_MinSize.x * 0.5f, minCenter.y + m_MinSize.y * 0.5f);

			RT.offsetMin = Vector2.Lerp(RT.offsetMin, minMin, Time.deltaTime * rate);
			RT.offsetMax = Vector2.Lerp(RT.offsetMax, minMax, Time.deltaTime * rate);
			gameObject.SetActive(false);
			gameObject.SetActive(true);

			if ((RT.offsetMin - minMin).magnitude < minMin.magnitude * .005f && (RT.offsetMax - minMax).magnitude < minMax.magnitude * .005f) {
				RT.offsetMin = minMin;
				RT.offsetMax = minMax;
				gameObject.SetActive(false);
				gameObject.SetActive(true);
			}
		}
	}
}