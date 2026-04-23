using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;


namespace IndiePixel {
    
    [RequireComponent(typeof(IP_Input_Controller), typeof(IP_Keyboard_Input))]
    public class IP_Heli_Controller : IP_Base_RBController
    {
        #region Variables
        [Header("Helicopter Properties")]
        public List<IP_Heli_Engine> engines;
        private IP_Input_Controller input;
        [Header("Helicopter Rotors")]
        public IP_Heli_Rotor_Controller rotorCtrl;
        #endregion

        

        #region Custom methods
        protected override void HandlePhysics()
        {
            input = GetComponent<IP_Input_Controller>();
            if (input) {
                HandleEngines();
                HandleCharacteristics();
                HandleRotors();
            }
            
        }

        protected virtual void HandleEngines()
        {
            for(int i = 0; i< engines.Count; i++) {
                engines[i].UpdateEngine(input.StickyThrottleInput);
                float finalPower = engines[i].CurrentHP;
                Debug.Log("IPHC : Engine " + i + " Power: " + finalPower);
            }
            //To be implemented in derived classes
        }

        protected virtual void HandleRotors()
        {
            if(rotorCtrl && engines.Count > 0)
            {
                rotorCtrl.UpdateRotors(input, engines[0].CurrentRPM);
            }
        }   

        protected virtual void HandleCharacteristics()
        {
            //To be implemented in derived classes
        }
        #endregion

        #region Helicopter Control Methods
        #endregion 
    }
}

