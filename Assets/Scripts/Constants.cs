using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const string InputTypeIndex = "InputTypeIndex";

    public static readonly Dictionary<int, ControlType> controlsDictionary = new()
    {
        {0, ControlType.Swipe},
        {1, ControlType.Drag},
        {2, ControlType.Keyboard},
    };

    public static class ControlsType
    {
        public const string Drag = "Drag";
        public const string Swipe = "Swipe";
        public const string Keyboard = "Keyboard";
    }
}
