using UnityEngine;
using UnityEngine.Events;

namespace Adrenak.Shiain {
	[ExecuteInEditMode]
	public abstract class Transition : MonoBehaviour {
		public enum State {
			Up,
			Down
		}

		public State state = State.Down;
		public bool IsTransitioning { get; private set; }
		public UnityEvent onStartTransitionUp;
		public UnityEvent onEndTransitionUp;
		public UnityEvent onStartTransitionDown;
		public UnityEvent onEndTransitionDown;

		[ContextMenu("Toggle Transition")]
		public void Toggle() {
			Debug.Log("Toggle");
			if (state == State.Up)
				TransitionDown();
			else
				TransitionUp();
		}

		[ContextMenu("Transition Up")]
		public void TransitionUp() {
			state = State.Up;
			IsTransitioning = true;
			onStartTransitionUp.Invoke();
		}

		[ContextMenu("Transition Down")]
		public void TransitionDown() {
			state = State.Down;
			IsTransitioning = true;
			onStartTransitionDown.Invoke();
		}

		void LateUpdate() {
			if (state == State.Up && IsTransitioning) {
				if (TransitionUpOverTime()) {
					onEndTransitionUp.Invoke();
					IsTransitioning = false;
				}
			}
			else if (state == State.Down && IsTransitioning) {
				if (TransitionDownOverTime()) {
					onEndTransitionDown.Invoke();
					IsTransitioning = false;
				}
			}
		}

		protected abstract bool TransitionUpOverTime();
		protected abstract bool TransitionDownOverTime();
	}
}
