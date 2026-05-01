using Meta.XR.MRUtilityKit;
using UnityEngine;

/// <summary>
/// After MRUK finishes scanning and detects the bed,
/// this moves an existing object so that:
/// Bed Reference Point == Object Child Reference Point
///
/// No Instantiate is used.
/// Uses MRUK semantic BED label instead of Unity tag.
/// </summary>
public class PlaceObjectOnBed : MonoBehaviour
{
    [Header("Existing Object To Move")]
    public GameObject objectToPlace;

    private void Start()
    {
        Debug.Log("[PlaceObjectOnBed] Start() called.");

        if (MRUK.Instance == null)
        {
            Debug.LogError("[PlaceObjectOnBed] MRUK Instance is NULL.");
            return;
        }

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
    }

    private void OnSceneLoaded()
    {
        Debug.Log("[PlaceObjectOnBed] OnSceneLoaded() triggered.");
        PlaceOnDetectedBed();
    }

    void PlaceOnDetectedBed()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        if (room == null)
        {
            Debug.LogError("[PlaceObjectOnBed] No room found.");
            return;
        }

        Debug.Log("[PlaceObjectOnBed] Room found.");
        Debug.Log("[PlaceObjectOnBed] Total anchors in room: " + room.Anchors.Count);

        MRUKAnchor detectedBed = null;

        Debug.Log("[PlaceObjectOnBed] Searching anchors for BED label...");

        foreach (var anchor in room.Anchors)
        {
            Debug.Log("[PlaceObjectOnBed] Checking Anchor: " + anchor.name);
            //detectedBed = anchor;
            if (anchor.name.Contains("FLOOR"))
            {
                detectedBed = anchor;
                Debug.Log("[PlaceObjectOnBed] BED found: " + anchor.name);
                break;
            }
        }
        //PrintSceneHierarchy();
        if (detectedBed == null)
        {
            Debug.LogWarning("[PlaceObjectOnBed] No BED anchor detected.");
            return;
        }

        if (objectToPlace == null)
        {
            Debug.LogError("[PlaceObjectOnBed] objectToPlace is NULL.");
            return;
        }

        // Bed reference point
        //if (detectedBed.transform.childCount == 0)
        //{
        //    Debug.LogError("[PlaceObjectOnBed] Bed has no child reference point.");
        //    return;
        //}
        objectToPlace.transform.position = detectedBed.transform.position + Vector3.up;

        //COMMENTED FOR TEST
        //Transform bedReference = detectedBed.transform.GetChild(0);
        //Debug.Log("[PlaceObjectOnBed] Bed reference point: " + bedReference.name);

        //// Object reference point
        //if (objectToPlace.transform.childCount == 0)
        //{
        //    Debug.LogError("[PlaceObjectOnBed] Object has no child reference point.");
        //    return;
        //}

        //Transform objectReference = objectToPlace.transform.GetChild(0);
        //Debug.Log("[PlaceObjectOnBed] Object reference point: " + objectReference.name);

        //// Calculate offset between object root and child reference point
        //Vector3 offset = objectToPlace.transform.position - objectReference.position;
        //Debug.Log("[PlaceObjectOnBed] Offset calculated: " + offset);

        //// Move object so child aligns with bed child
        //objectToPlace.transform.position = bedReference.position + offset;
        //Debug.Log("[PlaceObjectOnBed] Object moved to align reference points.");

        //// Match rotation
        //objectToPlace.transform.rotation = detectedBed.transform.rotation;
        //Debug.Log("[PlaceObjectOnBed] Object rotation matched to bed.");
    }

    void PrintSceneHierarchy()
    {
        Debug.Log("========== SCENE HIERARCHY START ==========");

        GameObject[] rootObjects = UnityEngine.SceneManagement.SceneManager
            .GetActiveScene()
            .GetRootGameObjects();

        foreach (GameObject root in rootObjects)
        {
            Debug.Log("[PlaceObjectOnBed]   : "+root.gameObject.name);
            PrintChildrenRecursive(root.transform, 0);
        }

        Debug.Log("========== SCENE HIERARCHY END ==========");
    }

    void PrintChildrenRecursive(Transform current, int level)
    {
        //string indent = new string('-', level * 2);

        //Debug.Log(indent + "> " + current.gameObject.name);


        foreach (Transform child in current)
        {
            Debug.Log("[PlaceObjectOnBed]   : " + child.gameObject.name);

            PrintChildrenRecursive(child, level + 1);
        }
    }
}