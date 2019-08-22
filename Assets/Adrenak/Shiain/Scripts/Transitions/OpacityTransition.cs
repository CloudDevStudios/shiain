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

		protected override bool TransitionUpOverTime() {
			Group.alpha = Mathf.Lerp(Group.alpha, 1, Time.deltaTime * rate);

			if (Group.alpha > .9f) {
				Group.alpha = 1;
				Group.blocksRaycasts = true;
				Group.interactable = true;
				return true;
			}
			return false;
		}

		protected override bool TransitionDownOverTime() {
			Group.alpha = Mathf.Lerp(Group.alpha, 0, Time.deltaTime * rate);

			if (Group.alpha < .1f) {
				Group.alpha = 0;
				Group.blocksRaycasts = false;
				Group.interactable = false;
				return true;
			}
			return false;
		}
	}
}
