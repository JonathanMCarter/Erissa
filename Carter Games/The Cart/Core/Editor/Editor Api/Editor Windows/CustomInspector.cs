﻿/*
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

using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Editor
{
	/// <summary>
	/// Inherit from to make a custom inspector.
	/// </summary>
	public abstract class CustomInspector : UnityEditor.Editor
	{
		/// <summary>
		/// Override to change the inspector GUI entirely.
		/// </summary>
		public override void OnInspectorGUI()
		{
			GUILayout.Space(7.5f);
			DrawScriptField();
			DrawInspectorGUI();
		}


		/// <summary>
		/// Implement to add your own GUI to the base GUI of the custom editor.
		/// </summary>
		protected abstract void DrawInspectorGUI();


		/// <summary>
		/// Draws the script field for the type the inspector is for.
		/// </summary>
		/// <param name="styleName"></param>
		private void DrawScriptField(string styleName = "HelpBox")
		{
			EditorGUILayout.BeginVertical(styleName);
			EditorGUILayout.Space(1.5f);

			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.PropertyField(serializedObject.Fp("m_Script"));
			EditorGUI.EndDisabledGroup();
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
		}


		/// <summary>
		/// Draws the base inspector when called.
		/// </summary>
		protected void DrawBaseInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
}