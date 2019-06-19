using UnityEngine;
using UnityEngine.Events;

namespace Adrenak.Shiain {
	[ExecuteInEditMode]
	[RequireComponent(typeof(CanvasGroup))]
	public class OpacityTransition : MonoBehaviour {
		public enum State {
			Appearing,
			Disappearing
		}

		public float m_Rate = 8;
		public State state = State.Appearing;
		public UnityEvent onAppeared;
		public UnityEvent onDisappeared;

		CanvasGroup m_Group;

		void Awake() {
			m_Group = GetComponent<CanvasGroup>();
		}

		void Update() {
			if (state == State.Appearing)
				AppearOverTime();
			else if (state == State.Disappearing)
				DisappearOverTime();
		}

		void AppearOverTime() {
			m_Group.alpha = Mathf.Lerp(m_Group.alpha, 1, Time.deltaTime * m_Rate);
			
			if (m_Group.alpha > .99f) {
				m_Group.alpha = 1;
				onAppeared?.Invoke();
			}
		}

		void DisappearOverTime() {
			m_Group.alpha = Mathf.Lerp(m_Group.alpha, 0, Time.deltaTime * m_Rate);

			if (m_Group.alpha < .01f) {
				m_Group.alpha = 0;
				onDisappeared?.Invoke();
			}
		}

		public void Appear() {
			state = State.Appearing;
			m_Group.blocksRaycasts = true;
			m_Group.interactable = true;
		}

		public void Disappear() {
			state = State.Disappearing;
			m_Group.blocksRaycasts = false;
			m_Group.interactable = false;
		}
	}
}
