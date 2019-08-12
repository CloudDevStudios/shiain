using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Adrenak.Shiain {
	public class MultipleTagGroup : MonoBehaviour {
		public event Action<Tag> OnSelected;
		public event Action<Tag> OnDeselected;
		[SerializeField] Transform m_Container;
		[SerializeField] Tag[] m_Elements;

		List<Tag> m_InnerList = new List<Tag>();

		public Transform Container {
			get { return m_Container; }
		}

		void Awake() {
			Add(m_Elements);
		}

		public Tag[] GetTags() {
			return m_InnerList.ToArray();
		}

		public void Add(Tag[] elements) {
			foreach (var element in elements) {
				m_InnerList.Add(element);

				element.OnSelected += () => OnElementSelected(element);

				element.OnDeselected += () => {
					if (m_InnerList.Contains(element))
						OnDeselected?.Invoke(element);
				};
			}
		}

		void OnElementSelected(Tag element) {
			if (m_InnerList.Contains(element))
				OnSelected?.Invoke(element);
		}

		public void Add(Tag element) {
			Add(new Tag[] { element });
		}

		public void Remove(Tag[] elements) {
			foreach (var element in elements)
				m_InnerList.Remove(element);
		}

		public void Remove(Tag element) {
			m_InnerList.Remove(element);
		}
	}
}
