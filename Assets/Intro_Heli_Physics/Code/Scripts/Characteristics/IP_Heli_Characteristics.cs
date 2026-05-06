using UnityEngine;

namespace IndiePixel
{
    public class IP_Heli_Characteristics : MonoBehaviour
    {
        [Header("Lift Properties")]
        public float maxLiftForce = 10f;
        public IP_Heli_MainRotor mainRotor;

        [Header("Tail Rotor Properties")]
        public float tailForce = 2f;

        [Header("Cyclic Properties")]
        public float cyclicPowerForce = 2f;
        public float cyclicForceMultiplier = 1000f;

        [Header("AutoLevel Properties")]
        public float autoLevelForce = 2f;

        private Vector3 flatFow;
        private float ForwardDot;
        private Vector3 flatRight;
        private float RightDot;

        [Header("Helicopter Controls")]
        public GameObject VerticleHandle;
        public GameObject HorizontalHandle;

        public GameObject CollectiveHandle;
        public GameObject ThrottleHandle;
        public GameObject PedalIncrease;
        public GameObject PedalDecrease; 
        public float speedMPH;

        public void UpdateCharacteristics(Rigidbody rb, IP_Input_Controller input)
        {
            HandleLift(rb, input);
            HandleCyclic(rb, input);
            HandlePedals(rb, input);
            CalculateAngles();
            AutoLevel(rb);
            //HandleHeliThrottleGraphics(input);

            float speedMps = rb.linearVelocity.magnitude;
            speedMPH = speedMps * 2.23694f;
        }

        protected virtual void HandleLift2(Rigidbody rb, IP_Input_Controller input) {

            //Debug.Log("Handling Lift");
            if (mainRotor) {
                Vector3 minLiftForce = (transform.up * (Physics.gravity.magnitude - .3f) * rb.mass) ;
                Vector3 liftForce = transform.up * (Physics.gravity.magnitude + maxLiftForce) * rb.mass;
                //Debug.Log("IP_Heli_Characteristics LiftForce : "+ liftForce);
                float normalzedRPM = mainRotor.CurrentRPMs / 500;
                //Debug.Log("IP_Heli_Characteristics normalizedRPM : " + normalzedRPM);
                Vector3 finalLiftForce = liftForce * normalzedRPM * input.StickyCollectiveInput;
                //finalLiftForce.y = Mathf.Clamp(finalLiftForce.y, minLiftForce.y, Mathf.Infinity);

                finalLiftForce.y = Mathf.Max(finalLiftForce.y, minLiftForce.y);
                rb.AddForce(finalLiftForce, ForceMode.Force);
                //rb.AddForce(liftForce);
                Debug.Log("IP_Heli_Characteristics : " + transform.up * (Physics.gravity.magnitude) * rb.mass + "  " + liftForce + "  " + normalzedRPM + "    " + input.StickyCollectiveInput + "    " + finalLiftForce);

            }

        }


        protected virtual void HandleLift(Rigidbody rb, IP_Input_Controller input)
        {
            if (mainRotor)
            {
                Vector3 minLiftForce =
                    transform.up * (Physics.gravity.magnitude - 0.3f) * rb.mass;

                Vector3 maxLiftForceVector =
                    transform.up * (Physics.gravity.magnitude + maxLiftForce) * rb.mass;

                float normalzedRPM = mainRotor.CurrentRPMs / 500f;

                float liftPercent =
                    normalzedRPM * input.StickyCollectiveInput;

                liftPercent = Mathf.Clamp01(liftPercent);

                Vector3 finalLiftForce =
                    Vector3.Lerp(minLiftForce, maxLiftForceVector, liftPercent);

                

                rb.AddForce(finalLiftForce, ForceMode.Force);
                Debug.Log("IP_Heli_Characteristics : " + "  " + transform.up * (Physics.gravity.magnitude) * rb.mass + minLiftForce + "  " + maxLiftForceVector + "  " + normalzedRPM + "    " + input.StickyCollectiveInput + "    " + finalLiftForce);

                //HandleHeliCollectiveGraphics(input.StickyCollectiveInput);
            }
        }

        void HandleHeliCollectiveGraphics(float stickyCollectiveInput)
        {

            // Convert 0→1 into -45→+45
            float collectiveAngle = Mathf.Lerp(-45f, 45f, stickyCollectiveInput);

            // Target rotation (X-axis only)
            Quaternion targetRotation = Quaternion.Euler(collectiveAngle, 0f, 0f);

            // Smooth interpolation (same feel as cyclic)
            CollectiveHandle.transform.localRotation = Quaternion.Lerp(
                CollectiveHandle.transform.localRotation,
                targetRotation,
                Time.deltaTime * 8f
            );
        }

        void HandleHeliThrottleGraphics(IP_Input_Controller input)
        {

            // Convert 0→1 into -45→+45
            float stickyAngle = Mathf.Lerp(-45f, 45f, input.StickyThrottleInput);

            // Target rotation (X-axis only)
            Quaternion targetRotation = Quaternion.Euler(stickyAngle, 0f, 0f);

            // Smooth interpolation (same feel as cyclic)
            ThrottleHandle.transform.localRotation = Quaternion.Lerp(
                ThrottleHandle.transform.localRotation,
                targetRotation,
                Time.deltaTime * 8f
            );
        }

        public virtual void HandleCyclic(Rigidbody rb, IP_Input_Controller input) {
            //Debug.Log("Handling Cyclic");
            float cyclicZForce = input.CyclicInput.x * cyclicPowerForce;
            rb.AddRelativeTorque(Vector3.forward * cyclicZForce, ForceMode.Acceleration);

            float cyclicXForce = -input.CyclicInput.y * cyclicPowerForce;
            rb.AddRelativeTorque(Vector3.right * cyclicXForce, ForceMode.Acceleration);

            Vector3 forwardVec = flatFow * ForwardDot;
            Vector3 rightVec = flatRight * RightDot;
            Vector3 FinalCyclicVector = Vector3.ClampMagnitude(forwardVec + rightVec, 1f)* cyclicPowerForce/10 * cyclicForceMultiplier;
            rb.AddForce(FinalCyclicVector, ForceMode.Force);
            //HandleHeliCyclicGraphics(input.CyclicInput);
        }

        void HandleHeliCyclicGraphics(Vector2 CyclicInput)
        {
            // Clamp input to [-1, 1]
            float vertticalCyclic = Mathf.Clamp(CyclicInput.y, -1f, 1f);     // pitch
            float horizontalCyclic = - Mathf.Clamp(CyclicInput.x, -1f, 1f);    // roll

            // --- Vertical Handle (X axis: -10 to +10) ---
            float verticalAngle = vertticalCyclic * 10f;

            // --- Horizontal Handle (Z axis: -30 to +30) ---
            float horizontalAngle = horizontalCyclic * 30f;

            // Target rotations
            Quaternion targetVertical = Quaternion.Euler(verticalAngle, 0f, 0f);
            Quaternion targetHorizontal = Quaternion.Euler(0f, 0f, horizontalAngle);

            // Smooth interpolation (better for XR feel)
            VerticleHandle.transform.localRotation = Quaternion.Lerp(
                VerticleHandle.transform.localRotation,
                targetVertical,
                Time.deltaTime * 8f
            );

            HorizontalHandle.transform.localRotation = Quaternion.Lerp(
                HorizontalHandle.transform.localRotation,
                targetHorizontal,
                Time.deltaTime * 8f
            );
        }

        public virtual void HandlePedals(Rigidbody rb, IP_Input_Controller input)
        {
            rb.AddTorque(Vector3.up * tailForce * input.PedalInput, ForceMode.Acceleration);
            Debug.Log("Handling Pedals : " + input.PedalInput);
            //HandleHeliPedalGraphics(input.PedalInput);
        }

        void HandleHeliPedalGraphics(float pedalInput)
        {
            // Clamp input [-1, 1]
            float clampedInput = Mathf.Clamp(pedalInput, -1f, 1f);

            // --- POSITIVE PEDAL (0 → 1) ---
            float positiveInput = Mathf.Clamp01(clampedInput); // only 0 to 1

            float increaseAngle = Mathf.Lerp(-45f, 45f, positiveInput);

            Quaternion increaseTarget = Quaternion.Euler(increaseAngle, 0f, 0f);

            PedalIncrease.transform.localRotation = Quaternion.Lerp(
                PedalIncrease.transform.localRotation,
                increaseTarget,
                Time.deltaTime * 8f
            );


            // --- NEGATIVE PEDAL (0 → -1) ---
            float negativeInput = Mathf.Clamp01(-clampedInput); // convert -1→0 into 1→0

            float decreaseAngle = Mathf.Lerp(-45f, 45f, negativeInput);

            Quaternion decreaseTarget = Quaternion.Euler(decreaseAngle, 0f, 0f);

            PedalDecrease.transform.localRotation = Quaternion.Lerp(
                PedalDecrease.transform.localRotation,
                decreaseTarget,
                Time.deltaTime * 8f
            );
        }

        private void CalculateAngles() {
            //To be implemented in derived classes
            flatFow = transform.forward;
            flatFow.y = 0f;
            flatFow = flatFow.normalized;
            flatRight = transform.right;
            flatRight.y = 0f;
            flatRight = flatRight.normalized;

            ForwardDot = Vector3.Dot(transform.up, flatFow);
            RightDot = Vector3.Dot(transform.up, flatRight);
        }

        private void AutoLevel(Rigidbody rb) {
            //To be implemented in derived classes

            float rightForce = -ForwardDot * autoLevelForce;
            float forwardForce = RightDot * autoLevelForce;
            rb.AddRelativeTorque(Vector3.forward * forwardForce, ForceMode.Acceleration);
            rb.AddRelativeTorque(Vector3.right * rightForce, ForceMode.Acceleration);
        }
    }
}
