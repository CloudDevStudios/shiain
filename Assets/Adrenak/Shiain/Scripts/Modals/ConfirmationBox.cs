using System;
using UnityEngine;
using UnityEngine.UI;

namespace Adrenak.Shiain {
	[RequireComponent(typeof(UIElement))]
	public class ConfirmationBox : MonoBehaviour {
		static ConfirmationBox m_Instance;
		static Action<bool> m_Response;

		[SerializeField] UIElement m_State;
		[SerializeField] Text m_Title;
		[SerializeField] Text m_Body;
		[SerializeField] Text m_Affirmative;
		[SerializeField] Text m_Negative;

		void OnValidate() {
			m_Instance = FindObjectOfType<ConfirmationBox>();
		}

		void Start() {
			if (m_Instance == null)
				Debug.LogWarning("Singleton for ConfirmationBox not found. Exit the game and try activating then deactivating the gameobject.");
		}

		[ContextMenu("Show Sample")]
		public void ShowSample() {
			Show("Some title", "Some body", "Yes", "No", null);
		}

		[ContextMenu("Close")]
		public void Close() {
			m_State.Close();
		}

		public static void Show(string title, string body, string affirmative, string negative, Action<bool> response) {
			m_Response = response;
			m_Instance.m_Title.text = title;
			m_Instance.m_Body.text = body;
			m_Instance.m_Affirmative.text = affirmative;
			m_Instance.m_Negative.text = negative;

			m_Instance.m_State.Open();
		}

		public void Respond(bool response) {
			m_Response?.Invoke(response);
			m_Response = null;

			m_State.Close();
		}
	}
}
