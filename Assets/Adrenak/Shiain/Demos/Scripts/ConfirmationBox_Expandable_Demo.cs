using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Adrenak.Shiain.Demos {
	public class ConfirmationBox_Expandable_Demo : MonoBehaviour {
		public void Show() {
			ConfirmationBox.Show("Some nice title", "Some body text that's descriptive but not too long", "Yay!", "Nay...", response => {
				Debug.Log(response);
			});
		}
	}
}
