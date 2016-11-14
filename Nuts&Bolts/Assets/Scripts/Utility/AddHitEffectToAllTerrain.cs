using UnityEngine;

public class AddHitEffectToAllTerrain : MonoBehaviour
{
    #region Fields
    public GameObject ObjectToSpawn;
    #endregion

    // Use this for initialization
    void Awake ()
    {
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            OnHitEffect newInstance = child.gameObject.AddComponent<OnHitEffect>();
            newInstance.DecalToSpawn = ObjectToSpawn;

            if (child.GetComponent<MeshRenderer>() != null)
            {
                child.gameObject.AddComponent<MeshCollider>().convex = true;
            }
        }
	}
}
