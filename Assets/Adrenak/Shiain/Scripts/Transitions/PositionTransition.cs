using UnityEngine;

namespace Adrenak.Shiain {
	public class PositionTransition : Transition {
		[SerializeField] AnimationCurve m_InCurve = AnimationCurve.Linear(0, 0, 1, 1);
		[SerializeField] float m_InDuration = .5f;
		[SerializeField] Vector3 m_InPosition;

		[SerializeField] AnimationCurve m_OutCurve = AnimationCurve.Linear(0, 0, 1, 1);
		[SerializeField] float m_OutDuration = .5f;
		[SerializeField] Vector3 m_OutPosition;

		[SerializeField] bool m_StartOffScreen;
		[SerializeField] bool m_AnimateInOnStart;

		RectTransform m_RT;
		RectTransform RT {
			get {
				if (m_RT == null)
					m_RT = GetComponent<RectTransform>();
				return m_RT;
			}
		}

		public Vector3 m_StartPos;
		public float m_StartTime;
		public float m_EndTime;

		private void OnValidate() {
			Start();
		}

		void Start() {
			if (m_StartOffScreen)
				RT.localPosition = m_OutPosition;

			if (m_AnimateInOnStart) {
				RT.localPosition = m_OutPosition;
				TransitionUp();
			}

			onStartTransitionUp.AddListener(() => {
				m_StartTime = Time.unscaledTime;
				m_EndTime = Time.unscaledTime + m_InDuration;
				m_StartPos = RT.localPosition;
			});

			onStartTransitionDown.AddListener(() => {
				m_StartTime = Time.unscaledTime;
				m_EndTime = Time.unscaledTime + m_OutDuration;
				m_StartPos = RT.localPosition;
			});
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.A))
				TransitionUp();
		}

		protected override bool TransitionUpOverTime() {
			if (m_EndTime >= Time.unscaledTime) {
				// Get the appropriate value from the AnimationCurve using a normalised value and lerp
				float curveValue = m_InCurve.Evaluate((Time.unscaledTime - m_StartTime) / (m_EndTime - m_StartTime));
				RT.localPosition = Vector3.LerpUnclamped(m_StartPos, m_InPosition, curveValue);
				return false;
			}
			else {
				RT.localPosition = m_InPosition;
				return true;
			}
		}

		protected override bool TransitionDownOverTime() {
			if (m_EndTime >= Time.unscaledTime) {
				// Get the appropriate value from the AnimationCurve using a normalised value and lerp
				float curveValue = m_InCurve.Evaluate((Time.unscaledTime - m_StartTime) / (m_EndTime - m_StartTime));
				RT.localPosition = Vector3.LerpUnclamped(m_StartPos, m_OutPosition, curveValue);
				return false;
			}
			else {
				RT.localPosition = m_OutPosition;
				return true;
			}
		}
	}
}
