using UnityEngine;


namespace IndiePixel {
    
    [RequireComponent(typeof(IP_Input_Controller), typeof(IP_Keyboard_Input))]
    public class IP_Heli_Controller : IP_Base_RBController
    {
        #region Variables
        //[Header("Controller Properties")]
        private IP_Input_Controller input;
        #endregion

        

        #region Custom methods
        protected override void HandlePhysics()
        {
            input = GetComponent<IP_Input_Controller>();
            HandleEngines();
            HandleCharacteristics();
        }

        protected virtual void HandleEngines()
        {
            //To be implemented in derived classes
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

