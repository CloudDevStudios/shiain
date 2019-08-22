using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Adrenak.Shiain {
	public class SingularTagGroup : MonoBehaviour {
		public class AddRequest {
			public string id;
			public string text;
			public object data;
		}

		public event Action<Tag> OnSelected;
		public event Action<Tag> OnDeselected;
		public Tag SelectedIndex { get { return m_SelectedTag; } }
		public List<Tag> Tags { get; private set; }
		public Transform Container { get { return m_Container; } }

		[Header("Setup")]
		public Color[] tagColors;
		[SerializeField] Tag m_Prefab;
		[SerializeField] Transform m_Container;

		[Header("Init")]
		[SerializeField] Tag m_SelectedTag;
		[SerializeField] List<AddRequest> m_InitialValues;

		float m_LastModifyTime;

		void Awake() {
			Tags = new List<Tag>();

			// Add dynamic tags on startup
			Add(m_InitialValues);

			if(m_SelectedTag != null)
				SelectTag(m_SelectedTag);
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
				foreach (var req in requests) {
					var tag = Add(req);
					if (tag != null)
						createdTags.Add(tag);
				}
			}
			catch {
				foreach(var tag in createdTags) {
					Tags.Remove(tag);
					Destroy(tag);
				}
			}
		}

		public Tag Add(AddRequest req) {
			// None of the requests should have the same ID
			foreach (var _tag in Tags)
				if (_tag.ID == req.id)
					return null;

			// None of the requests should have the same ID as an existing tag
			if (Tags.Where(x => x.ID == req.id).Count() > 0) {
				Debug.Log("Tags IDs should be unique");
				return null;
			}
			
			var tag = InstantiateTag(req);
			Tags.Add(tag);
			tag.OnSelected += () => {
				SelectTag(tag);
				OnSelected?.Invoke(tag);
			};
			return tag;
		}

		public void SelectTag(Tag tag) {
			m_SelectedTag = tag;

			// Deselect all tags
			foreach (var t in Tags) {
				t.isSilent = true;
				t.HandleDeselection();
				t.isSilent = false;
			}

			m_SelectedTag.isSilent = true;
			m_SelectedTag.HandleSelection();
			m_SelectedTag.isSilent = false;
		}

		Tag InstantiateTag(AddRequest request) {
			var go = Instantiate(m_Prefab, m_Container);
			go.name = request.text;
			var tag = go.GetComponent<Tag>();

			tag.Init(request.id, request.text, false, request.data);
			tag.SetColor(RandomColor);

			m_LastModifyTime = Time.time;

			return tag;
		}

		public void Remove(Tag[] tags) {
			foreach (var tag in tags)
				Remove(tag);
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
