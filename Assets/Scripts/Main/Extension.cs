using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LD
{
    public static class Extension
    {
        public static bool RTL = false;
        public static void Extend()
        {
            // JsonExtend.Extend();
        }

        public static void SetText(this Text text, int val)
        {
            text.text = val.ToString();
        }

        public static void SetText(this Text text, float val)
        {
            text.text = val.ToString(CultureInfo.CurrentCulture);
        }
        public static void SetText(this Text text, string val)
        {
            text.text = val;
        }

        public static void SetText(this TMPro.TextMeshProUGUI text, int val)
        {
            text.text = val.ToString();
        }
        public static void SetText(this Text text, long val)
        {
            text.text = val.ToString();
        }
        public static void SetText(this TMPro.TextMeshProUGUI text, float val)
        {
            text.text = val.ToString();
        }

        public static void AddListener(this Button button, UnityAction action)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
        }

        public static void AddListener(this RectTransform_Button_Image_Container btnContainer, UnityAction action)
        {
            btnContainer.button.AddListener(action);
        }

        public static void AddListener(this Toggle toggle, UnityAction<bool> action)
        {
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener(action);
        }
        public static void AddListener(this Dropdown dropdown, UnityAction<int> action)
        {
            dropdown.onValueChanged.RemoveAllListeners();
            dropdown.onValueChanged.AddListener(action);
        }
        public static void AddListener(this InputField inputField, UnityAction<string> action)
        {
            inputField.onValueChanged.RemoveAllListeners();
            inputField.onValueChanged.AddListener(action);
        }
        public static void AddListener(this Slider slider, UnityAction<float> action)
        {
            slider.onValueChanged.RemoveAllListeners();
            slider.onValueChanged.AddListener(action);
        }

    }
}