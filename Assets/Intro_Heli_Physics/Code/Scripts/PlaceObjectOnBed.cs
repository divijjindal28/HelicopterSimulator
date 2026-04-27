using Meta.XR.MRUtilityKit;
using UnityEngine;

/// <summary>
/// After MRUK finishes scanning and detects the bed,
/// this places an object at the CENTER TOP of the detected bed.
/// </summary>
public class PlaceObjectOnBed : MonoBehaviour
{
    [Header("Object To Place")]
    public GameObject objectPrefab;

    [Header("Height Offset Above Bed")]
    public float topOffset = 0.05f;   // 5 cm above bed surface

    private void Start()
    {
        Debug.Log("[PlaceObjectOnBed] Start() called.");

        if (MRUK.Instance == null)
        {
            Debug.LogError("[PlaceObjectOnBed] MRUK Instance is NULL.");
            return;
        }

        Debug.Log("[PlaceObjectOnBed] MRUK Instance found.");

        MRUK.Instance.SceneLoadedEvent.AddListener(OnSceneLoaded);
        Debug.Log("[PlaceObjectOnBed] SceneLoadedEvent listener added.");
    }

    private void OnDestroy()
    {
        Debug.Log("[PlaceObjectOnBed] OnDestroy() called.");

        if (MRUK.Instance != null)
        {
            MRUK.Instance.SceneLoadedEvent.RemoveListener(OnSceneLoaded);
            Debug.Log("[PlaceObjectOnBed] SceneLoadedEvent listener removed.");
        }
        else
        {
            Debug.Log("[PlaceObjectOnBed] MRUK Instance already null during destroy.");
        }
    }

    private void OnSceneLoaded()
    {
        Debug.Log("[PlaceObjectOnBed] OnSceneLoaded() triggered.");
        PlaceOnDetectedBed();
    }

    void PlaceOnDetectedBed()
    {
        Debug.Log("[PlaceObjectOnBed] Searching for detected bed with tag 'Bed'.");

        GameObject detectedBed = GameObject.FindGameObjectWithTag("Bed");

        if (detectedBed == null)
        {
            Debug.LogWarning("[PlaceObjectOnBed] No bed detected.");
            return;
        }

        Debug.Log("[PlaceObjectOnBed] Bed detected: " + detectedBed.name);

        Renderer bedRenderer = detectedBed.GetComponentInChildren<Renderer>();

        if (bedRenderer == null)
        {
            Debug.LogWarning("[PlaceObjectOnBed] Bed renderer not found.");
            return;
        }

        Debug.Log("[PlaceObjectOnBed] Bed renderer found: " + bedRenderer.name);

        Bounds bedBounds = bedRenderer.bounds;

        Debug.Log("[PlaceObjectOnBed] Bed Bounds Center: " + bedBounds.center);
        Debug.Log("[PlaceObjectOnBed] Bed Bounds Max: " + bedBounds.max);
        Debug.Log("[PlaceObjectOnBed] Bed Bounds Size: " + bedBounds.size);

        // Center of bed
        Vector3 spawnPosition = bedBounds.center;
        Debug.Log("[PlaceObjectOnBed] Initial spawn position (center): " + spawnPosition);

        // Move to top surface
        spawnPosition.y = bedBounds.max.y + topOffset;
        Debug.Log("[PlaceObjectOnBed] Final spawn position (top center + offset): " + spawnPosition);

        if (objectPrefab == null)
        {
            Debug.LogError("[PlaceObjectOnBed] objectPrefab is NULL. Cannot instantiate.");
            return;
        }

        Debug.Log("[PlaceObjectOnBed] Instantiating object: " + objectPrefab.name);

        Instantiate(
            objectPrefab,
            spawnPosition,
            Quaternion.identity
        );

        Debug.Log("[PlaceObjectOnBed] Object placed on top center of bed successfully.");
    }
}