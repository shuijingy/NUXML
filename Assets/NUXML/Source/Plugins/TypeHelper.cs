#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using System.Reflection;
using System.Linq.Expressions;
using UnityEngine;
#endregion

namespace NUXML
{
    /// <summary>
    /// Helper methods for finding and instantiating objects through reflection.
    /// </summary>
    public static class TypeHelper
    {
        #region Fields

        private static List<Type> _scriptAssemblyTypes;
        private static Dictionary<Type, Func<ViewFieldBase>>     _viewFieldFactory;
		private static Dictionary<Type, Func<NGUIViewFieldBase>> _viewFieldFactoryNGUI;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a static instance of the class.
        /// </summary>
        static TypeHelper()
        {
            _viewFieldFactory = new Dictionary<Type, Func<ViewFieldBase>>
            {
                { typeof(_float),                   () => new _float() },
                { typeof(_string),                  () => new _string() },
                { typeof(_int),                     () => new _int() },
                { typeof(_bool),                    () => new _bool() },
                { typeof(_char),                    () => new _char() },
                { typeof(_Color),                   () => new _Color() },
                { typeof(_ElementSize),             () => new _ElementSize() },
                { typeof(_Font),                    () => new _Font() },
                { typeof(_ElementMargin),           () => new _ElementMargin() },
                { typeof(_Material),                () => new _Material() },
                { typeof(_Quaternion),              () => new _Quaternion() },
                { typeof(_Sprite),                  () => new _Sprite() },
                { typeof(_Vector2),                 () => new _Vector2() },
                { typeof(_Vector3),                 () => new _Vector3() },
                { typeof(_Vector4),                 () => new _Vector4() },
                { typeof(_ElementAlignment),        () => new _ElementAlignment() },
                { typeof(_ElementOrientation),      () => new _ElementOrientation() },
                { typeof(_AdjustToText),            () => new _AdjustToText() },
                { typeof(_FontStyle),               () => new _FontStyle() },
                { typeof(_HorizontalWrapMode),      () => new _HorizontalWrapMode() },
                { typeof(_VerticalWrapMode),        () => new _VerticalWrapMode() },
                { typeof(_FillMethod),              () => new _FillMethod() },
                { typeof(_ImageType),               () => new _ImageType() },
                { typeof(_ElementSortDirection),    () => new _ElementSortDirection() },
                { typeof(_ImageFillMethod),         () => new _ImageFillMethod() },
                { typeof(_InputFieldCharacterValidation), () => new _InputFieldCharacterValidation() },
                { typeof(_InputFieldContentType),   () => new _InputFieldContentType() },
                { typeof(_InputFieldInputType),     () => new _InputFieldInputType() },
                { typeof(_TouchScreenKeyboardType), () => new _TouchScreenKeyboardType() },
                { typeof(_InputFieldLineType),      () => new _InputFieldLineType() },
                { typeof(_ScrollbarDirection),      () => new _ScrollbarDirection() },
#if !UNITY_4_6 && !UNITY_5_0 && !UNITY_5_1
                { typeof(_ScrollbarVisibility),     () => new _ScrollbarVisibility() },
#endif
                { typeof(_PanelScrollbarVisibility),() => new _PanelScrollbarVisibility() },
                { typeof(_ScrollRectMovementType),  () => new _ScrollRectMovementType() },
                { typeof(_RectTransformComponent),  () => new _RectTransformComponent() },
                { typeof(_ScrollbarComponent),      () => new _ScrollbarComponent() },
                { typeof(_GraphicRenderMode),       () => new _GraphicRenderMode() },
                { typeof(_CameraComponent),         () => new _CameraComponent() },
                { typeof(_CanvasScaleMode),         () => new _CanvasScaleMode() },
                { typeof(_BlockingObjects),         () => new _BlockingObjects() },
                { typeof(_GameObject),              () => new _GameObject() },
                { typeof(_HideFlags),               () => new _HideFlags() },
                { typeof(_OverflowMode),            () => new _OverflowMode() },
                { typeof(_RaycastBlockMode),        () => new _RaycastBlockMode() },
                { typeof(_Mesh),                    () => new _Mesh() },
                { typeof(_object),                  () => new _object() },
                { typeof(_IObservableList),         () => new _IObservableList() },
                { typeof(_GenericObservableList),   () => new _GenericObservableList() },
                { typeof(ViewFieldBase),            () => new ViewFieldBase() }
            };
			_viewFieldFactoryNGUI = new Dictionary<Type, Func<NGUIViewFieldBase>>
			{
				{ typeof(_float_),                   () => new _float_() },
				{ typeof(_string_),                  () => new _string_() },
				{ typeof(_int_),                     () => new _int_() },
				{ typeof(_bool_),                    () => new _bool_() },
				{ typeof(_char_),                    () => new _char_() },
				{ typeof(_Color_),                   () => new _Color_() },
				{ typeof(_ElementSize_),             () => new _ElementSize_() },
				{ typeof(_Font_),                    () => new _Font_() },
				{ typeof(_ElementMargin_),           () => new _ElementMargin_() },
				{ typeof(_Material_),                () => new _Material_() },
				{ typeof(_Quaternion_),              () => new _Quaternion_() },
				{ typeof(_Sprite_),                  () => new _Sprite_() },
				{ typeof(_Vector2_),                 () => new _Vector2_() },
				{ typeof(_Vector3_),                 () => new _Vector3_() },
				{ typeof(_Vector4_),                 () => new _Vector4_() },
				{ typeof(_ElementAlignment_),        () => new _ElementAlignment_() },
				{ typeof(_ElementOrientation_),      () => new _ElementOrientation_() },
				{ typeof(_AdjustToText_),            () => new _AdjustToText_() },
				{ typeof(_FontStyle_),               () => new _FontStyle_() },
				{ typeof(_HorizontalWrapMode_),      () => new _HorizontalWrapMode_() },
				{ typeof(_VerticalWrapMode_),        () => new _VerticalWrapMode_() },
				{ typeof(_FillMethod_),              () => new _FillMethod_() },
				{ typeof(_ImageType_),               () => new _ImageType_() },
				{ typeof(_ElementSortDirection_),    () => new _ElementSortDirection_() },
				{ typeof(_ImageFillMethod_),         () => new _ImageFillMethod_() },
				{ typeof(_InputFieldCharacterValidation_), () => new _InputFieldCharacterValidation_() },
				{ typeof(_InputFieldContentType_),   () => new _InputFieldContentType_() },
				{ typeof(_InputFieldInputType_),     () => new _InputFieldInputType_() },
				{ typeof(_TouchScreenKeyboardType_), () => new _TouchScreenKeyboardType_() },
				{ typeof(_InputFieldLineType_),      () => new _InputFieldLineType_() },
				{ typeof(_ScrollbarDirection_),      () => new _ScrollbarDirection_() },
				#if !UNITY_4_6 && !UNITY_5_0 && !UNITY_5_1
				{ typeof(_ScrollbarVisibility_),     () => new _ScrollbarVisibility_() },
				#endif
				{ typeof(_PanelScrollbarVisibility_),() => new _PanelScrollbarVisibility_() },
				{ typeof(_ScrollRectMovementType_),  () => new _ScrollRectMovementType_() },
				{ typeof(_RectTransformComponent_),  () => new _RectTransformComponent_() },
				{ typeof(_ScrollbarComponent_),      () => new _ScrollbarComponent_() },
				{ typeof(_GraphicRenderMode_),       () => new _GraphicRenderMode_() },
				{ typeof(_CameraComponent_),         () => new _CameraComponent_() },
				{ typeof(_CanvasScaleMode_),         () => new _CanvasScaleMode_() },
				{ typeof(_BlockingObjects_),         () => new _BlockingObjects_() },
				{ typeof(_GameObject_),              () => new _GameObject_() },
				{ typeof(_HideFlags_),               () => new _HideFlags_() },
				{ typeof(_OverflowMode_),            () => new _OverflowMode_() },
				{ typeof(_RaycastBlockMode_),        () => new _RaycastBlockMode_() },
				{ typeof(_Mesh_),                    () => new _Mesh_() },
				{ typeof(_object_),                  () => new _object_() },
				{ typeof(_IObservableList_),         () => new _IObservableList_() },
				{ typeof(_GenericObservableList_),   () => new _GenericObservableList_() },
				{ typeof(NGUIViewFieldBase),        () => new NGUIViewFieldBase() }
			};
        }

        #endregion

        #region Methods                

        /// <summary>
        /// Gets all types derived from specified base type.
        /// </summary>
        public static IEnumerable<Type> FindDerivedTypes(Type baseType)
        {
            var derivedTypes = new List<Type>();
            if (_scriptAssemblyTypes != null)
            {
                foreach (var type in _scriptAssemblyTypes)
                {
                    if (baseType.IsAssignableFrom(type))
                    {
                        derivedTypes.Add(type);
                    }
                }
                return derivedTypes;
            }            

            // look in assembly csharp only
            var scriptAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name.StartsWith("Assembly-")).ToList();
            if (scriptAssemblies.Count > 0)
            {
                _scriptAssemblyTypes = new List<Type>();
                foreach (var assembly in scriptAssemblies)
                {
                    _scriptAssemblyTypes.AddRange(assembly.GetLoadableTypes().ToList());                    
                }

                foreach (var type in _scriptAssemblyTypes)
                {
                    if (baseType.IsAssignableFrom(type))
                    {
                        derivedTypes.Add(type);
                    }
                }
            }
            else
            {
                // look in all assemblies
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (var type in assembly.GetLoadableTypes())
                    {
                        try
                        {
                            if (baseType.IsAssignableFrom(type))
                            {
                                derivedTypes.Add(type);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }

            return derivedTypes;
        }

        /// <summary>
        /// Extension method for getting loadable types from an assembly.
        /// </summary>
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null)
            {
                return Enumerable.Empty<Type>();
            }

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

        /// <summary>
        /// Instiantiates a type.
        /// </summary>
        public static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// Helper method for generating dependency field activators.
        /// </summary>
        public static void PrintDependencyFields()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var derivedType in FindDerivedTypes(typeof(ViewFieldBase)))
            {
                sb.AppendLine(String.Format("{{ typeof({0}), () => new {0}() }},", derivedType.Name));
            }

            Utils.Log(sb.ToString());
        }
        
        /// <summary>
        /// Creates a dependency field from type.
        /// </summary>
        public static ViewFieldBase CreateViewField(Type type)
        {
            Func<ViewFieldBase> activator;
            if (_viewFieldFactory.TryGetValue(type, out activator))
            {
                return activator();
            }
            else
            {
                return TypeHelper.CreateInstance(type) as ViewFieldBase;
            }
        }

		/// <summary>
		/// Creates a dependency field from type.
		/// </summary>
		public static NGUIViewFieldBase CreateNGUIViewField(Type type)
		{
			Func<NGUIViewFieldBase> activator;
			if (_viewFieldFactoryNGUI.TryGetValue(type, out activator))
			{
				return activator();
			}
			else
			{
				return TypeHelper.CreateInstance(type) as NGUIViewFieldBase;
			}
		}

        #endregion
    }
}