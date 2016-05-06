﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using NUXML;
#endregion

namespace NUXML.Editor
{
    /// <summary>
    /// New menu items.
    /// </summary>
    public class MenuItems
    {
        #region Methods

        [MenuItem("Assets/Create/[View]")]
        
		static void CreateViewMenuItem()
        {
            var    configuration = Configuration.Instance;
            string path          = AssetDatabase.GetAssetPath(Selection.activeObject);
            string comparePath   = path.EndsWith("/") ? path : path + "/";


            // check if path is under the "Assets/Views/" folder
            if (String.IsNullOrEmpty(path) || !configuration.ViewPaths.Any(x => comparePath.StartsWith(x, StringComparison.OrdinalIgnoreCase)))
            {
                path = "Assets/Views";
                System.IO.Directory.CreateDirectory("Assets/Views/");
            }
            else
            {
                if (!Directory.Exists(path))
                {
                    // try removing filename from path
                    path = Path.GetDirectoryName(path);
                    if (!Directory.Exists(path))
                    {
                        Debug.LogError(String.Format("Unable to create view at path \"{0}\". Directory not found.", path));
                        return; 
                    }
                }
            }

            // create new view asset
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/NewView.xml");
            File.WriteAllText(assetPathAndName, "<NewView>\n    <Label Text=\"My New View\" />\n</NewView>");
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = AssetDatabase.LoadAssetAtPath(assetPathAndName, typeof(TextAsset));
        }

        #endregion
    }
}
