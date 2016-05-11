#region Using Statements
using NUXML.Views.UI;
using System;
using UnityEngine.EventSystems;
#endregion

namespace NUXML.Views.UI
{
    /// <summary>
    /// Tab selection action data.
    /// </summary>
    public class TabSelectionActionData : ActionData
    {
        #region Fields

        public Tab TabView;
        public object Item;
        public bool IsSelected;

        #endregion
    }
}
