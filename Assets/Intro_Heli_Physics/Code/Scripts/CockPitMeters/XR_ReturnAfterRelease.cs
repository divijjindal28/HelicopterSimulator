using UnityEngine;
using Oculus.Interaction;

public class XR_ReturnAfterRelease : MonoBehaviour
{
    public float returnSpeed = 6f;

    private Quaternion initialLocalRotation;
    private Grabbable grabbable;
    private OneGrabRotateTransformer oneGrabRotateTransformer;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();
        oneGrabRotateTransformer = GetComponent<OneGrabRotateTransformer>();
        initialLocalRotation = transform.localRotation;
        Debug.Log("XR_ReturnAfterRelease initialLocalRotation : " + initialLocalRotation);
    }

    void Update()
    {
        // If being grabbed → do nothing (transformer is in control)
        if (grabbable.SelectingPointsCount > 0)
            return;

        // Smooth return after release
        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            initialLocalRotation,
            Time.deltaTime * returnSpeed
        );

        if (Quaternion.Angle(transform.localRotation, initialLocalRotation) < 0.5f)
        {
            transform.localRotation = initialLocalRotation;
            oneGrabRotateTransformer.ResetAngle(); // reset internal angle tracking
        }
        Debug.Log("XR_ReturnAfterRelease currentLocalRotation : " + transform.localRotation);
    }
}