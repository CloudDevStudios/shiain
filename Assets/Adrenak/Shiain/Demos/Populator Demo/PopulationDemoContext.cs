using UnityEngine;

namespace Adrenak.Shiain.Demos.Population {
	public class PopulationDemoContext : MonoBehaviour {
		[SerializeField] User[] m_Users;
		[SerializeField] UserWidget m_WidgetPrefab;
		[SerializeField] SimplePopulator m_Populator;

		[ContextMenu("Populate")]
		public void Populate() {
			m_Populator.SetWidgetPrefab(m_WidgetPrefab);
			m_Populator.Populate(m_Users, 6);
		}

		[ContextMenu("Next")]
		public void Next() {
			m_Populator.Next();
		}

		[ContextMenu("Previous")]
		public void Previous() {
			m_Populator.Previous();
		}
	}
}
