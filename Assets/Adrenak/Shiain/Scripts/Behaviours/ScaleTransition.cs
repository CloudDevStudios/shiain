using UnityEngine;
using UnityEngine.Events;

namespace Adrenak.Shiain {
	[ExecuteInEditMode]
	[RequireComponent(typeof(CanvasGroup))]
	public class ScaleTransition : MonoBehaviour {
		public enum State {
			Expanding,
			Shrinking
		}

		public float rate = 8f;
		public State state = State.Shrinking;

		[SerializeField] Vector2 m_MinSize;
		[SerializeField] Vector2 m_MaxSize;
		public UnityEvent onExpanded;
		public UnityEvent onShrunk;

		Vector2 minCenter;
		Vector2 maxCenter;

		Vector2 minMin;
		Vector2 minMax;
		Vector2 maxMin;
		Vector2 maxMax;

		RectTransform m_RT;
		RectTransform RT {
			get {
				if (m_RT == null) m_RT = GetComponent<RectTransform>();
				return m_RT;
			}
		}

		public void Toggle() {
			if (state == State.Expanding)
				state = State.Shrinking;
			else
				state = State.Expanding;
		}

		public void Expand() {
			state = State.Expanding;
		}

		public void Shrink() {
			state = State.Shrinking;
		}

		void OnValidate() {
			if (m_MaxSize != Vector2.zero) return;

			state = State.Expanding;
			m_MaxSize = new Vector2 {
				x = RT.offsetMax.x - RT.offsetMin.x,
				y = RT.offsetMax.y - RT.offsetMin.y
			};
		}

		void Update() {
			if (state == State.Expanding)
				ExpandOverTime();
			else if (state == State.Shrinking)
				ShrinkOverTime();
		}

		void ExpandOverTime() {
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

				onExpanded?.Invoke();
			}
		}

		void ShrinkOverTime() {
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

				onShrunk?.Invoke();
			}
		}
	}
}