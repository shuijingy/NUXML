#region Using Statements
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace NUXML
{   
	/// <summary>
	/// Generic base class for dependency view fields.
	/// </summary>
	public class NGUIViewField<T> : NGUIViewFieldBase
	{
		#region Fields

		public T _value;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets view field notifying observers if the value has changed.
		/// </summary>
		public T Value
		{
			get
			{
				if (ParentNGUIView != null)
				{
					return (T)ParentNGUIView.GetValue(ViewFieldPath);
				}

				return _value;
			}
			set
			{
				if (ParentNGUIView != null)
				{
					ParentNGUIView.SetValue(ViewFieldPath, value);
				}
				else
				{
					_value = value;
					_isSet = true;
				}
			}
		}

		/// <summary>
		/// Sets view field directly without notifying observers that the value has changed.
		/// </summary>
		public T DirectValue
		{
			set
			{
				if (ParentNGUIView != null)
				{
					ParentNGUIView.SetValue(ViewFieldPath, value, true, null, null, false);
				}
				else
				{
					_value = value;
					_isSet = true;
				}
			}
		}

		/// <summary>
		/// Gets boolean indicating if the value has been set. 
		/// </summary>
		public bool IsSet
		{
			get
			{
				if (ParentNGUIView != null)
				{
					return ParentNGUIView.IsSet(ViewFieldPath);
				}
				else
				{
					return _isSet;
				}
			}
		}

		#endregion
	}
}
