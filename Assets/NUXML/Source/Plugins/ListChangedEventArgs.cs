﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
#endregion

namespace NUXML
{
    /// <summary>
    /// Contains arguments for the list changed event.
    /// </summary>
    public class ListChangedEventArgs : EventArgs
    {
        public ListChangeAction ListChangeAction;
        public int    StartIndex;
        public int    EndIndex;
        public string FieldPath;
    }

    /// <summary>
    /// Enum indicating what type of list changed has occurred.
    /// </summary>
    public enum ListChangeAction
    {
        /// <summary>
        /// Items added to list.
        /// </summary>
        Add = 0,

        /// <summary>
        /// Items moved (rearranged) within list.
        /// </summary>
        Move = 1,

        /// <summary>
        /// Items removed from list.
        /// </summary>
        Remove = 2,

        /// <summary>
        /// Items replaced in list.
        /// </summary>
        Replace = 3,

        /// <summary>
        /// All items cleared from list.
        /// </summary>
        Clear = 4,

        /// <summary>
        /// Items modified in list.
        /// </summary>
        Modify = 5,

        /// <summary>
        /// Item selected in list
        /// </summary>
        Select = 6
    }
}
