using UnityEngine;

public class HandAndLeverPositionMatch : MonoBehaviour
{
    [Header("Main hand root")]
    public Transform hand;

    [Header("Offset point inside the hand")]
    public Transform handOffset;

    [Header("Target lever")]
    public Transform lever;


    //public Animator handAnimator;
    //public Animator leverAnimator;

    //private bool triggered = false;

    //void Update()
    //{
    //    if (!triggered)
    //    {
    //        triggered = true;

    //        // Trigger BOTH in same frame
    //        handAnimator.Play("LeftHandANimation-Lever", 0, 0f);
    //        leverAnimator.Play("Instructing", 0, 0f);
    //    }
    //}

    void LateUpdate()
    {
        if (hand == null || handOffset == null || lever == null)
            return;

        //// Rotation difference from offset to hand
        //Quaternion rotationDifference =
        //    hand.rotation * Quaternion.Inverse(handOffset.rotation);

        //// Apply corrected hand rotation
        //hand.rotation = lever.rotation * rotationDifference;

        // Position correction
        Vector3 positionDifference =
            hand.position - handOffset.position;

        // Apply corrected hand position
        hand.position = lever.position + positionDifference;
    }
}
