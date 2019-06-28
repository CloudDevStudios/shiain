using System;
using UnityEngine;
using UnityEngine.UI;
using Adrenak.Shiain.NaughtyAttributes;

namespace Adrenak.Shiain {
	[ExecuteInEditMode]
	[RequireComponent(typeof(UIElement))]
	public class ConfirmationBox : MonoBehaviour {
		static Action<bool> m_Response;

		[SerializeField] UIElement m_State;
		[SerializeField] Text m_Title;
		[SerializeField] Text m_Body;
		[SerializeField] Text m_Affirmative;
		[SerializeField] Text m_Negative;

		[Button("Show Sample")]
		public void ShowSample() {
			Show("Some title", "Some body", "Yes", "No", null);
		}

		[Button("Close")]
		public void Close() {
			m_State.Close();
		}

		public void Show(string title, string body, string affirmative, string negative, Action<bool> response) {
			m_Response = response;
			m_Title.text = title;
			m_Body.text = body;
			m_Affirmative.text = affirmative;
			m_Negative.text = negative;

			m_State.Open();
		}

		public void Respond(bool response) {
			m_Response?.Invoke(response);
			m_Response = null;

			m_State.Close();
		}
	}
}
