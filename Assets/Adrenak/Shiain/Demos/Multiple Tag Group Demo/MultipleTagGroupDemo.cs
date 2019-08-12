using UnityEngine;
using UnityEngine.UI;
using Adrenak.Shiain;

public class MultipleTagGroupDemo : MonoBehaviour {
	public MultipleTagGroup m_Group;
	public Tag prefab;
	public string[] names;
	public Text message;

	void Start() {
		foreach(var name in names) {
			var tag = Instantiate(prefab, m_Group.Container);
			tag.gameObject.name = name;
			tag.Init(name);
			m_Group.Add(tag);
		}

		m_Group.OnSelected += M_Group_OnSelected;
		m_Group.OnDeselected += M_Group_OnDeselected;
	}

	private void M_Group_OnDeselected(Tag obj) {
		message.text = ("Deselected : " + obj.gameObject.name);
	}

	private void M_Group_OnSelected(Tag obj) {
		message.text = ("Selected : " + obj.gameObject.name);
	}
}
