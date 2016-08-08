#region Using Statements
using NUXML.ValueConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using NUXML.Views.UI;
#endregion

namespace NUXML.Views.NGUI
{
	/// <summary>
	/// Panel view.
	/// </summary>

	[HideInPresenter]
	public class NGUIPanel : NGUIView
	{
		#region Fields
	
		public UIPanel PanelComponent;
		#endregion

		#region Methods

		/// <summary>
		/// Sets default values of the view
		/// </summary>
		public override void SetDefaultValues()
		{
			base.SetDefaultValues();

			Debug.Log(GameObjectName + " SetDefaultValues");
		}

		/// <summary>
		/// Called when the behavior of the view has been changed.
		/// </summary>
		public override void BehaviorChanged()
		{
			base.BehaviorChanged();

			Debug.Log(GameObjectName + " SetDefaultValues");
		}

		public override void IsActiveChanged()
		{
			base.IsActiveChanged();

			Debug.Log(GameObjectName + " SetDefaultValues");
		}

		#endregion
	}
}
