﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Adrenak.Shiain.UIShapesKit
{

	[AddComponentMenu("UI/Shapes/Polygon", 30)]
	public class Polygon : MaskableGraphic, IShape
	{

		public GeoUtils.ShapeProperties ShapeProperties =
			new GeoUtils.ShapeProperties();

		public UIShapesKit.PointsList.PointListsProperties PointListsProperties =
			new UIShapesKit.PointsList.PointListsProperties();

		public UIShapesKit.Polygons.PolygonProperties PolygonProperties =
			new UIShapesKit.Polygons.PolygonProperties();

		public GeoUtils.ShadowsProperties ShadowProperties = new GeoUtils.ShadowsProperties();

		public GeoUtils.AntiAliasingProperties AntiAliasingProperties = 
			new GeoUtils.AntiAliasingProperties();

		UIShapesKit.PointsList.PointsData[] pointsListData =
			new Adrenak.Shiain.UIShapesKit.PointsList.PointsData[] { new UIShapesKit.PointsList.PointsData()};
		GeoUtils.EdgeGradientData edgeGradientData;

		Rect pixelRect;

		public void ForceMeshUpdate()
		{
			if (pointsListData == null || pointsListData.Length != PointListsProperties.PointListProperties.Length)
			{
				System.Array.Resize(ref pointsListData, PointListsProperties.PointListProperties.Length);
			}

			for (int i = 0; i < pointsListData.Length; i++)
			{
				pointsListData[i].NeedsUpdate = true;
				PointListsProperties.PointListProperties[i].GeneratorData.NeedsUpdate = true;
			}
				
			SetVerticesDirty();
			SetMaterialDirty();
		}

		protected override void OnEnable()
		{
			for (int i = 0; i < pointsListData.Length; i++)
			{
				pointsListData[i].IsClosed = true;
			}

			base.OnEnable();
		}

		#if UNITY_EDITOR
		protected override void OnValidate()
		{
			if (pointsListData == null || pointsListData.Length != PointListsProperties.PointListProperties.Length)
			{
				System.Array.Resize(ref pointsListData, PointListsProperties.PointListProperties.Length);
			}

			for (int i = 0; i < pointsListData.Length; i++)
			{
				pointsListData[i].NeedsUpdate = true;
				pointsListData[i].IsClosed = true;
			}



			AntiAliasingProperties.OnCheck();

			ForceMeshUpdate();
		}
		#endif

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();

			if (pointsListData == null || pointsListData.Length != PointListsProperties.PointListProperties.Length)
			{
				System.Array.Resize(ref pointsListData, PointListsProperties.PointListProperties.Length);

				for (int i = 0; i < pointsListData.Length; i++)
				{
					pointsListData[i].NeedsUpdate = true;
					pointsListData[i].IsClosed = true;
				}
			}

			pixelRect = RectTransformUtility.PixelAdjustRect(rectTransform, canvas);

			AntiAliasingProperties.UpdateAdjusted(canvas);
			ShadowProperties.UpdateAdjusted();

			for (int i = 0; i < PointListsProperties.PointListProperties.Length; i++)
			{
				PointListsProperties.PointListProperties[i].GeneratorData.SkipLastPosition = true;
				PointListsProperties.PointListProperties[i].SetPoints();
			}

			for (int i = 0; i < PointListsProperties.PointListProperties.Length; i++)
			{
				if (
					PointListsProperties.PointListProperties[i].Positions != null &&
					PointListsProperties.PointListProperties[i].Positions.Length > 2
				) {
					PolygonProperties.UpdateAdjusted(PointListsProperties.PointListProperties[i]);

					// shadows
					if (ShadowProperties.ShadowsEnabled)
					{
						for (int j = 0; j < ShadowProperties.Shadows.Length; j++)
						{
							edgeGradientData.SetActiveData(
								1.0f - ShadowProperties.Shadows[j].Softness,
								ShadowProperties.Shadows[j].Size,
								AntiAliasingProperties.Adjusted
							);

							UIShapesKit.Polygons.AddPolygon(
								ref vh,
								PolygonProperties,
								PointListsProperties.PointListProperties[i],
								ShadowProperties.GetCenterOffset(pixelRect.center, j),
								ShadowProperties.Shadows[j].Color,
								GeoUtils.ZeroV2,
								ref pointsListData[i],
								edgeGradientData
							);
						}
					}
				}
			}


			for (int i = 0; i < PointListsProperties.PointListProperties.Length; i++)
			{
				if (
					PointListsProperties.PointListProperties[i].Positions != null &&
					PointListsProperties.PointListProperties[i].Positions.Length > 2
				) {
					PolygonProperties.UpdateAdjusted(PointListsProperties.PointListProperties[i]);

					// fill
					if (ShadowProperties.ShowShape)
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

						UIShapesKit.Polygons.AddPolygon(
							ref vh,
							PolygonProperties,
							PointListsProperties.PointListProperties[i],
							pixelRect.center,
							ShapeProperties.FillColor,
							GeoUtils.ZeroV2,
							ref pointsListData[i],
							edgeGradientData
						);
					}
				}
			}


		}

	}
}
