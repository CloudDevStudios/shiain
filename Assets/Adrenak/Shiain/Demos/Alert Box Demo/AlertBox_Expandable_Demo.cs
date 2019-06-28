using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Adrenak.Shiain.Demos {
	public class AlertBox_Expandable_Demo : MonoBehaviour {
		public AlertBox box;

		public void Show() {
			box.Show("Some nice title", "Some body text that's descriptive but not too long", "Cool!", null);
		}
	}
}
