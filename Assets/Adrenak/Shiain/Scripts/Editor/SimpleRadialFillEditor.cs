﻿using UnityEngine;
using UnityEditor;

namespace Adrenak.Shiain {
	public class SimpleRadialFillEditor : ShaderGUI {
		MaterialEditor _materialEditor;
		MaterialProperty[] _properties;

		//MAIN SETTINGS
		private MaterialProperty _Radius = null;
		private MaterialProperty _Arcrange = null;
		private MaterialProperty _Fillpercentage = null;
		private MaterialProperty _Globalopacity = null;
		private MaterialProperty _Rotation = null;
		//MAINBAR
		private MaterialProperty _Barmincolor = null;
		private MaterialProperty _Barmaxcolor = null;

		protected static bool ShowMainSettings = true;
		protected static bool ShowMainbarSettings = true;


		public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties) {
			_properties = properties;
			_materialEditor = materialEditor;
			EditorGUI.BeginChangeCheck();
			DrawGUI();
		}

		void DrawGUI() {
			GetProperties();

			startFoldout();
			ShowMainSettings = EditorGUILayout.Foldout(ShowMainSettings, "General");
			if (ShowMainSettings) {
				DrawMainSettings();
			}
			endFoldout();


			startFoldout();
			ShowMainbarSettings = EditorGUILayout.Foldout(ShowMainbarSettings, "Bar color");
			if (ShowMainbarSettings) {
				DrawMainbarSettings();
			}
			endFoldout();
		}

		void GetProperties() {

			//MAIN SETTINGS
			_Radius = FindProperty("_Radius", _properties);
			_Arcrange = FindProperty("_Arcrange", _properties);
			_Fillpercentage = FindProperty("_Fillpercentage", _properties);
			_Globalopacity = FindProperty("_Globalopacity", _properties);
			_Rotation = FindProperty("_Rotation", _properties);
			//MAINBAR
			_Barmincolor = FindProperty("_Barmincolor", _properties);
			_Barmaxcolor = FindProperty("_Barmaxcolor", _properties);

		}

		void DrawMainSettings() {
			//MAIN SETTINGS
			EditorGUI.indentLevel++;
			_materialEditor.SetDefaultGUIWidths();
			_materialEditor.ShaderProperty(_Radius, _Radius.displayName);
			_materialEditor.ShaderProperty(_Arcrange, _Arcrange.displayName);
			_materialEditor.ShaderProperty(_Fillpercentage, _Fillpercentage.displayName);
			_materialEditor.ShaderProperty(_Globalopacity, _Globalopacity.displayName);
			_materialEditor.ShaderProperty(_Rotation, _Rotation.displayName);
			EditorGUI.indentLevel--;
		}

		void DrawMainbarSettings() {
			//GLOBAL EMISSION
			EditorGUI.indentLevel++;
			_materialEditor.SetDefaultGUIWidths();
			_materialEditor.ShaderProperty(_Barmincolor, "Color (min)");
			_materialEditor.ShaderProperty(_Barmaxcolor, "Color (max)");
			EditorGUI.indentLevel--;
		}

		void startFoldout() {
			EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			EditorGUI.indentLevel++;
		}

		void endFoldout() {
			EditorGUI.indentLevel--;
			EditorGUILayout.EndVertical();
		}
	}
}