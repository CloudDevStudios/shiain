using System;
using UnityEngine;
using UnityEngine.UI;
using Adrenak.Shiain.NaughtyAttributes;

namespace Adrenak.Shiain {
	[ExecuteInEditMode]
	[RequireComponent(typeof(UIElement))]
	public class AlertBox : MonoBehaviour {
		static Action m_Response;

		[SerializeField] UIElement m_State;
		[SerializeField] Text m_Title;
		[SerializeField] Text m_Body;
		[SerializeField] Text m_Affirmative;

		[Button("Show Sample")]
		public void ShowSample() {
			Show("Some title", "Some body", "OK", null);
		}

		[Button("Close")]
		public void Close() {
			m_State.Close();
		}

		public void Show(string title, string body, string affirmative, Action response) {
			m_Response = response;
			m_Title.text = title;
			m_Body.text = body;
			m_Affirmative.text = affirmative;

			m_State.Open();
		}

		public void Respond() {
			m_Response?.Invoke();
			m_Response = null;

			m_State.Close();
		}
	}
}
