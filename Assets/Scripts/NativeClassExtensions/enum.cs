using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NativeClassExtensions
{
    public class EnumExtensions
    {
        public static GlobalVariableEnum[] GlobalVariableEnumAsArray()
        {
            return (GlobalVariableEnum[])Enum.GetValues(typeof(GlobalVariableEnum));
        }
    }
}