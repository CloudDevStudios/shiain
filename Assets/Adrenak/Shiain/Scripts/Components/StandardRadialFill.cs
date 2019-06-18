using UnityEngine;
using UnityEngine.UI;

namespace Adrenak.Shiain {
	public class StandardRadialFill : MonoBehaviour {
		string ShaderName {
			get {
				if (Image != null)
					return "Adrenak/RadialProgressBar/StandardRadialFill";
				else if (Renderer != null)
					return "Adrenak/RadialProgressBar/StandardRadialFillUI";
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
				if(Image != null)
					Image.material = value;
				if(Renderer != null)
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

		// ================================================
		// BACKGROUND
		// ================================================
		public Color BGFillColor {
			get { return Mat.GetColor("_Backgroundfillcolor"); }
			set { Mat.SetColor("_Backgroundfillcolor", value); }
		}

		public float BGOpacity {
			get { return Mat.GetFloat("_Backgroundopacity"); }
			set { Mat.SetFloat("_Backgroundopacity", value); }
		}

		public Color BGBorderColor {
			get { return Mat.GetColor("_Backgroundbordercolor"); }
			set { Mat.SetColor("_Backgroundbordercolor", value); }
		}

		public float BGBorderOpacity {
			get { return Mat.GetFloat("_Backgroundborderopacity"); }
			set { Mat.SetFloat("_Backgroundborderopacity", value); }
		}

		public float BGBorderRadialWidth {
			get { return Mat.GetFloat("_Backgroundborderradialwidth"); }
			set { Mat.SetFloat("_Backgroundborderradialwidth", value); }
		}

		public float BGBorderTangentWidth {
			get { return Mat.GetFloat("_Backgroundbordertangentwidth"); }
			set { Mat.SetFloat("_Backgroundbordertangentwidth", value); }
		}

		// ================================================
		// BAR BORDER
		// ================================================
		public Color BarBorderMinColor {
			get { return Mat.GetColor("_Bordermincolor"); }
			set { Mat.SetColor("_Bordermincolor", value); }
		}

		public Color BarBorderMaxColor {
			get { return Mat.GetColor("_Bordermaxcolor"); }
			set { Mat.SetColor("_Bordermaxcolor", value); }
		}

		public float BarBorderOpacity {
			get { return Mat.GetFloat("_Mainbarborderopacity"); }
			set { Mat.SetFloat("_Mainbarborderopacity", value); }
		}

		public float BarBorderRadialWidth {
			get { return Mat.GetFloat("_Mainborderradialwidth"); }
			set { Mat.SetFloat("_Mainborderradialwidth", value); }
		}

		public float BarBorderTangentWidth {
			get { return Mat.GetFloat("_Mainbordertangentwidth"); }
			set { Mat.SetFloat("_Mainbordertangentwidth", value); }
		}

		// ================================================
		// BAR MAIN TEX
		// ================================================
		public Texture MainTex {
			get { return Mat.GetTexture("_Maintex"); }
			set { Mat.SetTexture("_Maintex", value); }
		}

		public Color MainTexMinColor {
			get { return Mat.GetColor("_Barmincolor"); }
			set { Mat.SetColor("_Barmincolor", value); }
		}

		public Color MainTexMaxColor {
			get { return Mat.GetColor("_Barmaxcolor"); }
			set { Mat.SetColor("_Barmaxcolor", value); }
		}

		public float MainTexOpacity {
			get { return Mat.GetFloat("_Maintexopacity"); }
			set { Mat.SetFloat("_Maintexopacity", value); }
		}

		public float MainTexContrast {
			get { return Mat.GetFloat("_Maintexcontrast"); }
			set { Mat.SetFloat("_Maintexcontrast", value); }
		}

		public bool MainTexInvert {
			get { return Mat.GetFloat("_Invertmaintex") == 1; }
			set { Mat.SetFloat("_Invertmaintex", value == true ? 1 : 0); }
		}

		// ================================================
		// BAR SECONDARY TEX
		// ================================================
		public Texture SecondaryTex {
			get { return Mat.GetTexture("_Secondarytex"); }
			set { Mat.SetTexture("_Secondarytex", value); }
		}

		public Color SecondaryTexMinColor {
			get { return Mat.GetColor("_Barsecondarymincolor"); }
			set { Mat.SetColor("_Barsecondarymincolor", value); }
		}

		public Color SecondaryTexMaxColor {
			get { return Mat.GetColor("_Barsecondarymaxcolor"); }
			set { Mat.SetColor("_Barsecondarymaxcolor", value); }
		}

		public float SecondaryTexOpacity {
			get { return Mat.GetFloat("_Secondarytexopacity"); }
			set { Mat.SetFloat("_Secondarytexopacity", value); }
		}

		public float SecondaryTexContrast {
			get { return Mat.GetFloat("_Secondarytexcontrast"); }
			set { Mat.SetFloat("_Secondarytexcontrast", value); }
		}

		public bool SecondarynTexInvert {
			get { return Mat.GetFloat("_Invertsecondarytex") == 1; }
			set { Mat.SetFloat("_Invertsecondarytex", value == true ? 1 : 0); }
		}


		// ================================================
		// BAR NOISE TEX
		// ================================================
		public Texture NoiseTex {
			get { return Mat.GetTexture("_Noisetex"); }
			set { Mat.SetTexture("_Noisetex", value); }
		}

		public float NoiseIntensity {
			get { return Mat.GetFloat("_Noiseintensity"); }
			set { Mat.SetFloat("_Noiseintensity", value); }
		}

		public float NoiseTexContrast {
			get { return Mat.GetFloat("_Noisetexcontrast"); }
			set { Mat.SetFloat("_Noisetexcontrast", value); }
		}

		public bool NoiseTexInvert {
			get { return Mat.GetFloat("_Invertnoisetex") == 1; }
			set { Mat.SetFloat("_Invertnoisetex", value == true ? 1 : 0); }
		}
	}
}