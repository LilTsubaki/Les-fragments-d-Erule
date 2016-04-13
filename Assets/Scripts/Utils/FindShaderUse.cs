using UnityEngine;
     using UnityEditor;
     using System.Collections.Generic;
     
     public class FindShaderUse : EditorWindow
     {
         string st = "";
         string stArea = "Empty List";
         Vector2 scrollPos;
         bool exact;
     
         [MenuItem("Window/Find Shader Use")]
         public static void ShowWindow()
         {
             EditorWindow.GetWindow(typeof(FindShaderUse));
         }
     
         public void OnGUI()
         {
             GUILayout.Label("Enter shader to find:");
     
             EditorGUILayout.BeginHorizontal();
             st = GUILayout.TextField(st);
             EditorGUIUtility.LookLikeControls(40);
             exact = EditorGUILayout.Toggle("Exact", exact, GUILayout.Width(60));
             EditorGUILayout.EndHorizontal();
     
             if (GUILayout.Button("Find Materials"))
             {
                 FindShader(st);
             }
     
             scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height-60));
             GUILayout.Label(stArea);
             EditorGUILayout.EndScrollView();        
         }
     
         private void FindShader(string shaderName)
         {
             int count = 0;
             stArea = "Materials using shader " + shaderName + ":\n\n";
     
             List<Material> armat = new List<Material>();
     
             Renderer[] arrend = (Renderer[])Resources.FindObjectsOfTypeAll(typeof(Renderer));
             foreach (Renderer rend in arrend)
             {
                 foreach (Material mat in rend.sharedMaterials)
                 {
                     if (!armat.Contains(mat))
                     {
                         armat.Add(mat);
                     }
                 }
             }
     
             foreach (Material mat in armat)
             {
                 if (mat != null && mat.shader != null && mat.shader.name != null)
                 {
                     if ((exact && mat.shader.name == shaderName) || (!exact && mat.shader.name.Contains(shaderName)))
                     {
                         stArea += ">" + mat.name + "\n";
                         count++;
                     }
                 }
             }
     
             stArea += "\n" + count + " materials using shader " + shaderName + " found.";
         }
     }