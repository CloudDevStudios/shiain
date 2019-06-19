using UnityEngine;
using UnityEngine.Events;

namespace Adrenak.Shiain {
	[ExecuteInEditMode]
	public class UIEvents : MonoBehaviour {
		public UnityEvent onOpened;
		public UnityEvent onClosed;
		public UnityEvent onMinimized;
		public UnityEvent onMaximized;
		public UnityEvent onForward;
		public UnityEvent onBack;

		public bool IsOpen { get; private set; }
		public bool IsMaximized { get; private set; }

		public void Open() {
			IsOpen = true;
			onOpened?.Invoke();
		}

		public void Close() {
			IsOpen = false;
			onClosed?.Invoke();
		}

		public void Maximize() {
			IsMaximized = true;
			onMaximized?.Invoke();
		}

		public void Minimize() {
			IsMaximized = false;
			onMinimized?.Invoke();
		}

		public void Forward() {
			onForward?.Invoke();
		}

		public void Back() {
			onBack?.Invoke();
		}
	}
}
