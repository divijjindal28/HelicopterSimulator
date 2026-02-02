using UnityEngine;


namespace IndiePixel
{
    public class IP_Keyboard_Input : IP_BaseHeli_Input
    {
        #region Variables
        
        #endregion

        #region Properties
        private float throttleInput = 0f;
        public float ThrottleInput
        {
            get { return throttleInput; }
        }

        public float collectiveInput = 0f;
        public float CollectiveInput
        {
            get { return collectiveInput; }
        }
        public Vector2 cyclicInput = Vector2.zero;
        public Vector2 CyclicInput
        {
            get { return cyclicInput; }
        }
        public float pedalInput = 0f;
        public float PedalInput
        {
            get { return pedalInput; }
        }
        #endregion

        #region BuiltInMethods
        #endregion
        #region CustomMethods
        protected override void HandleInputs()
        {
            base.HandleInputs();
            HandleThrottle();
            HandlePedal();
            HandleCollective();
            HandleCyclic();

            ClampInputs();
        }

        void HandleThrottle() {
            throttleInput = Input.GetAxis("Throttle");
        }

        void HandlePedal() {
            pedalInput = Input.GetAxis("Pedal");
        }

        void HandleCollective() {
            collectiveInput = Input.GetAxis("Collective");
        }

        void HandleCyclic() {
            cyclicInput.y = vertical;
            cyclicInput.x = horizontal;
        }

        protected void ClampInputs()
        {
            throttleInput = Mathf.Clamp(throttleInput, -1f, 1f);
            collectiveInput = Mathf.Clamp(collectiveInput, -1f, 1f);
            cyclicInput = Vector2.ClampMagnitude(cyclicInput, 1f);
            pedalInput = Mathf.Clamp(pedalInput, -1f, 1f);

        }
        #endregion
    }


}
