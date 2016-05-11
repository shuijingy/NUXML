#region Using Statements
using NUXML.ValueConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#endregion

namespace NUXML.Views.UI
{
    /// <summary>
    /// View that holds user interface views.
    /// </summary>
    /// <d>Represents a root UICanvas containing a user interface in the scene.</d>
    [HideInPresenter]
    public class UserInterface : UICanvas
    {
    }
}
