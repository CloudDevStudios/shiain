using System.Linq;
using System.Collections.Generic;
using Adrenak.Shiain.NaughtyAttributes;
using UnityEngine;

namespace Adrenak.Shiain {
	public class SimplePopulator : MonoBehaviour, IPopulator {
		public object[] Data { get; private set; }
		public Pagination Pagination { get; private set; }

		[ShowNativeProperty] public bool IsPopulated { get; private set; }
		[SerializeField] Transform m_Container;

		PopulationWidget m_Widget;
		List<PopulationWidget> m_Instantiated = new List<PopulationWidget>();

		public void SetWidgetPrefab(PopulationWidget widget) {
			m_Widget = widget;
		}

		public void Populate(object[] data, int pageSize) {
			if (IsPopulated) {
				Debug.Log("Populator on" + gameObject.name + " is already populated.");
				return;
			}
			if(m_Widget == null) {
				Debug.Log("No widget prefab in " + gameObject.name + " populator");
				return;
			}

			Data = data;
			Pagination = Pagination ?? new Pagination(data.Length, pageSize);
			CreateWidgets();
		}

		void CreateWidgets() {
			IsPopulated = true;
			foreach(var model in Pagination.Get(Data)) {
				var instance = Instantiate(m_Widget, m_Container);
				instance.Init(model);
				m_Instantiated.Add(instance);
			}
		}

		public void Next() {
			Clear();
			var result = Pagination.Next();
			CreateWidgets();
		}

		public void Previous() {
			Clear();
			var result = Pagination.Previous();
			CreateWidgets();
		}

		public void Clear() {
			if (!IsPopulated) return;
			IsPopulated = false;

			foreach(var p in m_Instantiated) {
				if (p.gameObject != null)
					Destroy(p.gameObject);
			}
			m_Instantiated.Clear();
		}
	}
}
