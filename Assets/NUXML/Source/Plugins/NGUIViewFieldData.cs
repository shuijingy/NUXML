﻿#region Using Statements
using NUXML.ValueConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
#endregion

namespace NUXML
{
	/// <summary>
	/// Contains data about a view field.
	/// </summary>
	public class NGUIViewFieldData
	{
		#region Fields

		public string   ViewFieldPath;
		public NGUIView SourceNGUIView;
		public NGUIView TargetNGUIView;
		public string   TargetViewFieldPath;
		public bool     TargetViewSet;
		public ValueConverter ValueConverter;
		public bool     IsOwner;
		public bool     IsViewFieldBaseType;
		public NGUIViewFieldPathInfo NGUIViewFieldPathInfo;
		public string ViewFieldTypeName;
		public Type   ViewFieldType;
		public bool   ViewFieldPathParsed;
		public string ParseError;
		public bool   SevereParseError;
		public bool   PropagateFirst;

		private HashSet<ValueObserver> _valueObservers;
		private bool _isSet;
		private bool _isSetInitialized;

		#endregion

		#region Methods

		/// <summary>
		/// Sets value of field.
		/// </summary>
		public object SetValue(object inValue, 
							   HashSet<NGUIViewFieldData> callstack, 
							   bool updateDefaultState = true,
							   ValueConverterContext context = null, 
						       bool notifyObservers = true)
		{
			if (callstack.Contains(this))
			{
				return null;
			}

			callstack.Add(this);

			if (!IsOwner)
			{
				var targetView = GetTargetView();
				if (targetView == null)
				{
					Debug.LogError(String.Format("[NUXML] {0}: Unable to assign value \"{1}\" to view field \"{2}\". View along path is null.", SourceNGUIView.GameObjectName, inValue, ViewFieldPath));
					return null;
				}

				return targetView.SetValue(TargetViewFieldPath, inValue, updateDefaultState, callstack, null, notifyObservers);
			}

			// check if path has been parsed
			if (!ViewFieldPathParsed)
			{
				// attempt to parse path
				if (!ParseViewFieldPath())
				{
					// path can't be resolved at this point
					if (SevereParseError)
					{
						// severe parse error means the path is incorrect
						Debug.LogError(String.Format("[NUXML] {0}: Unable to assign value \"{1}\". {2}", SourceNGUIView.GameObjectName, inValue, ParseError));
					}

					// unsevere parse errors can be expected, e.g. value along path is null
					return null;
				}
			}

			object value = inValue;

			// get converted value            
			if (ValueConverter != null)
			{
				var conversionResult = ValueConverter.Convert(value, context != null ? context : ValueConverterContext.Default);
				if (!conversionResult.Success)
				{
					Debug.LogError(String.Format("[NUXML] {0}: Unable to assign value \"{1}\" to view field \"{2}\". Value converion failed. {3}", SourceNGUIView.GameObjectName, value, ViewFieldPath, conversionResult.ErrorMessage));
					return null;
				}
				value = conversionResult.ConvertedValue;
			}

			// set value
			object oldValue = NGUIViewFieldPathInfo.SetValue(SourceNGUIView, value);

			// notify observers if the value has changed
			if (notifyObservers)
			{
				// set isSet-indicator
				SetIsSet();

				bool valueChanged = value != null ? !value.Equals(oldValue) : oldValue != null;
				if (valueChanged)
				{
					NotifyValueObservers(callstack);

					// find dependent view fields and notify their value observers
					SourceNGUIView.NotifyDependentValueObservers(ViewFieldPath);
				}
			}

			return value;
		}

		/// <summary>
		/// Sets isSet-indicator.
		/// </summary>
		public void SetIsSet()
		{
			if (!_isSetInitialized)
			{
				SourceNGUIView.AddIsSetField(ViewFieldPath);
				_isSetInitialized = true;
			}
			_isSet = true;
		}

		/// <summary>
		/// Gets value of field.
		/// </summary>
		public object GetValue(out bool hasValue)
		{
			if (!IsOwner)
			{
				var targetView = GetTargetView();
				if (targetView == null)
				{
					hasValue = false;
					//Debug.LogError(String.Format("[NUXML] {0}: Unable to get value from view field \"{1}\". View along path is null.", SourceNGUIView.GameObjectName, ViewFieldPath));
					return null;
				}

				return targetView.GetValue(TargetViewFieldPath, out hasValue);
			}
			else
			{
				// check if path has been parsed
				if (!ViewFieldPathParsed)
				{
					// attempt to parse path
					if (!ParseViewFieldPath())
					{
						hasValue = false;
						return null;
					}
				}

				return NGUIViewFieldPathInfo.GetValue(SourceNGUIView, out hasValue);
			}
		}

		/// <summary>
		/// Registers a value observer.
		/// </summary>
		public void RegisterValueObserver(ValueObserver valueObserver)
		{
			if (_valueObservers == null)
			{
				_valueObservers = new HashSet<ValueObserver>();
			}

			_valueObservers.Add(valueObserver);
		}

		/// <summary>
		/// Notifies all value observers that value has been set.
		/// </summary>
		public void NotifyValueObservers(HashSet<NGUIViewFieldData> callstack)
		{
			if (_valueObservers == null)
			{
				return;
			}

			foreach (var valueObserver in _valueObservers)
			{
				valueObserver.Notify(callstack);
			}
		}

		/// <summary>
		/// Notifies all binding value observers that value has been set.
		/// </summary>
		public void NotifyBindingValueObservers(HashSet<NGUIViewFieldData> callstack)
		{
			if (_valueObservers == null)
			{
				return;
			}

			foreach (var valueObserver in _valueObservers)
			{
				if (valueObserver is BindingValueObserver)
				{
					valueObserver.Notify(callstack);
				}
			}
		}

		/// <summary>
		/// Notifies all change handler value observers that value has been set.
		/// </summary>
		public void NotifyChangeHandlerValueObservers(HashSet<NGUIViewFieldData> callstack)
		{
			if (_valueObservers == null)
			{
				return;
			}

			foreach (var valueObserver in _valueObservers)
			{
				if (valueObserver is ChangeHandlerValueObserver)
				{
					valueObserver.Notify(callstack);
				}
			}
		}

		/// <summary>
		/// Gets field data from field path.
		/// </summary>
		public static NGUIViewFieldData FromViewFieldPath(NGUIView sourceView, string viewFieldPath)
		{
			if (String.IsNullOrEmpty(viewFieldPath) || sourceView == null)
			{
				return null;
			}

			NGUIViewFieldData fieldData = new NGUIViewFieldData();
			fieldData.ViewFieldPath = viewFieldPath;
			fieldData.TargetNGUIView      = sourceView;
			fieldData.TargetViewFieldPath = viewFieldPath;
			fieldData.SourceNGUIView = sourceView;
			fieldData.IsOwner = true;
			fieldData.NGUIViewFieldPathInfo = new NGUIViewFieldPathInfo();
			fieldData.ViewFieldTypeName = sourceView.ViewTypeName;

			Type viewType = typeof(NGUIView);
			var viewFields = viewFieldPath.Split('.');

			// do we have a view field path consisting of multiple view fields?
			if (viewFields.Length > 1)
			{
				// yes. get first view field
				var firstViewField = viewFields[0];

				// is this a field that refers to another view?
				var fieldInfo = sourceView.GetType().GetField(firstViewField);
				if (fieldInfo != null && viewType.IsAssignableFrom(fieldInfo.FieldType))
				{
					// yes. set target view and return
					fieldData.NGUIViewFieldPathInfo.MemberInfo.Add(fieldInfo);
					fieldData.IsOwner = false;
					fieldData.TargetViewSet = false;
					fieldData.TargetViewFieldPath = String.Join(".", viewFields.Skip(1).ToArray());
					return fieldData;
				}

				// is this a property that refers to a view?
				var propertyInfo = sourceView.GetType().GetProperty(firstViewField);
				if (propertyInfo != null && viewType.IsAssignableFrom(propertyInfo.PropertyType))
				{
					// yes. set target view and return
					fieldData.NGUIViewFieldPathInfo.MemberInfo.Add(propertyInfo);
					fieldData.IsOwner = false;
					fieldData.TargetViewSet = false;
					fieldData.TargetViewFieldPath = String.Join(".", viewFields.Skip(1).ToArray());
					return fieldData;
				}

				// does first view field or property exist?
				if (fieldInfo == null && propertyInfo == null)
				{
					// no. check if it refers to a view in the hierarchy
					var result = fieldData.TargetNGUIView.Find<View>(x => x.Id == viewFields[0], true, fieldData.TargetView);
					if (result == null)
					{
						// no. assume that it refers to this view (in cases like x.SetValue(() => x.Field, value))
						return FromViewFieldPath(sourceView, String.Join(".", viewFields.Skip(1).ToArray()));
					}

					// view found
					fieldData.IsOwner = false;
					fieldData.TargetViewSet = true;
					fieldData.TargetNGUIView = result;
					fieldData.TargetViewFieldPath = String.Join(".", viewFields.Skip(1).ToArray());
					return fieldData;
				}
			}

			// try parse the path
			fieldData.ParseViewFieldPath();
			return fieldData;
		}

		/// <summary>
		/// Tries to parse the view field path and get view field path info. Called only if we're the owner of the field.
		/// </summary>
		public bool ParseViewFieldPath()
		{
			SevereParseError = false;
			ViewFieldPathParsed = false;
			NGUIViewFieldPathInfo.MemberInfo.Clear();
			NGUIViewFieldPathInfo.Dependencies.Clear();

			// if we get here we are the owner of the field and need to parse the path
			var viewTypeData = NGUIViewData.GetViewTypeData(SourceNGUIView.ViewTypeName);
			ValueConverter = viewTypeData.GetViewFieldValueConverter(ViewFieldPath);

			//Type viewFieldType = SourceNGUIView.GetType();
			var viewFields = ViewFieldPath.Split('.');
			object viewFieldObject = SourceNGUIView;
			var viewFieldBaseType = typeof(NGUIViewFieldBase);

			// parse view field path
			bool parseSuccess = true;
			string dependencyPath = string.Empty;
			for (int i = 0; i < viewFields.Length; ++i)
			{
				bool isLastField = (i == viewFields.Length - 1);
				string viewField = viewFields[i];

				// add dependency
				if (!isLastField)
				{
					dependencyPath += (i > 0 ? "." : "") + viewField;
					NGUIViewFieldPathInfo.Dependencies.Add(dependencyPath);
				}

				if (!parseSuccess)
				{
					continue;
				}

				var viewFieldType = viewFieldObject.GetType();
				var memberInfo = viewFieldType.GetFieldInfo(viewField);
				if (memberInfo == null)
				{
					SevereParseError = true;
					ParseError = String.Format("Unable to parse view field path \"{0}\". Couldn't find member with the name \"{1}\".", ViewFieldPath, viewField);
					return false;
				}

				NGUIViewFieldPathInfo.MemberInfo.Add(memberInfo);
				ViewFieldType = memberInfo.GetFieldType();

				// handle special ViewFieldBase types
				if (viewFieldBaseType.IsAssignableFrom(ViewFieldType))
				{
					viewFieldObject = memberInfo.GetFieldValue(viewFieldObject);
					if (viewFieldObject == null)
					{
						ParseError = String.Format("Unable to parse view field path \"{0}\". Field/property with the name \"{1}\" was null.", ViewFieldPath, viewField);
						parseSuccess = false;
						continue;
					}

					memberInfo = ViewFieldType.GetFieldInfo("_value"); // set internal dependency view field directly
					NGUIViewFieldPathInfo.MemberInfo.Add(memberInfo);
					ViewFieldType = memberInfo.GetFieldType();
					IsViewFieldBaseType = isLastField;
				}

				if (isLastField)
				{
					ViewFieldType = memberInfo.GetFieldType();
					ViewFieldTypeName = ViewFieldType.Name;
					ValueConverter = ValueConverter ?? ViewData.GetValueConverterForType(ViewFieldTypeName);

					// handle special case if converter is null and field type is enum
					if (ValueConverter == null && ViewFieldType.IsEnum())
					{
						ValueConverter = new EnumValueConverter(ViewFieldType);
					}
				}
				else
				{
					viewFieldObject = memberInfo.GetFieldValue(viewFieldObject);
				}

				if (viewFieldObject == null)
				{
					ParseError = String.Format("Unable to parse view field path \"{0}\". Field/property with the name \"{1}\" was null.", ViewFieldPath, viewField);
					parseSuccess = false;
					continue;
				}
			}

			ViewFieldPathParsed = parseSuccess;
			return parseSuccess;
		}

		/// <summary>
		/// Gets target view. Only called if this view isn't the owner.
		/// </summary>
		public NGUIView GetTargetView()
		{
			if (TargetViewSet)
			{
				return TargetNGUIView;
			}

			bool hasValue;
			return NGUIViewFieldPathInfo.GetValue(SourceNGUIView, out hasValue) as NGUIView;
		}

		/// <summary>
		/// Gets bool indicating if the view field has been set.
		/// </summary>
		public bool IsSet()
		{
			if (_isSetInitialized || _isSet) 
			{
				return _isSet;
			}

			// check with source view if the view field has been set
			_isSetInitialized = true;
			_isSet = SourceNGUIView.GetIsSetFieldValue(ViewFieldPath);
			return _isSet;
		}

		#endregion
	}
}

