using UnityEngine;
using UnityEngine.UI;

namespace Adrenak.Shiain {
	[RequireComponent(typeof(Toggle))]
	public class ToggleAnim : MonoBehaviour {
		Toggle toggleObject;

		[Header("ANIMATORS")]
		public Animator toggleAnimator;

		[Header("ANIM NAMES")]
		public string toggleOn;
		public string toggleOff;

		void Awake() {
			toggleObject = GetComponent<Toggle>();
			toggleObject.onValueChanged.AddListener(TaskOnClick);
		}

		void TaskOnClick(bool value) {
			if (toggleObject.isOn) 
				toggleAnimator.Play(toggleOn);
			else 
				toggleAnimator.Play(toggleOff);
		}
	}
}