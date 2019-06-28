using System.Collections.Generic;

namespace Adrenak.Shiain {
	public interface IPopulator {
		void SetWidgetPrefab(PopulationWidget widget);
		void Populate(object[] populatables, int size);
		void Clear();
	}
}
