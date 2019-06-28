using UnityEngine;

namespace Adrenak.Shiain {
	public abstract class PopulationWidget : MonoBehaviour {
		/// <summary>
		/// Use this to initialize the widget.
		/// Cast the object to the required type
		/// and use the values
		/// </summary>
		/// <param name="data"></param>
		public abstract void Init(object data);

		/// <summary>
		/// Use this to perform clean up procedures like Destroy
		/// any dynamically created Sprites/Texture2D
		/// </summary>
		public abstract void CleanUp();

		void OnDestroy() {
			CleanUp();
		}
	}
}