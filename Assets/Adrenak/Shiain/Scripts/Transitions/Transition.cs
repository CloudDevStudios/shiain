using UnityEngine;
using UnityEngine.Events;
using Adrenak.Shiain.NaughtyAttributes;

namespace Adrenak.Shiain {
	[ExecuteInEditMode]
	public abstract class Transition : MonoBehaviour {
		public enum State {
			Up,
			Down
		}

		[ReadOnly] [SerializeField] State m_State = State.Down;
		[ReadOnly] [SerializeField] bool m_IsTransitioning;
		public UnityEvent onStartTransitionUp;
		public UnityEvent onEndTransitionUp;
		public UnityEvent onStartTransitionDown;
		public UnityEvent onEndTransitionDown;

		[Button("Toggle Transition")]
		public void Toggle() {
			if (m_State == State.Up)
				TransitionDown();
			else
				TransitionUp();
#if UNITY_EDITOR
			gameObject.SetActive(false);
			gameObject.SetActive(true);
#endif
		}

		[Button("Transition Up")]
		public void TransitionUp() {
			m_State = State.Up;
			m_IsTransitioning = true;
			onStartTransitionUp.Invoke();
#if UNITY_EDITOR
			gameObject.SetActive(false);
			gameObject.SetActive(true);
#endif
		}

		[Button("Transition Down")]
		public void TransitionDown() {
			m_State = State.Down;
			m_IsTransitioning = true;
			onStartTransitionDown.Invoke();
#if UNITY_EDITOR
			gameObject.SetActive(false);
			gameObject.SetActive(true);
#endif
		}

		protected void SetState(State state) {
			m_State = state;
		}

		void LateUpdate() {
			if (m_State == State.Up && m_IsTransitioning) {
				if (TransitionUpOverTime()) {
					onEndTransitionUp.Invoke();
					m_IsTransitioning = false;
				}
			}
			else if (m_State == State.Down && m_IsTransitioning) {
				if (TransitionDownOverTime()) {
					onEndTransitionDown.Invoke();
					m_IsTransitioning = false;
				}
			}
		}

		protected abstract bool TransitionUpOverTime();
		protected abstract bool TransitionDownOverTime();		
	}
}
