using ICities;
using UnityEngine;

using ColossalFramework;

using LessRandomDisasters.Redirection.Attributes;

namespace LessRandomDisasters
{

    [TargetType(typeof(DisasterManager))]
    public class DisasterManagerDetour : DisasterManager
    {
        [RedirectMethod]
        private DisasterInfo FindRandomDisasterInfo()
        {
            UnlockManager instance = Singleton<UnlockManager>.instance;
            int num = PrefabCollection<DisasterInfo>.PrefabCount();
            int num2 = 0;
            for (int i = 0; i < num; i++)
            {
                DisasterInfo prefab = PrefabCollection<DisasterInfo>.GetPrefab((uint)i);
                if (prefab != null && instance.Unlocked(prefab.m_UnlockMilestone))
                {
                    num2 += prefab.m_finalRandomProbability;
                }
                Debug.Log("LRD"+prefab.name + " " + prefab.m_finalRandomProbability);
            }
            if (num2 == 0)
            {
                return null;
            }
            num2 = Singleton<SimulationManager>.instance.m_randomizer.Int32((uint)num2);
            for (int j = 0; j < num; j++)
            {
                DisasterInfo prefab2 = PrefabCollection<DisasterInfo>.GetPrefab((uint)j);
                if (prefab2 != null && instance.Unlocked(prefab2.m_UnlockMilestone))
                {
                    if (num2 < prefab2.m_finalRandomProbability)
                    {
                        return prefab2;
                    }
                    num2 -= prefab2.m_finalRandomProbability;
                }
            }
            return null;
        }

    }

}
