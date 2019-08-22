using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using Adrenak.Shiain.UIShapesKit;

namespace Adrenak.Shiain {
	public class Tag : MonoBehaviour {
		public bool isSilent;

		public event Action OnSelected;
		public UnityEvent onSelected;

		public event Action OnDeselected;
		public UnityEvent onDeselected;

		[SerializeField] Text m_DisplayText;
		[SerializeField] Button m_Button;
		[SerializeField] Rectangle m_Fill;
		[SerializeField] Rectangle m_Outline;

		public object Data { get; private set; }
		public bool IsSelected;
		public string ID { get; private set; }
		public string Text {
			get { return m_DisplayText.text; }
			set { m_DisplayText.text = value; }
		}

		void Awake() {
			m_Button.onClick.AddListener((UnityAction)(() => {
				if (IsSelected)
					HandleDeselection();
				else
					HandleSelection();
			}));
		}

		public void Init(string id, string displayText, bool isSelected, object data) {
			ID = id;
			Text = displayText;
			Data = data;

			isSilent = true;
			if (isSelected)
				HandleSelection();
			else
				HandleDeselection();
			isSilent = true;
		}

		public void SetData(object data) {
			Data = data;
		}

		public void SetColor(Color color) {
			var fill = color;
			fill.a = .33f;
			m_Fill.ShapeProperties.FillColor = fill;
			m_Outline.ShapeProperties.OutlineColor = fill;
		}

		public void HandleDeselection() {
			IsSelected = false;
			m_Fill.gameObject.SetActive(false);
			if (!isSilent) {
				OnDeselected?.Invoke();
				onDeselected?.Invoke();
			}
		}

		public void HandleSelection() {
			IsSelected = true;
			m_Fill.gameObject.SetActive(true);
			if (!isSilent) {
				OnSelected?.Invoke();
				onSelected?.Invoke();
			}
		}
	}
}
