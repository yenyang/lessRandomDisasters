using System;
using System.Collections.Generic;
using UnityEngine;

namespace LessRandomDisasters
{
    internal static class ModSettings
    {
        private static Dictionary<String, float> DisasterProbabilities;
        private static bool _ignoreUnfinishedDisasters;
        private static int? _ignoreUnfinishedDisastersInt;
        public static Dictionary<String, float> DefaultDisasterProbabilities;
        
        public static bool IgnoreUnfinishedDisasters
        {
            get
            {
                if (_ignoreUnfinishedDisastersInt == null)
                {
                    _ignoreUnfinishedDisastersInt = PlayerPrefs.GetInt("RF_IgnoreUnfinishedDisasters", 1);
                }
                if (_ignoreUnfinishedDisastersInt == 1)
                {
                    _ignoreUnfinishedDisasters = true;
                }
                else
                {
                    _ignoreUnfinishedDisasters = false;
                }
                return _ignoreUnfinishedDisasters;
            }
            set
            {
                if (value == true)
                {
                    _ignoreUnfinishedDisastersInt = 1;
                }
                else
                {
                    _ignoreUnfinishedDisastersInt = 0;
                }
                PlayerPrefs.SetInt("RF_IgnoreUnfinishedDisasters", (int)_ignoreUnfinishedDisastersInt);


            }
        }

        private static bool _showAsPercent;
        private static int? _showAsPercentInt;
        public static bool ShowAsPercent
        {
            get
            {
                if (_showAsPercentInt == null)
                {
                    _showAsPercentInt = PlayerPrefs.GetInt("RF_ShowAsPercent", 1);
                }
                if (_showAsPercentInt == 1)
                {
                    _showAsPercent = true;
                }
                else
                {
                    _showAsPercent = false;
                }
                return _showAsPercent;
            }
            set
            {
                if (value == true)
                {
                    _showAsPercentInt = 1;
                }
                else
                {
                    _showAsPercentInt = 0;
                }
                PlayerPrefs.SetInt("RF_ShowAsPercent", (int)_showAsPercentInt);


            }
        }
        public static float getDisasterProbabilitity(string name)
        {
            return 0;
        }
        public static bool setDisasterPropbability(string name, float value)
        {
            int num = PrefabCollection<DisasterInfo>.PrefabCount();
            
            for (int i = 0; i < num; i++)
            {
                DisasterInfo prefab = PrefabCollection<DisasterInfo>.GetPrefab((uint)i);
                if (prefab != null)
                {
                    if (prefab.name == name)
                    {
                        PlayerPrefs.SetFloat("LRD_" + name, value);
                        prefab.m_finalRandomProbability = (int)value;
                        if (DisasterProbabilities.ContainsKey(name))
                        {
                            DisasterProbabilities[name] = value;
                        } else
                        {
                            DisasterProbabilities.Add(name, value);
                        }
                    }
                }

            }
            return false;
        }
        public static float getTotalProbability()
        {
            float total;
            total = 0;
            foreach (KeyValuePair<string, float> pair in DisasterProbabilities)
            {
                total += pair.Value;
            }
            return total;
        }
        public static void initializeDisasterProbabilities()
        {
            DisasterProbabilities = new Dictionary<string, float>();
            DefaultDisasterProbabilities = new Dictionary<string, float>();
            DefaultDisasterProbabilities.Add("Structure Fire", 0);
            DefaultDisasterProbabilities.Add("Structure Collapse", 0);
            DefaultDisasterProbabilities.Add("Generic Flood", 0);
            DefaultDisasterProbabilities.Add("Meteor Strike", 100);
            DefaultDisasterProbabilities.Add("Tsunami", 100);
            DefaultDisasterProbabilities.Add("Forest Fire", 150);
            DefaultDisasterProbabilities.Add("Earthquake", 300);
            DefaultDisasterProbabilities.Add("Tornado", 50);
            DefaultDisasterProbabilities.Add("Thunderstorm", 200);
            DefaultDisasterProbabilities.Add("Sinkhole", 100);
            DefaultDisasterProbabilities.Add("Chirpynado", 3);
        }
        public static void resetModSettings()
        {
           
            ModSettings.IgnoreUnfinishedDisasters = true;
            ModSettings.ShowAsPercent = true;
           
            foreach (KeyValuePair<string, float> pair in DefaultDisasterProbabilities)
            {
                setDisasterPropbability(pair.Key, DefaultDisasterProbabilities[pair.Key]);
            }


        }
    }
}
