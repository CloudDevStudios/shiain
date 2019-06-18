using UnityEngine;
using UnityEngine.UI;

namespace Adrenak.Shiain {
	public class SimpleRadialFill : MonoBehaviour {
		string ShaderName {
			get {
				if (Image != null)
					return "Adrenak/RadialProgressBar/SimpleRadialFillUI";
				else if (Renderer != null)
					return "Adrenak/RadialProgressBar/SimpleRadialFill";
				return string.Empty;
			}
		}

		Image m_Image;
		Image Image {
			get {
				if (m_Image == null)
					m_Image = GetComponent<Image>();
				return m_Image;
			}
		}

		Renderer m_Renderer;
		Renderer Renderer {
			get {
				if (m_Renderer == null)
					m_Renderer = GetComponent<Renderer>();
				return m_Renderer;
			}
		}

		Material m_Mat;
		Material Mat {
			get {
				if (m_Mat == null && Image != null)
					m_Mat = Image.material;
				if (m_Mat == null && Renderer != null)
					m_Mat = Renderer.sharedMaterial;
				return m_Mat;
			}
			set {
				m_Mat = value;
				if (Image != null)
					Image.material = value;
				if (Renderer != null)
					Renderer.sharedMaterial = value;
			}
		}

		private void OnValidate() {
			if (Mat == null || Mat.shader.name != ShaderName) {
				Destroy(Mat);
				var mat = new Material(Shader.Find(ShaderName));
				mat.name = "Radial";
				Mat = mat;
			}
		}

		// ================================================
		// ROTATION
		// ================================================
		public float Radius {
			get { return Mat.GetFloat("_Radius"); }
			set { Mat.SetFloat("_Radius", value); }
		}

		public int ArcRange {
			get { return Mat.GetInt("_Arcrange"); }
			set { Mat.SetInt("_Arcrange", value); }
		}

		public float FillPercentage {
			get { return Mat.GetFloat("_Fillpercentage"); }
			set { Mat.SetFloat("_Fillpercentage", value); }
		}

		public float GlobalOpacity {
			get { return Mat.GetFloat("_GlobalOpacity"); }
			set { Mat.SetFloat("_GlobalOpacity", value); }
		}

		public int Rotation {
			get { return Mat.GetInt("_Rotation"); }
			set { Mat.SetInt("_Rotation", value); }
		}

		public Color MainTexMinColor {
			get { return Mat.GetColor("_Barmincolor"); }
			set { Mat.SetColor("_Barmincolor", value); }
		}

		public Color MainTexMaxColor {
			get { return Mat.GetColor("_Barmaxcolor"); }
			set { Mat.SetColor("_Barmaxcolor", value); }
		}
	}
}
