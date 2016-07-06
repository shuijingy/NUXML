#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace NUXML
{
	/// <summary>
	/// Value observer.
	/// </summary>
	public class NGUIValueObserver
	{
		#region Constructor

		/// <summary>
		/// Initializes static instance of the class.
		/// </summary>
		static NGUIValueObserver()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Notifies the value observer.
		/// </summary>
		public virtual bool Notify(HashSet<NGUIViewFieldData> callstack)
		{
			return true;
		}

		#endregion
	}
}
