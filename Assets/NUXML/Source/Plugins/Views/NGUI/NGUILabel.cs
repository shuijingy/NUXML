#region Using Statements
using NUXML.ValueConverters;
using NUXML.Views;
using NUXML.Views.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
#endregion

namespace NUXML.Views.NGUI
{
	/// <summary>
	/// Label view.
	/// </summary>
	/// <d>Presents (read-only) text. Can adjust its size to text. Can display rich text with BBCode style syntax.</d>
	[HideInPresenter]
	public class NGUILabel : NGUIView
	{
		#region Fields

		#region TextComponent

		/// <summary>
		/// NGUI UI component used to render text.
		/// </summary>
		/// <d>NGUI UI component used to render text.</d>
		public UILabel TextComponent;

		/// <summary>
		/// Label text.
		/// </summary>
		/// <d>The text of the label. The label can be set to adjust its size to the text through the AdjustToText field.</d>
		[MapTo("TextComponent.text", "TextChanged")]
		public _string_ Text;

		/// <summary>
		/// Label text font.
		/// </summary>
		/// <d>The font of the label text.</d>
		[MapTo("TextComponent.font", "TextStyleChanged")]
		public _Font_ Font;

		/// <summary>
		/// Label text font size.
		/// </summary>
		/// <d>The size of the label text.</d>
		[MapTo("TextComponent.fontSize", "TextStyleChanged")]
		public _int_ FontSize;

		/// <summary>
		/// Label text font color.
		/// </summary>
		/// <d>The font color of the label text.</d>
		[MapTo("TextComponent.color")]
		public _Color_ FontColor;

		/// <summary>
		/// Label text font style.
		/// </summary>
		/// <d>The font style of the label text.</d>
		[MapTo("TextComponent.fontStyle", "TextStyleChanged")]
		public _FontStyle_ FontStyle;

		#endregion

		/// <summary>
		/// Label text alignment.
		/// </summary>
		/// <d>The alignment of the text inside the label. Can be used with TextMargin and TextOffset to get desired positioning of the text.</d>
		[ChangeHandler("BehaviorChanged")]
		public _ElementAlignment_ TextAlignment;

		/// <summary>
		/// Adjust label to text.
		/// </summary>
		/// <d>An enum indiciating how the label should adjust its size to the label text. By default the label does not adjust its size to the text.</d>
		[ChangeHandler("LayoutChanged")]
		public _AdjustToText_ AdjustToText;

		private static Regex _tagRegex = new Regex(@"\[(?<tag>[^\]]+)\]");

		#endregion

		#region Methods

		/// <summary>
		/// Sets default values of the view.
		/// </summary>
		public override void SetDefaultValues()
		{
			base.SetDefaultValues();

//			Width.DirectValue  = new ElementSize(120);
//			Height.DirectValue = new ElementSize(40);

			TextAlignment.DirectValue = ElementAlignment.Left;
			if (TextComponent != null)
			{
				TextComponent.fontSize  = 18;
				TextComponent.color     = Color.black;
			}
		}

		/// <summary>
		/// Called when text changes.
		/// </summary>
		public virtual void TextChanged()
		{
			// parse text
			TextComponent.text = ParseText(TextComponent.text);

			if (AdjustToText == NUXML.AdjustToText.None)
			{
				// size of view doesn't change with text, no need to notify parents
				QueueChangeHandler("LayoutChanged");
			}
			else
			{
				// size of view changes with text so notify parents                
				LayoutsChanged();
			}
		}

		/// <summary>
		/// Called when a field affecting the layout of the view has changed.
		/// </summary>
		public override void LayoutChanged()
		{            
			if (TextComponent == null)
			{
				return;
			}

//			AdjustLabelToText();
			base.LayoutChanged();
		}

		/// <summary>
		/// Called when the text style has changed.
		/// </summary>
		public virtual void TextStyleChanged()
		{
			if (AdjustToText == NUXML.AdjustToText.None)
			{
				return;
			}

			LayoutsChanged();
		}

		/// <summary>
		/// Called when a field affecting the behavior and visual appearance of the view has changed.
		/// </summary>
		public override void BehaviorChanged()
		{
			base.BehaviorChanged();

//			TextComponent.alignment = TextAnchor;

		}

//		/// <summary>
//		/// Adjusts the label to text.
//		/// </summary>
//		public void AdjustLabelToText()
//		{
//			if (AdjustToText == NUXML.AdjustToText.Width)
//			{
//				Width.DirectValue = new ElementSize(TextComponent.preferredWidth);
//			}
//			else if (AdjustToText == NUXML.AdjustToText.Height)
//			{
//				Height.DirectValue = new ElementSize(TextComponent.preferredHeight);
//			}
//			else if (AdjustToText == NUXML.AdjustToText.WidthAndHeight)
//			{
//				Width.DirectValue = new ElementSize(TextComponent.preferredWidth);
//				Height.DirectValue = new ElementSize(TextComponent.preferredHeight);
//			}
//		}

		/// <summary>
		/// Replaces BBCode style tags with unity rich text syntax and parses embedded views.
		/// </summary>
		public string ParseText(string text)
		{
			if (text == null)
			{
				return String.Empty;
			}

//			if (!TextComponent.supportRichText)
//				return text;

			string formattedText = string.Empty;
			string separatorString = "&sp;";

			// search for tokens and apply formatting and embedded views
			List<TextToken> tokens = new List<TextToken>();
			formattedText = _tagRegex.Replace(text, x =>
				{
					string tag = x.Groups["tag"].Value.Trim();
					string tagNoWs = tag.RemoveWhitespace();

					// check if tag matches default tokens
					if (String.Equals("B", tagNoWs, StringComparison.OrdinalIgnoreCase))
					{
						// bold
						tokens.Add(new TextToken { TextTokenType = TextTokenType.BoldStart });
						return separatorString;
					}
					else if (String.Equals("/B", tagNoWs, StringComparison.OrdinalIgnoreCase))
					{
						// bold end
						tokens.Add(new TextToken { TextTokenType = TextTokenType.BoldEnd });
						return separatorString;
					}
					else if (String.Equals("I", tagNoWs, StringComparison.OrdinalIgnoreCase))
					{
						// italic
						tokens.Add(new TextToken { TextTokenType = TextTokenType.ItalicStart });
						return separatorString;
					}
					else if (String.Equals("/I", tagNoWs, StringComparison.OrdinalIgnoreCase))
					{
						// italic end
						tokens.Add(new TextToken { TextTokenType = TextTokenType.ItalicEnd });
						return separatorString;
					}
					else if (tagNoWs.StartsWith("SIZE=", StringComparison.OrdinalIgnoreCase))
					{
						// parse size value
						var vc = new IntValueConverter();
						var convertResult = vc.Convert(tagNoWs.Substring(5), ValueConverterContext.Empty);
						if (!convertResult.Success)
						{
							// unable to parse token
							Debug.LogError(String.Format("[NUXML] {0}: Unable to parse text embedded size tag \"[{1}]\". {2}", GameObjectName, tag, convertResult.ErrorMessage));
							return String.Format("[{0}]", tag);
						}

						tokens.Add(new TextToken { TextTokenType = TextTokenType.SizeStart, FontSize = (int)convertResult.ConvertedValue });
						return separatorString;
					}
					else if (String.Equals("/SIZE", tagNoWs, StringComparison.OrdinalIgnoreCase))
					{
						// size end
						tokens.Add(new TextToken { TextTokenType = TextTokenType.SizeEnd });
						return separatorString;
					}
					else if (tagNoWs.StartsWith("COLOR=", StringComparison.OrdinalIgnoreCase))
					{
						// parse color value
						var vc = new ColorValueConverter();
						var convertResult = vc.Convert(tagNoWs.Substring(6), ValueConverterContext.Empty);
						if (!convertResult.Success)
						{
							// unable to parse token
							Debug.LogError(String.Format("[NUXML] {0}: Unable to parse text embedded color tag \"[{1}]\". {2}", GameObjectName, tag, convertResult.ErrorMessage));
							return String.Format("[{0}]", tag);
						}

						Color color = (Color)convertResult.ConvertedValue;
						tokens.Add(new TextToken { TextTokenType = TextTokenType.ColorStart, FontColor = color });
						return separatorString;
					}
					else if (String.Equals("/COLOR", tagNoWs, StringComparison.OrdinalIgnoreCase))
					{
						// color end
						tokens.Add(new TextToken { TextTokenType = TextTokenType.ColorEnd });
						return separatorString;
					}

					return String.Format("[{0}]", tag);
				});

			// replace newline in string
			formattedText = formattedText.Replace("\\n", Environment.NewLine);

			// split the string up on each line
			StringBuilder result = new StringBuilder();
			var splitString = formattedText.Split(new string[] { separatorString }, StringSplitOptions.None);
			int splitIndex = 0;
			int fontBoldCount = 0;
			int fontItalicCount = 0;
			Stack<int> fontSizeStack = new Stack<int>();

			// loop through each split string and apply tokens (embedded views & font styles)
			foreach (var str in splitString)
			{
				int tokenIndex = splitIndex - 1;
				var token = tokenIndex >= 0 && tokenIndex < tokens.Count ? tokens[tokenIndex] : null;
				++splitIndex;

				// do we have a token?
				if (token != null)
				{
					// yes. parse token type
					switch (token.TextTokenType)
					{
					case TextTokenType.BoldStart:
						result.Append("<b>");
						++fontBoldCount;
						break;

					case TextTokenType.BoldEnd:
						result.Append("</b>");
						--fontBoldCount;
						break;

					case TextTokenType.ItalicStart:
						result.Append("<i>");
						++fontItalicCount;
						break;

					case TextTokenType.ItalicEnd:
						result.Append("</i>");
						--fontItalicCount;
						break;

					case TextTokenType.SizeStart:
						result.Append(String.Format("<size={0}>", token.FontSize));
						fontSizeStack.Push(token.FontSize);
						break;

					case TextTokenType.SizeEnd:
						result.Append("</size>");
						fontSizeStack.Pop();
						break;

					case TextTokenType.ColorStart:
						int r = (int)(token.FontColor.r * 255f);
						int g = (int)(token.FontColor.g * 255f);
						int b = (int)(token.FontColor.b * 255f);
						int a = (int)(token.FontColor.a * 255f);
						result.Append(String.Format("<color=#{0}{1}{2}{3}>", r.ToString("X2"), g.ToString("X2"), b.ToString("X2"), a.ToString("X2")));
						break;

					case TextTokenType.ColorEnd:
						result.Append("</color>");
						break;

					case TextTokenType.Unknown:
					default:
						break;
					}
				}

				result.Append(str);
			}

			Debug.Log("--> ParseText:" + result.ToString());
			return result.ToString();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets text anchor.
		/// </summary>
		public TextAnchor TextAnchor
		{
			get
			{
				switch (TextAlignment.Value)
				{
				case ElementAlignment.TopLeft:
					return TextAnchor.UpperLeft;
				case ElementAlignment.Top:
					return TextAnchor.UpperCenter;
				case ElementAlignment.TopRight:
					return TextAnchor.UpperRight;
				case ElementAlignment.Left:
					return TextAnchor.MiddleLeft;
				case ElementAlignment.Right:
					return TextAnchor.MiddleRight;
				case ElementAlignment.BottomLeft:
					return TextAnchor.LowerLeft;
				case ElementAlignment.Bottom:
					return TextAnchor.LowerCenter;
				case ElementAlignment.BottomRight:
					return TextAnchor.LowerRight;
				case ElementAlignment.Center:
				default:
					return TextAnchor.MiddleCenter;
				}
			}
		}

		#endregion
	}
}
