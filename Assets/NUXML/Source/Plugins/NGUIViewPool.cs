#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUXML.Views.UI;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace NUXML
{
	/// <summary>
	/// Provides access to a view pool.
	/// </summary>
	public class NGUIViewPool
	{
		#region Fields

		public NGUIViewPoolContainer ViewPoolContainer;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public NGUIViewPool(NGUIViewPoolContainer nguiViewPoolContainer)
		{
			this.ViewPoolContainer = nguiViewPoolContainer;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Inserts a view into the view pool.
		/// </summary>
		public void InsertView(NGUIView nguiView)
		{
			nguiView.MoveTo(ViewPoolContainer, -1, false);
		}

		/// <summary>
		/// Gets first available view in the pool.
		/// </summary>
		public NGUIView GetNGUIView()
		{
			return ViewPoolContainer.GetChildNGUI(0);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets boolean indicating if pool is full.
		/// </summary>
		public bool IsFull
		{
			get
			{
				return ViewPoolContainer.IsFull;
			}
		}

		/// <summary>
		/// Gets boolean indicating if pool is empty.
		/// </summary>
		public bool IsEmpty
		{
			get
			{
				return ViewPoolContainer.ChildCount <= 0;
			}
		}

		#endregion
	}
}
