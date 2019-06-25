using UnityEngine;

namespace Adrenak.Shiain {
	public class TabMenu : MonoBehaviour {
		[SerializeField] bool m_StartClosed;
		[SerializeField] bool m_ToggleRepeatClicks;
		[SerializeField] UIElement[] m_Elements;

		void Start() {
			if (m_StartClosed)
				CloseAllElements();
		}

		public void OpenElement(int index) {
			if(index < 0 || index >= m_Elements.Length) return;

			var e = m_Elements[index];
			if (e == null) return;

			for(int i = 0; i < m_Elements.Length; i++) {
				if(i != index)
					m_Elements[i].Close();
			}

			if (m_ToggleRepeatClicks) {
				if (m_Elements[index].IsOpen)
					m_Elements[index].Close();
				else
					m_Elements[index].Open();
			}
			else {
				m_Elements[index].Open();
			}
		}

		public void CloseAllElements() {
			for (int i = 0; i < m_Elements.Length; i++) {
				if (m_Elements[i] != null)
					m_Elements[i].Close();
			}
		}
	}
}
