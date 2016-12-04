using ICities;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using LessRandomDisasters.Redirection;
using ColossalFramework;

namespace LessRandomDisasters
{
    public class LoadingFunctions : LoadingExtensionBase
    {
        

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
            AssemblyRedirector.Deploy();
            
        }

        public override void OnLevelLoaded(LoadMode mode)
        {

           
            
            base.OnLevelLoaded(mode);
        }

      

        public override void OnLevelUnloading()
        {
            
            base.OnLevelUnloading();
        }


       public override void OnReleased()
        {
            base.OnReleased();
            AssemblyRedirector.Revert();
        }
    }
}

