using ColossalFramework.UI;
using ICities;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LessRandomDisasters.UI
{
    class OptionsDisasterProbabilitySlider : OptionsItemBase
    {
        public float value
        {
            get { return (float)m_value;  }
            set { m_value = value; }
        }
        public float min = 0f;
        public float max = 1000f;
        public float step = 1f;
        public UISlider slider;
        string floatFormat = "F1";
        public override void Create(UIHelperBase helper)
        {
            slider = helper.AddSlider(readableName, min, max, step, value, IgnoredFunction) as UISlider;
            slider.enabled = enabled;
            slider.name = uniqueName;
            slider.width += 150;
            if (ModSettings.ShowAsPercent == false)
            {
                slider.tooltip = value.ToString();
            } else
            {
                slider.tooltip = ((value / ModSettings.getTotalProbability()).ToString(floatFormat) + "%");
            }
            slider.eventValueChanged += new PropertyChangedEventHandler<float>(delegate (UIComponent component, float newValue)
            {
                value = newValue;
                if (ModSettings.ShowAsPercent == false)
                {
                    slider.tooltip = value.ToString();
                }
                else
                {
                    Debug.Log("[LRD]Slider " + value.ToString() + " " + ModSettings.getTotalProbability().ToString());
                    slider.tooltip = ((value / ModSettings.getTotalProbability()*100).ToString(floatFormat) + "%");
                }
                slider.RefreshTooltip();
                ModSettings.setDisasterPropbability(uniqueName, value);
                
            });
            slider.eventClicked += Slider_eventClicked;
        }

        private void Slider_eventClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            if (ModSettings.ShowAsPercent == false)
            {
                slider.tooltip = value.ToString();
            }
            else
            {
                Debug.Log("[LRD]Slider " + value.ToString() + " " + ModSettings.getTotalProbability().ToString());
                slider.tooltip = ((value / ModSettings.getTotalProbability() * 100).ToString(floatFormat) + "%");
            }
            slider.RefreshTooltip();
        }
    }
}
