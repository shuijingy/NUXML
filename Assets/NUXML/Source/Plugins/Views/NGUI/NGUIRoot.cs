#region Using Statements
using NUXML.ValueConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using NUXML.Views.UI;
#endregion

namespace NUXML.Views.NGUI
{
	/// <summary>
	/// Canvas view.
	/// </summary>
	/// <d>The canvas view is used to render UI components and controls things like draw sort order, scaling and render mode. 
	/// In order for UIViews to be rendered and positioned correctly they must be put under a parent NGUIRoot or a subclass of (like UIRoot).</d>
	[HideInPresenter]
	public class NGUIRoot : UIView
	{
		#region Fields

		[MapTo("UIRoot.Scaling")]
		public _Scaling_ ScalingStyle;

		#endregion


		#region Methods

		/// <summary>
		/// Sets default values of the view.
		/// </summary>
		public override void SetDefaultValues()
		{
			base.SetDefaultValues();
	
			//Dummy

		}

		#endregion
	}
}
