#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
#endregion

namespace NUXML.Editor
{
	/// <summary>
	/// Custom inspector for View components.
	/// </summary>
	[CustomEditor(typeof(NGUIView), true)]
	public class NGUIViewInspector : UnityEditor.Editor
	{
		#region Methods

		/// <summary>
		/// Called when inspector GUI is to be rendered.
		/// </summary>
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			// add button for updating view
			if (GUILayout.Button("Update NGUI View"))
			{
				var view = (NGUIView)target;
				view.LayoutChanged();
			}
		}

		#endregion
	}
}
