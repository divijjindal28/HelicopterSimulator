using UnityEngine;


namespace IndiePixel
{
    public class IP_Keyboard_Input : IP_BaseHeli_Input
    {
        #region Variables
        
        #endregion

        #region Properties
        private float throttleInput = 0f;
        public float RawThrottleInput
        {
            get { return throttleInput; }
        }

        protected float stickyThrottleInput = 0f;
        public float StickyThrottleInput
        {
            get { return stickyThrottleInput; }
        }

        public float collectiveInput = 0f;
        public float CollectiveInput
        {
            get { return collectiveInput; }
        }

        public float stickyCollectiveInput = 0f;
        public float StickyCollectiveInput
        {
            get { return stickyCollectiveInput; }
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
            //HandleThrottle();
            //HandlePedal();
            //HandleCollective();
            //HandleCyclic();

            HandleThrottleOVR();
            HandleCollectiveOVR();
            HandleCyclicOVR();
            HandlePedalOVR();

            ClampInputs();
            HandleStickyThrottle();
            HandleStickyCollective();
        }

        void HandleThrottle() {
            throttleInput = Input.GetAxis("Throttle");
        }

        void HandleThrottleOVR()
        {
            try
            {
                float throttleSpeed = .4f;   // change speed here
                Debug.Log("IP_Keyboard_Input HandleThrottleOVR Started");

                if (OVRInput.Get(OVRInput.Button.One))
                {
                    throttleInput += throttleSpeed * Time.deltaTime;
                    Debug.Log("IP_Keyboard_Input HandleThrottleOVR A Pressed -> Throttle Increasing");
                }

                if (OVRInput.Get(OVRInput.Button.Two))
                {
                    throttleInput -= throttleSpeed * Time.deltaTime;
                    Debug.Log("IP_Keyboard_Input HandleThrottleOVR  B Pressed -> Throttle Decreasing");
                }
                
                throttleInput = Mathf.Clamp(throttleInput, -1f, 1f);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("IP_Keyboard_Input HandleThrottleOVR ERROR in HandleThrottleOVR: " + ex.Message);
            }
        }

        void HandlePedal() {
            pedalInput = Input.GetAxis("Pedal");
        }

        void HandlePedalOVR()
        {
            try
            {
                Debug.Log("IP_Keyboard_Input HandlePedalOVR Started");

                // Read right thumbstick horizontal axis
                float horizontalInput =
                    OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;

                // Optional: use right controller stick instead
                // float horizontalInput =
                //     OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;

                pedalInput = horizontalInput;

                pedalInput = Mathf.Clamp(pedalInput, -1f, 1f);

                Debug.Log("IP_Keyboard_Input HandlePedalOVR Pedal Input: " + pedalInput);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("IP_Keyboard_Input HandlePedalOVR ERROR: " + ex.Message);
            }
        }

        void HandleCollective() {
            collectiveInput = Input.GetAxis("Collective");
        }

        void HandleCollectiveOVR()
        {
            try
            {
                Debug.Log("IP_Keyboard_Input HandleCollectiveOVR Started");
                // Read right thumbstick vertical axis
                float verticalInput =
                    OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;
                Debug.Log("IP_Keyboard_Input HandleCollectiveOVR : "+verticalInput);
                // Optional: use right controller stick instead
                // float verticalInput =
                //     OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y;

                
                
                collectiveInput = verticalInput;
                collectiveInput = Mathf.Clamp(collectiveInput, -1f, 1f);
                Debug.Log("IP_Keyboard_Input HandleCollectiveOVR Collective Input: " + collectiveInput);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("IP_Keyboard_Input HandleCollectiveOVR ERROR: " + ex.Message);
            }
        }

        void HandleCyclic() {

            cyclicInput.y = vertical;
            cyclicInput.x = horizontal;
        }
        void HandleCyclicOVR()
        {
            try
            {
                Debug.Log("IP_Keyboard_Input HandleCyclic Started");

                // Keyboard fallback
                cyclicInput.y = vertical;
                cyclicInput.x = horizontal;

                // Left controller joystick
                Vector2 leftStick =
                    OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

                // Override with left stick if moved
                if (leftStick.magnitude > 0.05f)
                {
                    cyclicInput.x = leftStick.x;   // roll left/right
                    cyclicInput.y = leftStick.y;   // pitch forward/back
                }

                cyclicInput = Vector2.ClampMagnitude(cyclicInput, 1f);

                Debug.Log("IP_Keyboard_Input HandleCyclic X: " + cyclicInput.x +
                          " Y: " + cyclicInput.y);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("IP_Keyboard_Input HandleCyclic ERROR: " + ex.Message);
            }
        }

        protected void ClampInputs()
        {
            throttleInput = Mathf.Clamp(throttleInput, -1f, 1f);
            collectiveInput = Mathf.Clamp(collectiveInput, -1f, 1f);
            cyclicInput = Vector2.ClampMagnitude(cyclicInput, 1f);
            pedalInput = Mathf.Clamp(pedalInput, -1f, 1f);

        }

        protected void HandleStickyThrottle() {
            stickyThrottleInput += RawThrottleInput * Time.deltaTime;
            stickyThrottleInput = Mathf.Clamp(stickyThrottleInput, 0f, 1f);
            Debug.Log("Sticky Throttle Input: " + stickyThrottleInput);
        }

        protected void HandleStickyCollective() {

            if (Mathf.Abs(collectiveInput) > 0.3f) {
                stickyCollectiveInput += -collectiveInput * Time.deltaTime * .3f;
            }
            //stickyCollectiveInput += -collectiveInput * Time.deltaTime * .3f;
            
            stickyCollectiveInput = Mathf.Clamp(stickyCollectiveInput, 0f, 1f);
            if (OVRInput.Get(OVRInput.Button.Three))
            {
                stickyCollectiveInput = 0.6f;
            }
            Debug.Log("Sticky Collective Input: " + stickyCollectiveInput + "   "+ collectiveInput);
        }
        #endregion
    }


}
