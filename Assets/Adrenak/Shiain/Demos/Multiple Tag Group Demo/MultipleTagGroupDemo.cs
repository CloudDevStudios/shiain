using UnityEngine;
using UnityEngine.UI;
using Adrenak.Shiain;
using Adrenak.Unex;

public class MultipleTagGroupDemo : MonoBehaviour {
	public MultipleTagGroup m_Group;
	public Tag prefab;
	public string[] names;
	public Text message;

	void Start() {
		Application.targetFrameRate = 60;
		var colors = new Color[] { Color.red, Color.green, Color.magenta, Color.blue, new Color(1, .5f, 0) };

		foreach (var name in names) {
			var tag = Instantiate(prefab, m_Group.Container);
			tag.gameObject.name = name;
			tag.Init(name);
			tag.SetColor(colors[(int)Random.Range(0, colors.Length)]);
			m_Group.Add(tag);
		}

		Runner.New().WaitForSeconds(.1f, () => {
			LayoutRebuilder.MarkLayoutForRebuild(GetComponent<RectTransform>());
		});

		m_Group.OnSelected += M_Group_OnSelected;
		m_Group.OnDeselected += M_Group_OnDeselected;
	}

	private void M_Group_OnDeselected(Tag obj) {
		if (message != null)
			message.text = ("Deselected : " + obj.gameObject.name);
	}

	private void M_Group_OnSelected(Tag obj) {
		if (message != null)
			message.text = ("Selected : " + obj.gameObject.name);
	}
}
