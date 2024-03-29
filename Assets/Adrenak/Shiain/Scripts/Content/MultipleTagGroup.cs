﻿using System.Linq;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Adrenak.Shiain {
	public class MultipleTagGroup : MonoBehaviour {
		public class AddRequest {
			public string id;
			public string text;
			public bool isSelected;
			public object data;
		}

		public event Action<Tag> OnSelected;
		public event Action<Tag> OnDeselected;
		public List<Tag> Tags { get; private set; }
		public Transform Container { get { return m_Container; } }

		[Header("Setup")]
		[SerializeField] Tag m_Prefab;
		[SerializeField] Transform m_Container;
		public Color[] tagColors;

		[Header("Init")]
		[SerializeField] List<AddRequest> m_InitialValues;

		float m_LastModifyTime;

		void Awake() {
			Tags = new List<Tag>();
			Add(m_InitialValues);
		}

		private void Update() {
			// For some reason, marking for redraw only once doesn't work
			// So, from the time of modification we invoke this every frame for
			// .5 seconds
			if (Time.time > m_LastModifyTime && Time.time < m_LastModifyTime + .5f) 
				LayoutRebuilder.MarkLayoutForRebuild(GetComponent<RectTransform>());				
		}

		public void Rebuild() {
			StartCoroutine(RebuildInternal());
		}

		IEnumerator RebuildInternal() {
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			LayoutRebuilder.MarkLayoutForRebuild(GetComponent<RectTransform>());
		}

		public void Add(List<AddRequest> requests) {
			var createdTags = new List<Tag>();
			try {
				foreach (var req in requests)
					Add(req);
			}
			catch {
				foreach (var tag in createdTags) {
					Tags.Remove(tag);
					Destroy(tag);
				}
			}
		}

		public void Add(AddRequest req) {
			foreach (var _tag in Tags) 
				if (_tag.ID == req.id) 
					return;

			var tag = InstantiateTag(req);
			if (Tags.Where(x => x.ID == tag.ID).Count() > 0) {
				Tags.Remove(tag);
				Destroy(tag);
				throw new Exception("Tags IDs should be unique");
			}
			RegisterTag(tag);
		}

		void RegisterTag(Tag tag) {
			Tags.Add(tag);
			tag.OnSelected += () => {
				if (Tags.Contains(tag))
					OnSelected?.Invoke(tag);
			};

			tag.OnDeselected += () => {
				if (Tags.Contains(tag))
					OnDeselected?.Invoke(tag);
			};
		}

		Tag InstantiateTag(AddRequest req) {
			var go = Instantiate(m_Prefab, m_Container);
			var tag = go.GetComponent<Tag>();

			tag.Init(req.id, req.text, req.isSelected, req.data);
			tag.SetColor(RandomColor);

			m_LastModifyTime = Time.time;

			return tag;
		}

		public void Remove(Tag[] tags) {
			foreach (var tag in tags)
				Tags.Remove(tag);
		}

		public void Remove(Tag tag) {
			Tags.Remove(tag);
			Destroy(tag);
			m_LastModifyTime = Time.time;
		}

		public int IndexOf(Tag tag) {
			return Tags.IndexOf(tag);
		}

		Color RandomColor {
			get { return tagColors[Random.Range(0, tagColors.Length)]; }
		}
	}
}
