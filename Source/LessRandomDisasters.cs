
using ICities;
using System.Collections.Generic;
using ColossalFramework.UI;
using System.Reflection;
using UnityEngine;
using ColossalFramework;
using System;
using LessRandomDisasters.UI;

namespace LessRandomDisasters
{
    public class LessRandomDisastersMod : IUserMod
    {

        public string Name { get { return "Less Random Disasters"; } }
        public string Description { get { return "Allows you to control the chances of each random disaster occuring. By [SSU]yenyang"; } }
        public List<OptionsItemBase> DisasterProbabilitySliders;
        public UICheckBox IgnoreUnfishedDisastersCheckBox;
        public UICheckBox ShowAsPercentCheckBox;
        private List<String> unfinishedDisasters;
        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase generalSettingsGroup = helper.AddGroup("General Settings");
            DisasterProbabilitySliders = new List<OptionsItemBase>();
            unfinishedDisasters = new List<String>();
            unfinishedDisasters.Add("Generic Flood");
            IgnoreUnfishedDisastersCheckBox = generalSettingsGroup.AddCheckbox("Ignore Unfinished Disasters (Restart Required)", ModSettings.IgnoreUnfinishedDisasters, OnIgnoreUnfinishedDisastersCheckBoxChanged) as UICheckBox;
            ShowAsPercentCheckBox = generalSettingsGroup.AddCheckbox("Show as Percent", ModSettings.ShowAsPercent, OnShowAsPercentCheckBoxChanged) as UICheckBox;
            ModSettings.initializeDisasterProbabilities();
            UIHelperBase disasterProbabilitiesGroup = helper.AddGroup("Disaster Probabilities (In Game Only)");

            int num = PrefabCollection<DisasterInfo>.PrefabCount();
            Debug.Log("[LRD]OnSettingsUI " + num);
            for (int i = 0; i < num; i++)
            {
                DisasterInfo prefab = PrefabCollection<DisasterInfo>.GetPrefab((uint)i);
                if (prefab != null)
                {
                    Debug.Log("[LRD]" + prefab.name + " " + prefab.m_finalRandomProbability);

                    if (unfinishedDisasters.Contains(prefab.name) == false || ModSettings.IgnoreUnfinishedDisasters == false)
                    {
                        DisasterProbabilitySliders.Add(new OptionsDisasterProbabilitySlider { value = PlayerPrefs.GetFloat("LRD_" + prefab.name, prefab.m_finalRandomProbability), min = 0f, max = 1000f, step = 1f, uniqueName = prefab.name, readableName = prefab.name });
                        
                        ModSettings.setDisasterPropbability(prefab.name, (int)PlayerPrefs.GetFloat("LRD_" + prefab.name));
                    }
                }

            }
            createDisasterProbabilitySliders(disasterProbabilitiesGroup, DisasterProbabilitySliders);
           
        }



       
        private void createDisasterProbabilitySliders(UIHelperBase helper, List<OptionsItemBase> sliderList)
        {
            foreach (OptionsItemBase slider in sliderList)
            {
                slider.enabled = true;
                slider.Create(helper);
            }
        }

        private void OnIgnoreUnfinishedDisastersCheckBoxChanged(bool val)
        {
            ModSettings.IgnoreUnfinishedDisasters = (bool)val;
        }
        private void OnShowAsPercentCheckBoxChanged(bool val)
        {
            ModSettings.ShowAsPercent = (bool)val;
        }
        private void resetAllSettings()
        {
            ModSettings.resetModSettings();
            
            IgnoreUnfishedDisastersCheckBox.isChecked = ModSettings.IgnoreUnfinishedDisasters;
            ShowAsPercentCheckBox.isChecked = ModSettings.ShowAsPercent;
            
            foreach (OptionsItemBase slider in DisasterProbabilitySliders)
            {
                OptionsDisasterProbabilitySlider sliderDPS = slider as OptionsDisasterProbabilitySlider;
                sliderDPS.slider.value = ;
            }
        }
    }
}



    
 
