using UnityEngine;

public class MultipleHandsandLeverPosition : MonoBehaviour
{
    [System.Serializable]
    public class HandSetup
    {
        public Transform hand;
        public Transform handOffset;
        public Transform leverTarget;
    }

    [Header("Hand Setups")]
    public HandSetup leftHand;

    public HandSetup rightHand;

    void LateUpdate()
    {
        UpdateHand(leftHand);
        UpdateHand(rightHand);
    }

    void UpdateHand(HandSetup setup)
    {
        if (setup.hand == null ||
            setup.handOffset == null ||
            setup.leverTarget == null)
            return;

        // Maintain offset correction
        Vector3 positionDifference =
            setup.hand.position - setup.handOffset.position;

        // Match offset point to lever target
        setup.hand.position =
            setup.leverTarget.position + positionDifference;

        //// OPTIONAL ROTATION FOLLOW
        //Quaternion rotationDifference =
        //    setup.hand.rotation *
        //    Quaternion.Inverse(setup.handOffset.rotation);

        //setup.hand.rotation =
        //    setup.leverTarget.rotation * rotationDifference;
    }
}
