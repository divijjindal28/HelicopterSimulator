using UnityEngine;


namespace IndiePixel
{
    public enum InputType
    {
        Keyboard,
        Mobile
    }

    [RequireComponent(typeof(IP_Keyboard_Input))]
    public class IP_Input_Controller : MonoBehaviour
    {
        #region Variables
        [Header("Input Properties")]
        public InputType inputType = InputType.Keyboard;
       
        private IP_Keyboard_Input keyInput;
        private float throttleInput;
        public float ThrottleInput {
            get { return throttleInput; }
        }

        private float stickyThrottleInput;
        public float StickyThrottleInput
        {
            get { return stickyThrottleInput; }
        }

        private float collectiveInput;
        public float CollectiveInput
        {
            get { return collectiveInput; }
        }

        

        private float stickyCollectiveInput;
        public float StickyCollectiveInput
        {
            get { return stickyCollectiveInput; }
        }

        private Vector2 cyclicInput;
        public Vector2 CyclicInput
        {
            get { return cyclicInput; }
        }

        private float pedalInput;
        public float PedalInput
        {
            get { return pedalInput; }
        }
        #endregion

        #region Built In Methods
        private void Start()
        {
            keyInput = GetComponent<IP_Keyboard_Input>();
            if(keyInput)
                SetInputType(inputType);
        }

        void Update()
        {
            switch (inputType) {
                case InputType.Keyboard:
                    throttleInput = keyInput.RawThrottleInput;
                    collectiveInput = keyInput.CollectiveInput;
                    stickyCollectiveInput = keyInput.StickyCollectiveInput;
                    cyclicInput = keyInput.CyclicInput;
                    pedalInput = keyInput.PedalInput;
                    stickyThrottleInput = keyInput.StickyThrottleInput;
                    break;
            }
        }
        #endregion

        #region Custom Methods
        void SetInputType(InputType type) {
            if (type == InputType.Keyboard)
            {
                keyInput.enabled = true;
            }
            else {
                keyInput.enabled = false;
            }
        }
        #endregion

    }
}

