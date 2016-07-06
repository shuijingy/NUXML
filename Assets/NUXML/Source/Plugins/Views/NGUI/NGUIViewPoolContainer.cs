#region Using Statements
using NUXML.Views.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace NUXML.Views.UI
{
	/// <summary>
	/// Pools views for fast creation.
	/// </summary>
	[HideInPresenter]
	public class NGUIViewPoolContainer : NGUIWidget
	{
		#region Fields

		public _int     PoolSize;
		public _int     MaxPoolSize;
		public NGUIView Template;

		#endregion

		#region Methods

		/// <summary>
		/// Initializes the view.
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();
			UpdateViewPool();            
		}

		/// <summary>
		/// Updates the view pool based on settings.
		/// </summary>
		public void UpdateViewPool()
		{
			if (PoolSize > MaxPoolSize)
			{
				MaxPoolSize.Value = PoolSize.Value;
			}

			var templateTypeName = Template != null ? Template.ViewTypeName : null;
			var itemsToDestroy = new List<NGUIView>();

			// remove any items not of the right type
			foreach (var child in this)
			{
				if (child.ViewTypeName != templateTypeName)
				{
					itemsToDestroy.Add(child);
				}
			}

			itemsToDestroy.ForEach(x => x.DestroyNGUI());            

			// fill remaining space of pool with views
			if (Template == null || ChildCount >= PoolSize)
			{
				return;
			}

			int addCount = PoolSize - ChildCount;
			for (int i = 0; i < addCount; ++i)
			{
				var item = CreateNGUIView(Template);
				item.Deactivate();
				item.InitializeNGUIViews();
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// Boolean indicating if pool container is full.
		/// </summary>
		public bool IsFull
		{
			get
			{
				return ChildCount >= MaxPoolSize;
			}
		}

		#endregion
	}
}
