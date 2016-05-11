﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
#endregion

namespace NUXML.ValueConverters
{
    /// <summary>
    /// Value converter for ElementSize type.
    /// </summary>
    public class ElementSizeValueConverter : ValueConverter
    {
        #region Fields

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ElementSizeValueConverter()
        {
            _type = typeof(ElementSize);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Value converter for ElementSize type.
        /// </summary>
        public override ConversionResult Convert(object value, ValueConverterContext context)
        {
            if (value == null)
            {
                return base.Convert(value, context);
            }

            if (value.GetType() == typeof(string))
            {
                var stringValue = (string)value;
                try
                {
                    var convertedValue = ElementSize.Parse(stringValue);
                    return new ConversionResult(convertedValue);
                }
                catch (Exception e)
                {
                    return ConversionFailed(value, e);
                }
            }

            return ConversionFailed(value);
        }

        #endregion
    }
}
