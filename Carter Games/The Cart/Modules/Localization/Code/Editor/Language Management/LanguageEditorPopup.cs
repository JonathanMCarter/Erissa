﻿#if CARTERGAMES_CART_MODULE_LOCALIZATION && UNITY_EDITOR

/*
 * Copyright (c) 2024 Carter Games
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;

namespace CarterGames.Cart.Modules.Localization.Editor
{
	public sealed class LanguageEditorPopup : EditorWindow
	{
		private SerializedObject AssetObjectRef => ScriptableRef.GetAssetDef<DataAssetDefinedLanguages>().ObjectRef;


		private void OnGUI()
		{
			EditorGUILayout.Space(5f);

			EditorGUILayout.HelpBox(
				"Add languages below. The name is a label for selecting more than anything and the code is used more in logic etc.",
				MessageType.Info);
			
			EditorGUILayout.Space(5f);
			
			EditorGUILayout.BeginVertical("HelpBox");
			
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(AssetObjectRef.Fp("languages"));

			if (EditorGUI.EndChangeCheck())
			{
				AssetObjectRef.ApplyModifiedProperties();
				AssetObjectRef.Update();
			}
			
			EditorGUILayout.EndVertical();
		}


		public static void ShowLanguagesEditorWindow()
		{
			GetWindow<LanguageEditorPopup>(true, "Manage Languages").Show();
		}
	}
}

#endif