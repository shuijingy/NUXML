﻿#region Using Statements
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
    public class ChangeHandlerValueObserver : ValueObserver
    {
        #region Fields

        public View      ParentView;
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
        public ChangeHandlerValueObserver(View parentView, string changeHandlerName, bool triggerImmediately)
        {
            ParentView = parentView;
            ChangeHandlerName = changeHandlerName;
            TriggerImmediately = triggerImmediately;
            
            _changeHandlerMethod = ParentView.GetType().GetMethod(ChangeHandlerName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            IsValid = _changeHandlerMethod != null;
        }

		//TODO
		public ChangeHandlerValueObserver(NGUIView parentNGUIView, string changeHandlerName, bool triggerImmediately)
		{
			ParentNGUIView     = parentNGUIView;
			ChangeHandlerName  = changeHandlerName;
			TriggerImmediately = triggerImmediately;

			_changeHandlerMethod = ParentView.GetType().GetMethod(ChangeHandlerName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			IsValid = _changeHandlerMethod != null;
		}

        #endregion

        #region Methods

        /// <summary>
        /// Notifies the change handler value observer that value has changed.
        /// </summary>
        public override bool Notify(HashSet<ViewFieldData> callstack)
        {
            if (TriggerImmediately)
            {
                Trigger();
            }
            else
            {
                ParentView.QueueChangeHandler(ChangeHandlerName);
            }

            return true;
        }

        /// <summary>
        /// Triggers the change handler.
        /// </summary>
        internal void Trigger()
        {
            //Debug.Log(String.Format("{0}.{1}() triggered!", ParentView.ViewTypeName, ChangeHandlerName));
            try
            {
                _changeHandlerMethod.Invoke(ParentView, null);
            }
            catch (Exception e)
            {
                Debug.LogError(String.Format("[NUXML] {0}: Exception thrown when triggering change handler \"{1}\": {2}", ParentView.GameObjectName, ChangeHandlerName, Utils.GetError(e)));
            }
        }

        #endregion
    }
}
