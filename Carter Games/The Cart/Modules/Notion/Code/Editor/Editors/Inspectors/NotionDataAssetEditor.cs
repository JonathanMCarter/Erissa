﻿#if CARTERGAMES_CART_MODULE_NOTIONDATA && UNITY_EDITOR

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

using System.Linq;
using System.Reflection;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.NotionData.Editor
{
    [CustomEditor(typeof(NotionDataAsset<>), true)]
    public sealed class NotionDataAssetEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            NotionApiRequestHandler.DataReceived.Remove(OnDataReceived);
            NotionApiRequestHandler.RequestError.Remove(OnErrorReceived);
        }


        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space(5f);
            
            RenderNotionSettings();
            
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
            
            EditorGUILayout.Space(1.5f);
            GeneralUtilEditor.DrawHorizontalGUILine();

            DrawPropertiesExcluding(serializedObject, "sortProperties");
        }
        

        private void RenderNotionSettings()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1.5f);
            
            EditorGUILayout.LabelField("Notion Database Settings", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("linkToDatabase"), NotionMetaData.DatabaseLink);
            EditorGUILayout.PropertyField(serializedObject.Fp("databaseApiKey"), NotionMetaData.ApiKey);

            EditorGUILayout.BeginHorizontal();

            // Coming... eventually...
            // if (GUILayout.Button("Filters"))
            // {
            //     
            // }
            
            if (GUILayout.Button($"Sorting ({serializedObject.Fp("sortProperties").arraySize})"))
            {
                SortPropertiesWindow.OpenWindow(serializedObject);
            }
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUI.BeginDisabledGroup(
                !NotionSecretKeyValidator.IsKeyValid(serializedObject.Fp("databaseApiKey").stringValue) ||
                string.IsNullOrEmpty(serializedObject.Fp("linkToDatabase").stringValue));


            GUILayout.Space(5f);
            
            GUI.backgroundColor = Color.green;
            
            if (GUILayout.Button("Download Data"))
            {
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    EditorUtility.DisplayDialog("Standalone Notion Data", "You cannot download data while offline.",
                        "Continue");
                    return;
                }
                
                // Do download stuff...
                var databaseId = serializedObject.Fp("linkToDatabase").stringValue.Split('/').Last().Split('?').First();
                
                NotionApiRequestHandler.DataReceived.Remove(OnDataReceived);
                NotionApiRequestHandler.DataReceived.Add(OnDataReceived);
                
                NotionApiRequestHandler.RequestError.Remove(OnErrorReceived);
                NotionApiRequestHandler.RequestError.Add(OnErrorReceived);
                
                NotionApiRequestHandler.ResetRequestData();
                
                var requestData = new NotionRequestData((DataAsset) serializedObject.targetObject, databaseId, serializedObject.Fp("databaseApiKey").stringValue, serializedObject.Fp("sortProperties").ToSortPropertyArray());

                NotionApiRequestHandler.WebRequestPostWithAuth(requestData);
            }
            
            GUI.backgroundColor = Color.white;
            
            EditorGUI.EndDisabledGroup();
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        private void OnDataReceived(NotionRequestResult data)
        {
            var queryResult = NotionDownloadParser.Parse(data.Data);
            
            target.GetType().BaseType.GetMethod("Apply", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.Invoke(serializedObject.targetObject, new object[] { queryResult });

            if (!data.SilentResponse)
            {
                EditorUtility.DisplayDialog("Notion Data Download", "Download completed successfully", "Continue");
            }
            
            NotionApiRequestHandler.DataReceived.Remove(OnDataReceived);
            NotionApiRequestHandler.RequestError.Remove(OnErrorReceived);

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }


        private void OnErrorReceived(NotionRequestError error)
        {
            if (error.Message.Contains("Could not find sort property"))
            {
                EditorUtility.DisplayDialog("Notion Data Download", $"Download failed ({error.Error}):\n{error.Message}", "Continue");
            }
            else
            {
                EditorUtility.DisplayDialog("Notion Data Download", "Download failed, please see console for errors and try again", "Continue");
            }

            
            NotionApiRequestHandler.DataReceived.Remove(OnDataReceived);
            NotionApiRequestHandler.RequestError.Remove(OnErrorReceived);
            
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}

#endif