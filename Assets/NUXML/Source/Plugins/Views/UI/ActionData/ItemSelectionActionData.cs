#region Using Statements
using NUXML.Views.UI;
using System;
using UnityEngine.EventSystems;
#endregion

namespace NUXML.Views.UI
{
    /// <summary>
    /// Item selection action data.
    /// </summary>
    public class ItemSelectionActionData : ActionData
    {
        #region Fields

        public ListItem ItemView;
        public object Item;
        public bool IsSelected;

        #endregion
    }
}
