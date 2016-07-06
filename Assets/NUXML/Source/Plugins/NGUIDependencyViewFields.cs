#region Using Statements
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace NUXML
{   
	[Serializable]
	public class _float_ : NGUIViewField<float>
	{
		public static implicit operator float (_float_ value) { return value.Value; }
	}

	[Serializable]
	public class _string_ : NGUIViewField<string>
	{
		public static implicit operator string (_string_ value) { return value.Value; }
	}

	[Serializable]
	public class _int_ : NGUIViewField<int>
	{
		public static implicit operator int (_int_ value) { return value.Value; }
	}

	[Serializable]
	public class _bool_ : NGUIViewField<bool>
	{
		public static implicit operator bool(_bool_ value) { return value.Value; }
	}

	[Serializable]
	public class _char_ : NGUIViewField<char>
	{
		public static implicit operator char(_char_ value) { return value.Value; }
	}

	[Serializable]
	public class _Color_ : NGUIViewField<Color>
	{
		public static implicit operator Color(_Color_ value) { return value.Value; }
	}

	[Serializable]
	public class _ElementSize_ : NGUIViewField<ElementSize> { }

	[Serializable]
	public class _Font_ : NGUIViewField<Font> { }

	[Serializable]
	public class _ElementMargin_ : NGUIViewField<ElementMargin> { }

	[Serializable]
	public class _Material_ : NGUIViewField<Material> { }

	[Serializable]
	public class _Quaternion_ : NGUIViewField<Quaternion> { }

	[Serializable]
	public class _Sprite_ : NGUIViewField<Sprite> { }

	[Serializable]
	public class _Vector2_ : NGUIViewField<Vector2> { }

	[Serializable]
	public class _Vector3_ : NGUIViewField<Vector3> { }

	[Serializable]
	public class _Vector4_ : NGUIViewField<Vector4> { }

	[Serializable]
	public class _ElementAlignment_ : NGUIViewField<ElementAlignment>
	{
		public static implicit operator ElementAlignment(_ElementAlignment_ value) { return value.Value; }
	}

	[Serializable]
	public class _ElementOrientation_ : NGUIViewField<ElementOrientation>
	{
		public static implicit operator ElementOrientation(_ElementOrientation_ value) { return value.Value; }
	}

	[Serializable]
	public class _AdjustToText_ : NGUIViewField<AdjustToText>
	{
		public static implicit operator AdjustToText(_AdjustToText_ value) { return value.Value; }
	}

	[Serializable]
	public class _FontStyle_ : NGUIViewField<FontStyle>
	{
		public static implicit operator FontStyle(_FontStyle_ value) { return value.Value; }
	}

	[Serializable]
	public class _HorizontalWrapMode_ : NGUIViewField<HorizontalWrapMode>
	{
		public static implicit operator HorizontalWrapMode(_HorizontalWrapMode_ value) { return value.Value; }
	}

	[Serializable]
	public class _VerticalWrapMode_ : NGUIViewField<VerticalWrapMode>
	{
		public static implicit operator VerticalWrapMode(_VerticalWrapMode_ value) { return value.Value; }
	}

	[Serializable]
	public class _FillMethod_ : NGUIViewField<UnityEngine.UI.Image.FillMethod>
	{
		public static implicit operator UnityEngine.UI.Image.FillMethod(_FillMethod_ value) { return value.Value; }
	}

	[Serializable]
	public class _ImageType_ : NGUIViewField<UnityEngine.UI.Image.Type>
	{
		public static implicit operator UnityEngine.UI.Image.Type(_ImageType_ value) { return value.Value; }
	}

	[Serializable]
	public class _ElementSortDirection_ : NGUIViewField<ElementSortDirection>
	{
		public static implicit operator ElementSortDirection(_ElementSortDirection_ value) { return value.Value; }
	}

	[Serializable]
	public class _ImageFillMethod_ : NGUIViewField<UnityEngine.UI.Image.FillMethod>
	{
		public static implicit operator UnityEngine.UI.Image.FillMethod(_ImageFillMethod_ value) { return value.Value; }
	}

	[Serializable]
	public class _InputFieldCharacterValidation_ : NGUIViewField<UnityEngine.UI.InputField.CharacterValidation>
	{
		public static implicit operator UnityEngine.UI.InputField.CharacterValidation(_InputFieldCharacterValidation_ value) { return value.Value; }
	}

	[Serializable]
	public class _InputFieldContentType_ : NGUIViewField<UnityEngine.UI.InputField.ContentType>
	{
		public static implicit operator UnityEngine.UI.InputField.ContentType(_InputFieldContentType_ value) { return value.Value; }
	}

	[Serializable]
	public class _InputFieldInputType_ : NGUIViewField<UnityEngine.UI.InputField.InputType>
	{
		public static implicit operator UnityEngine.UI.InputField.InputType(_InputFieldInputType_ value) { return value.Value; }
	}

	[Serializable]
	public class _TouchScreenKeyboardType_ : NGUIViewField<UnityEngine.TouchScreenKeyboardType>
	{
		public static implicit operator UnityEngine.TouchScreenKeyboardType(_TouchScreenKeyboardType_ value) { return value.Value; }
	}

	[Serializable]
	public class _InputFieldLineType_ : NGUIViewField<UnityEngine.UI.InputField.LineType>
	{
		public static implicit operator UnityEngine.UI.InputField.LineType(_InputFieldLineType_ value) { return value.Value; }
	}

	[Serializable]
	public class _ScrollbarDirection_ : NGUIViewField<UnityEngine.UI.Scrollbar.Direction>
	{
		public static implicit operator UnityEngine.UI.Scrollbar.Direction(_ScrollbarDirection_ value) { return value.Value; }
	}

	#if !UNITY_4_6 && !UNITY_5_0 && !UNITY_5_1
	[Serializable]
	public class _ScrollbarVisibility_ : NGUIViewField<UnityEngine.UI.ScrollRect.ScrollbarVisibility>
	{
		public static implicit operator UnityEngine.UI.ScrollRect.ScrollbarVisibility(_ScrollbarVisibility_ value) { return value.Value; }
	}
	#endif

	[Serializable]
	public class _PanelScrollbarVisibility_ : NGUIViewField<PanelScrollbarVisibility>
	{
		public static implicit operator PanelScrollbarVisibility(_PanelScrollbarVisibility_ value) { return value.Value; }
	}

	[Serializable]
	public class _ScrollRectMovementType_ : NGUIViewField<UnityEngine.UI.ScrollRect.MovementType>
	{
		public static implicit operator UnityEngine.UI.ScrollRect.MovementType(_ScrollRectMovementType_ value) { return value.Value; }
	}

	[Serializable]
	public class _RectTransformComponent_ : NGUIViewField<RectTransform>
	{
		public static implicit operator RectTransform(_RectTransformComponent_ value) { return value.Value; }
	}

	[Serializable]
	public class _ScrollbarComponent_ : NGUIViewField<UnityEngine.UI.Scrollbar>
	{
		public static implicit operator UnityEngine.UI.Scrollbar(_ScrollbarComponent_ value) { return value.Value; }
	}

	[Serializable]
	public class _GraphicRenderMode_ : NGUIViewField<UnityEngine.RenderMode>
	{
		public static implicit operator UnityEngine.RenderMode(_GraphicRenderMode_ value) { return value.Value; }
	}

	[Serializable]
	public class _CameraComponent_ : NGUIViewField<UnityEngine.Camera>
	{
		public static implicit operator UnityEngine.Camera(_CameraComponent_ value) { return value.Value; }
	}

	[Serializable]
	public class _CanvasScaleMode_ : NGUIViewField<UnityEngine.UI.CanvasScaler.ScaleMode>
	{
		public static implicit operator UnityEngine.UI.CanvasScaler.ScaleMode(_CanvasScaleMode_ value) { return value.Value; }
	}

	[Serializable]
	public class _BlockingObjects_ : NGUIViewField<UnityEngine.UI.GraphicRaycaster.BlockingObjects>
	{
		public static implicit operator UnityEngine.UI.GraphicRaycaster.BlockingObjects(_BlockingObjects_ value) { return value.Value; }
	}

	[Serializable]
	public class _GameObject_ : NGUIViewField<UnityEngine.GameObject>
	{
		public static implicit operator UnityEngine.GameObject(_GameObject_ value) { return value.Value; }
	}

	[Serializable]
	public class _HideFlags_ : NGUIViewField<UnityEngine.HideFlags>
	{
		public static implicit operator UnityEngine.HideFlags(_HideFlags_ value) { return value.Value; }
	}

	[Serializable]
	public class _OverflowMode_ : NGUIViewField<OverflowMode>
	{
		public static implicit operator OverflowMode(_OverflowMode_ value) { return value.Value; }
	}

	[Serializable]
	public class _RaycastBlockMode_ : NGUIViewField<RaycastBlockMode>
	{
		public static implicit operator RaycastBlockMode(_RaycastBlockMode_ value) { return value.Value; }
	}

	[Serializable]
	public class _Mesh_ : NGUIViewField<Mesh>
	{
		public static implicit operator Mesh(_Mesh_ value) { return value.Value; }
	}

	public class _UIInput_ : NGUIViewField<UIInput>
	{
		public static implicit operator UIInput(_UIInput_ value) { return value.Value;}
	}

	public class _UILable_ : NGUIViewField<UILabel>
	{
		public static implicit operator UILabel(_UILable_ value) { return value.Value;}
	}

	public class _UIPanel_ : NGUIViewField<UIPanel>
	{
		public static implicit operator UIPanel(_UIPanel_ value) { return value.Value;}
	}

	public class _UIRoot_ : NGUIViewField<UIRoot>
	{
		public static implicit operator UIRoot(_UIRoot_ value) { return value.Value;}
	}

	public class _Scaling_ : NGUIViewField<UIRoot.Scaling>
	{
		public static implicit operator UIRoot.Scaling(_Scaling_ value) { return value.Value;}
	}

	public class _UISprite_ : NGUIViewField<UISprite>
	{
		public static implicit operator UISprite(_UISprite_ value) { return value.Value;}
	}

	public class _UIWidget_ : NGUIViewField<UIWidget>
	{
		public static implicit operator UIWidget(_UIWidget_ value) { return value.Value;}
	}
		
	[Serializable]
	public class _object_ : NGUIViewField<object> { }

	[Serializable]
	public class _IObservableList_ : NGUIViewField<IObservableList> { }

	[Serializable]
	public class _GenericObservableList_ : NGUIViewField<GenericObservableList> { }
}
