using UnityEngine;

namespace Adrenak.Shiain {
	[RequireComponent(typeof(CanvasGroup))]
	public class OpacityTransition : Transition {
		public float rate = 8;	
		CanvasGroup m_Group;
		CanvasGroup Group {
			get {
				if (m_Group == null)
					m_Group = GetComponent<CanvasGroup>();
				return m_Group;
			}
		}

		void OnValidate() {
			onStartTransitionUp.RemoveListener(HandleOnStartTransitionUp);
			onStartTransitionUp.AddListener(HandleOnStartTransitionUp);

			onStartTransitionDown.RemoveListener(HandleOnStartTransitionDown);
			onStartTransitionDown.AddListener(HandleOnStartTransitionDown);
		}

		void HandleOnStartTransitionUp() {
			Group.blocksRaycasts = true;
			Group.interactable = true;
		}

		void HandleOnStartTransitionDown() {
			Group.blocksRaycasts = false;
			Group.interactable = false;
		}

		protected override bool TransitionUpOverTime() {
			Group.alpha = Mathf.Lerp(Group.alpha, 1, Time.deltaTime * rate);

			if (Group.alpha > .99f) {
				Group.alpha = 1;
				return true;
			}
			return false;
		}

		protected override bool TransitionDownOverTime() {
			Group.alpha = Mathf.Lerp(Group.alpha, 0, Time.deltaTime * rate);

			if (Group.alpha < .01f) {
				Group.alpha = 0;
				return true;
			}
			return false;
		}
	}
}
