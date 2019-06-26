﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Adrenak.Shiain.UIShapesKit
{
	[AddComponentMenu("UI/Shapes/Rectangle", 1)]
	public class Rectangle : MaskableGraphic, IShape
	{

		public GeoUtils.OutlineShapeProperties ShapeProperties =
			new GeoUtils.OutlineShapeProperties();

		public UIShapesKit.RoundedRects.RoundedProperties RoundedProperties = 
			new UIShapesKit.RoundedRects.RoundedProperties();

		public GeoUtils.OutlineProperties OutlineProperties =
			new GeoUtils.OutlineProperties();

		public GeoUtils.ShadowsProperties ShadowProperties = new GeoUtils.ShadowsProperties();

		public GeoUtils.AntiAliasingProperties AntiAliasingProperties = 
			new GeoUtils.AntiAliasingProperties();

		public Sprite Sprite;

		UIShapesKit.RoundedRects.RoundedCornerUnitPositionData unitPositionData;
		GeoUtils.EdgeGradientData edgeGradientData;

		public void ForceMeshUpdate()
		{
			SetVerticesDirty();
			SetMaterialDirty();
		}

		#if UNITY_EDITOR
		protected override void OnValidate()
		{
			RoundedProperties.OnCheck(rectTransform.rect);
			OutlineProperties.OnCheck();
			AntiAliasingProperties.OnCheck();

			ForceMeshUpdate();
		}
		#endif

		protected override void OnPopulateMesh(VertexHelper vh)	{
			vh.Clear();

			Rect pixelRect = RectTransformUtility.PixelAdjustRect(rectTransform, canvas);

			RoundedProperties.UpdateAdjusted(pixelRect, 0.0f);
			AntiAliasingProperties.UpdateAdjusted(canvas);
			OutlineProperties.UpdateAdjusted();
			ShadowProperties.UpdateAdjusted();

			// draw fill shadows
			if (ShadowProperties.ShadowsEnabled)
			{
				if (ShapeProperties.DrawFill && ShapeProperties.DrawFillShadow)
				{
					for (int i = 0; i < ShadowProperties.Shadows.Length; i++)
					{
						edgeGradientData.SetActiveData(
							1.0f - ShadowProperties.Shadows[i].Softness,
							ShadowProperties.Shadows[i].Size,
							AntiAliasingProperties.Adjusted
						);

						UIShapesKit.RoundedRects.AddRoundedRect(
							ref vh,
							ShadowProperties.GetCenterOffset(pixelRect.center, i),
							pixelRect.width,
							pixelRect.height,
							RoundedProperties,
							ShadowProperties.Shadows[i].Color,
							GeoUtils.ZeroV2,
							ref unitPositionData,
							edgeGradientData
						);
					}
				}
			}

			if (ShadowProperties.ShowShape && ShapeProperties.DrawFill)
			{
				if (AntiAliasingProperties.Adjusted > 0.0f)
				{
					edgeGradientData.SetActiveData(
						1.0f,
						0.0f,
						AntiAliasingProperties.Adjusted
					);
				}
				else
				{
					edgeGradientData.Reset();
				}

				UIShapesKit.RoundedRects.AddRoundedRect(
					ref vh,
					pixelRect.center,
					pixelRect.width,
					pixelRect.height,
					RoundedProperties,
					ShapeProperties.FillColor,
					GeoUtils.ZeroV2,
					ref unitPositionData,
					edgeGradientData
				);
			}

			if (ShadowProperties.ShadowsEnabled)
			{
				// draw outline shadows
				if (ShapeProperties.DrawOutline && ShapeProperties.DrawOutlineShadow)
				{
					for (int i = 0; i < ShadowProperties.Shadows.Length; i++)
					{
						edgeGradientData.SetActiveData(
							1.0f - ShadowProperties.Shadows[i].Softness,
							ShadowProperties.Shadows[i].Size,
							AntiAliasingProperties.Adjusted
						);

						UIShapesKit.RoundedRects.AddRoundedRectLine(
							ref vh,
							ShadowProperties.GetCenterOffset(pixelRect.center, i),
							pixelRect.width,
							pixelRect.height,
							OutlineProperties,
							RoundedProperties,
							ShadowProperties.Shadows[i].Color,
							GeoUtils.ZeroV2,
							ref unitPositionData,
							edgeGradientData
						);
					}
				}
			}

			// fill
			if (ShadowProperties.ShowShape && ShapeProperties.DrawOutline)
			{
				if (AntiAliasingProperties.Adjusted > 0.0f)
				{
					edgeGradientData.SetActiveData(
						1.0f,
						0.0f,
						AntiAliasingProperties.Adjusted
					);
				}
				else
				{
					edgeGradientData.Reset();
				}

				UIShapesKit.RoundedRects.AddRoundedRectLine(
					ref vh,
					pixelRect.center,
					pixelRect.width,
					pixelRect.height,
					OutlineProperties,
					RoundedProperties,
					ShapeProperties.OutlineColor,
					GeoUtils.ZeroV2,
					ref unitPositionData,
					edgeGradientData
				);
			}
		}

		protected override void UpdateMaterial()
		{
			base.UpdateMaterial();

			// check if this sprite has an associated alpha texture (generated when splitting RGBA = RGB + A as two textures without alpha)

			if (Sprite == null)
			{
				canvasRenderer.SetAlphaTexture(null);
				return;
			}

			Texture2D alphaTex = Sprite.associatedAlphaSplitTexture;

			if (alphaTex != null)
			{
				canvasRenderer.SetAlphaTexture(alphaTex);
			}
		}

		public override Texture mainTexture
		{
			get
			{
				if (Sprite == null)
				{
					if (material != null && material.mainTexture != null)
					{
						return material.mainTexture;
					}
					return s_WhiteTexture;
				}

				return Sprite.texture;
			}
		}
	}
}