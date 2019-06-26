using UnityEngine;

// TODO: Check when values are driven by layout groups
namespace Adrenak.Shiain {
	[ExecuteInEditMode]
	[RequireComponent(typeof(CanvasGroup))]
	public class ScaleTransition : Transition {
		public float rate = 8f;
		
		[SerializeField] Vector2 m_MinSize;
		[SerializeField] Vector2 m_MaxSize;
		
		Vector2 minCenter;
		Vector2 maxCenter;

		Vector2 minMin;
		Vector2 minMax;
		Vector2 maxMin;
		Vector2 maxMax;

		CanvasGroup m_Group;
		CanvasGroup Group {
			get {
				if (m_Group == null) m_Group = GetComponent<CanvasGroup>();
				return m_Group;
			}
		}

		RectTransform m_RT;
		RectTransform RT {
			get {
				if (m_RT == null) m_RT = GetComponent<RectTransform>();
				return m_RT;
			}
		}

		void OnValidate() {
			if (m_MaxSize != Vector2.zero) return;

			state = State.Up;
			m_MaxSize = new Vector2 {
				x = RT.offsetMax.x - RT.offsetMin.x,
				y = RT.offsetMax.y - RT.offsetMin.y
			};
		}

		private void Awake() {
			onStartTransitionUp.AddListener(() => {
				Group.blocksRaycasts = false;
				Group.interactable = false;
			});

			onEndTransitionUp.AddListener(() => {
				Group.blocksRaycasts = true;
				Group.interactable = true;
			});

			onStartTransitionDown.AddListener(() => {
				Group.blocksRaycasts = false;
				Group.interactable = false;
			});

			onEndTransitionDown.AddListener(() => {
				Group.blocksRaycasts = true;
				Group.interactable = true;
			});
		}

		protected override bool TransitionUpOverTime() {
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

			if (((RT.offsetMin.x - maxMin.x) < 2 && (RT.offsetMax.x - maxMax.x) < 2) &&
				((RT.offsetMin.y - maxMin.y) < 2 && (RT.offsetMax.y - maxMax.y) < 2)
			) {
				RT.offsetMin = maxMin;
				RT.offsetMax = maxMax;
				gameObject.SetActive(false);
				gameObject.SetActive(true);
		
				return true;
			}
			return false;
		}

		protected override bool TransitionDownOverTime() {
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

			if (((RT.offsetMin.x - minMin.x) < 2 && (RT.offsetMax.x - minMax.x) < 2) &&
				((RT.offsetMin.y - minMin.y) < 2 && (RT.offsetMax.y - minMax.y) < 2)
			) {
				RT.offsetMin = minMin;
				RT.offsetMax = minMax;
				gameObject.SetActive(false);
				gameObject.SetActive(true);

				return true;
			}
			return false;
		}
	}
}