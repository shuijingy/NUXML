#region Using Statements
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace NUXML
{
	/// <summary>
	/// Base class for dependency NGUI View fields.
	/// </summary>
	public class NGUIViewFieldBase
	{
		#region Fields

		public NGUIView  ParentNGUIView;
		public string    ViewFieldPath;
      	public bool      IsMapped;
		public bool      _isSet;
      	public event EventHandler ValueSet;

        #endregion

        #region Methods

        /// <summary>
        /// Triggers the ValueSet event.
        /// </summary>
        public void TriggerValueSet()
        {
            if (ValueSet != null)
            {
                ValueSet(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
