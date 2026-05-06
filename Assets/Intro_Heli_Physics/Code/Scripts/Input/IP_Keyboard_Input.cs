using UnityEngine;

namespace IndiePixel
{
    public class IP_Keyboard_Input : IP_BaseHeli_Input
    {
        #region Handle References

        public Transform CollectiveHandle;
        public Transform ThrottleHandle;
        public Transform PedalIncrease;
        public Transform PedalDecrease;
        public Transform VerticleHandle;
        public Transform HorizontalHandle;

        #endregion

        #region Properties

        private float throttleInput = 0f;
        public float RawThrottleInput => throttleInput;

        protected float stickyThrottleInput = 0f;
        public float StickyThrottleInput => stickyThrottleInput;

        public float collectiveInput = 0f;
        public float CollectiveInput => collectiveInput;

        public float stickyCollectiveInput = 0f;
        public float StickyCollectiveInput => stickyCollectiveInput;

        public Vector2 cyclicInput = Vector2.zero;
        public Vector2 CyclicInput => cyclicInput;

        public float pedalInput = 0f;
        public float PedalInput => pedalInput;

        #endregion

        protected override void HandleInputs()
        {
            base.HandleInputs();

            // DISABLED OVR INPUT
            // HandleThrottleOVR();
            // HandleCollectiveOVR();
            // HandleCyclicOVR();
            // HandlePedalOVR();

            HandleThrottleFromGraphics();
            HandleCollectiveFromGraphics();
            HandlePedalFromGraphics();
            HandleCyclicFromGraphics();

            ClampInputs();
            HandleStickyThrottle();
            HandleStickyCollective();
        }

        #region GRAPHICS → INPUT

        void HandleThrottleFromGraphics()
        {
            float angle = GetNormalizedAngle(ThrottleHandle.localEulerAngles.x);
            float normalized = Mathf.InverseLerp(-45f, 45f, angle);

            throttleInput = normalized;
        }

        void HandleCollectiveFromGraphics()
        {
            float angle = GetNormalizedAngle(CollectiveHandle.localEulerAngles.x);
            float normalized = Mathf.InverseLerp(-45f, 45f, angle);

            collectiveInput = normalized;
        }

        void HandlePedalFromGraphics()
        {
            float incAngle = GetNormalizedAngle(PedalIncrease.localEulerAngles.x);
            float decAngle = GetNormalizedAngle(PedalDecrease.localEulerAngles.x);

            float inc = Mathf.InverseLerp(-45f, 45f, incAngle);
            float dec = Mathf.InverseLerp(-45f, 45f, decAngle);

            pedalInput = inc - dec; // -1 → 1
        }

        void HandleCyclicFromGraphics()
        {
            float verticalAngle = GetNormalizedAngle(VerticleHandle.localEulerAngles.x);
            float horizontalAngle = GetNormalizedAngle(HorizontalHandle.localEulerAngles.z);

            float pitch = Mathf.Clamp(verticalAngle / 10f, -1f, 1f);
            float roll = Mathf.Clamp(-horizontalAngle / 30f, -1f, 1f);

            cyclicInput.y = pitch;
            cyclicInput.x = roll;
        }

        #endregion

        #region Helpers

        float GetNormalizedAngle(float angle)
        {
            if (angle > 180f) angle -= 360f;
            return angle;
        }

        protected void ClampInputs()
        {
            throttleInput = Mathf.Clamp(throttleInput, -1f, 1f);
            collectiveInput = Mathf.Clamp(collectiveInput, -1f, 1f);
            cyclicInput = Vector2.ClampMagnitude(cyclicInput, 1f);
            pedalInput = Mathf.Clamp(pedalInput, -1f, 1f);
        }

        protected void HandleStickyThrottle()
        {
            stickyThrottleInput = Mathf.Clamp01(throttleInput);
        }

        protected void HandleStickyCollective()
        {
            stickyCollectiveInput = Mathf.Clamp01(collectiveInput);
        }

        #endregion
    }
}