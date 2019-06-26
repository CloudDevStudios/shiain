using UnityEngine;

namespace Adrenak.Shiain {
	[ExecuteInEditMode]
	[RequireComponent(typeof(CanvasGroup))]
	public class OpacityTransition : Transition {
		public float rate = 8;	
		CanvasGroup m_Group;

		void Start() {
			m_Group = GetComponent<CanvasGroup>();
			onStartTransitionUp.AddListener(() => {
				m_Group.blocksRaycasts = true;
				m_Group.interactable = true;
			});
			onStartTransitionDown.AddListener(() => {
				m_Group.blocksRaycasts = false;
				m_Group.interactable = false;
			});
		}

		protected override bool TransitionUpOverTime() {
			m_Group.alpha = Mathf.Lerp(m_Group.alpha, 1, Time.deltaTime * rate);

			if (m_Group.alpha > .99f) {
				m_Group.alpha = 1;
				return true;
			}
			return false;
		}

		protected override bool TransitionDownOverTime() {
			m_Group.alpha = Mathf.Lerp(m_Group.alpha, 0, Time.deltaTime * rate);

			if (m_Group.alpha < .01f) {
				m_Group.alpha = 0;
				return true;
			}
			return false;
		}
	}
}
