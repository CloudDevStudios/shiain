#if !UNITY_2018_3_OR_NEWER
using UnityEngine;

namespace Adrenak.Shiain.UIShapesKit {
	public class MinAttribute : PropertyAttribute {
		public readonly float minFloat;
		public readonly int minInt;

		public MinAttribute(float min) {
			this.minFloat = min;
		}

		public MinAttribute(int min) {
			this.minInt = min;
		}
	}
}
#endif