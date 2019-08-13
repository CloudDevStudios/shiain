using UnityEngine;
using UnityEngine.UI;
using System;
using Adrenak.Shiain.UIShapesKit;

namespace Adrenak.Shiain {
	public class Tag : MonoBehaviour {
		public bool isSilent;
		public event Action OnSelected;
		public event Action OnDeselected;

		[SerializeField] Text m_DisplayText;
		[SerializeField] Button m_Button;
		[SerializeField] Rectangle m_Fill;
		[SerializeField] Rectangle m_Outline;

		public bool m_IsSelected;

		void Awake() {
			m_Button.onClick.AddListener(() => {
				if (m_IsSelected)
					HandleDeselection();
				else
					HandleSelection();
			});
		}

		public void Init(string displayText, bool isSelected = false) {
			m_DisplayText.text = displayText;
			if (isSelected)
				HandleSelection();
			else
				HandleDeselection();

			gameObject.SetActive(false);
			gameObject.SetActive(true);
		}

		public void SetColor(Color color) {
			var fill = color;
			fill.a = .33f;
			m_Fill.ShapeProperties.FillColor = fill;
			m_Outline.ShapeProperties.OutlineColor = fill;
		}

		public void HandleDeselection() {
			m_IsSelected = false;
			m_Fill.enabled = false;
			if (!isSilent)
				OnDeselected?.Invoke();
		}

		public void HandleSelection() {
			m_IsSelected = true;
			m_Fill.enabled = true;
			if (!isSilent)
				OnSelected?.Invoke();
		}

		public bool IsSelected() {
			return m_IsSelected;
		}
	}
}
