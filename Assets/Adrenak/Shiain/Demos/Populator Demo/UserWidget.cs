using UnityEngine.UI;
using UnityEngine;

namespace Adrenak.Shiain.Demos.Population {
	public class UserWidget : PopulationWidget {
		[SerializeField] Text m_NameText;
		[SerializeField] Text m_AgeText;

		public override void Init(object data) {
			var model = (User)data;
			m_NameText.text = "Name: " + model.name;
			m_AgeText.text = "Age: " + model.age;
		}

		public override void CleanUp() {
			m_NameText.text = m_AgeText.text = string.Empty;
		}
	}
}