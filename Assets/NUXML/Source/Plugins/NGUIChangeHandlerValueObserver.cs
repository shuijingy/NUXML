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
	/// Change handler value observer.
	/// </summary>
	public class NGUIChangeHandlerValueObserver : NGUIValueObserver
	{
		#region Fields

		public NGUIView  ParentNGUIView;
		public string    ChangeHandlerName;
		public bool      TriggerImmediately;
		public bool      IsValid;

		private MethodInfo _changeHandlerMethod;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public NGUIChangeHandlerValueObserver(NGUIView parentView, string changeHandlerName, bool triggerImmediately)
		{
			ParentNGUIView    = parentView;
			ChangeHandlerName = changeHandlerName;
			TriggerImmediately = triggerImmediately;

			_changeHandlerMethod = ParentNGUIView.GetType().GetMethod(ChangeHandlerName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			IsValid = _changeHandlerMethod != null;
		}
	
		#endregion

		#region Methods

		/// <summary>
		/// Notifies the change handler value observer that value has changed.
		/// </summary>
		public override bool Notify(HashSet<NGUIViewFieldData> callstack)
		{
			if (TriggerImmediately)
			{
				Trigger();
			}
			else
			{
				ParentNGUIView.QueueChangeHandler(ChangeHandlerName);
			}

			return true;
		}

		/// <summary>
		/// Triggers the change handler.
		/// </summary>
		internal void Trigger()
		{
			Debug.Log(String.Format("{0}.{1}() triggered!", ParentNGUIView.ViewTypeName, ChangeHandlerName));
			try
			{
				_changeHandlerMethod.Invoke(ParentNGUIView, null);
			}
			catch (Exception e)
			{
				Debug.LogError(String.Format("[NUXML] {0}: Exception thrown when triggering change handler \"{1}\": {2}", ParentNGUIView.GameObjectName, ChangeHandlerName, Utils.GetError(e)));
			}
		}

		#endregion
	}
}
