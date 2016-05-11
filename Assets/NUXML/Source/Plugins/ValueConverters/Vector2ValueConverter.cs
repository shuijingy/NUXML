﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using System.Globalization;
#endregion

namespace NUXML.ValueConverters
{
    /// <summary>
    /// Value converter for Vector2 type.
    /// </summary>
    public class Vector2ValueConverter : ValueConverter
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Vector2ValueConverter()
        {
            _type = typeof(Vector2);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Value converter for Vector2 type.
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
                float[] valueList;
                try
                {
                    valueList = stringValue.Split(',').Select(x => System.Convert.ToSingle(x, CultureInfo.InvariantCulture)).ToArray();
                }
                catch (Exception e)
                {
                    return ConversionFailed(value, e);
                }

                if (valueList.Length == 1)
                {
                    return new ConversionResult(new Vector2(valueList[0], valueList[0]));
                }
                else if (valueList.Length == 2)
                {
                    return new ConversionResult(new Vector2(valueList[0], valueList[1]));
                }
                else
                {
                    return StringConversionFailed(value);
                }
            }

            return ConversionFailed(value);
        }

        #endregion
    }
}