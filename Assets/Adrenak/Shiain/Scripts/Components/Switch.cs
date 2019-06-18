using System;
using UnityEngine;
using UnityEngine.UI;

namespace Adrenak.Shiain {
	public class Switch : MonoBehaviour {
		public Action<bool> OnSwitch;

		[SerializeField] Button m_OnButton;
		[SerializeField] Button m_OffButton;

		private void Awake() {
			m_OffButton.onClick.AddListener(() => {
				if (OnSwitch != null) OnSwitch.Invoke(false);
			});
			m_OnButton.onClick.AddListener(() => {
				if (OnSwitch != null) OnSwitch.Invoke(true);
			});
		}
	}
}
