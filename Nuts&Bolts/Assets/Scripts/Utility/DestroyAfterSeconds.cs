using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    #region Variables
    public float Lifetime;
    #endregion
  
    void Awake ()
    {
        Destroy(gameObject, Lifetime);
	}	
}
