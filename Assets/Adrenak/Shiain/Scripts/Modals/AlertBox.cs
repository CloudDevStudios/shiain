using System;
using UnityEngine;
using UnityEngine.UI;

// TODO: Do away with singleton
// Allow invoking using references and (maybe) also
// introduce a reference dictionary to allow
// for easy invokation
namespace Adrenak.Shiain {
	[RequireComponent(typeof(UIElement))]
	public class AlertBox : MonoBehaviour {
		static AlertBox m_Instance;
		static Action m_Response;

		[SerializeField] UIElement m_State;
		[SerializeField] Text m_Title;
		[SerializeField] Text m_Body;
		[SerializeField] Text m_Affirmative;

		void OnValidate() {
			m_Instance = FindObjectOfType<AlertBox>();
		}

		void Start() {
			if (m_Instance == null)
				Debug.LogWarning("Singleton for AlertBox not found. Exit the game and try activating then deactivating the gameobject.");
		}

		[ContextMenu("Show Sample")]
		public void ShowSample() {
			Show("Some title", "Some body", "OK", null);
		}

		[ContextMenu("Close")]
		public void Close() {
			m_State.Close();
		}

		public static void Show(string title, string body, string affirmative, Action response) {
			m_Response = response;
			m_Instance.m_Title.text = title;
			m_Instance.m_Body.text = body;
			m_Instance.m_Affirmative.text = affirmative;

			m_Instance.m_State.Open();
		}

		public void Respond() {
			m_Response?.Invoke();
			m_Response = null;

			m_State.Close();
		}
	}
}
