using UnityEngine;

namespace IndiePixel
{
    public class Heli_Compass : MonoBehaviour
    {
        [Header("References")]
        public Transform helicopter; // your heli transform
        public RectTransform compassBackground; // child 0 (rotating strip)
        public RectTransform pointer; // child 1 (fixed arrow)

        [Header("Settings")]
        public float smoothSpeed = 5f;
        public bool invert = true; // UI usually rotates opposite

        private float currentHeading;

        void Update()
        {
            if (!helicopter) return;

            // Get helicopter yaw (0–360)
            float targetHeading = helicopter.eulerAngles.y;

            // Smooth interpolation (prevents jitter)
            currentHeading = Mathf.LerpAngle(currentHeading, targetHeading, Time.deltaTime * smoothSpeed);

            // Rotate compass
            float displayAngle = invert ? currentHeading : -currentHeading;

            compassBackground.localRotation = Quaternion.Euler(0f, 0f, displayAngle);

            // Pointer stays fixed (usually pointing up)
            pointer.localRotation = Quaternion.identity;
        }
    }
}