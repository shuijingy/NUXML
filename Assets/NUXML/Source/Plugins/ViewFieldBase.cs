﻿#region Using Statements
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace NUXML
{
    /// <summary>
    /// Base class for dependency view fields.
    /// </summary>
    public class ViewFieldBase
    {
        #region Fields

        public View   ParentView;
        public string ViewFieldPath;
        public bool   _isSet;

        #endregion
    }
}
