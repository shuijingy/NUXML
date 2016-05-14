﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
#endregion

namespace NUXML
{
	/// <summary>
	/// Contains data about a view field binding.
	/// </summary>
	[Serializable]
	public class NGUIViewFieldBinding
	{
		#region Fields

		public static Regex BindingRegex          = new Regex(@"{(?<field>[A-Za-z0-9_#!=@\.\[\]]+)(?<format>:[^}]+)?}");
		public static Regex TransformBindingRegex = new Regex(@"(?<function>\$[A-Za-z0-9_]+)\((?<params>[A-Za-z0-9_#!=@\{\}\.\, ]+)\)");
		public string BindingString;
		public string ViewField;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public NGUIViewFieldBinding()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Checks if a view field value contains bindings.
		/// </summary>
		public static bool ValueHasBindings(string value)
		{
			return BindingRegex.IsMatch(value) || TransformBindingRegex.IsMatch(value);
		}

		#endregion
	}
}
