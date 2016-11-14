using UnityEngine;

public class OnHitEffect : MonoBehaviour
{
    #region Variables
    public GameObject DecalToSpawn; // Decal
    #endregion

    // Creates effects
    void Damage (GunHit gunHit)
    {
       GameObject decal = Instantiate(DecalToSpawn, gunHit.Hit.point, Quaternion.LookRotation(-gunHit.Hit.normal)) as GameObject;
       decal.transform.position -= decal.transform.forward * 0.02f;
    }
}
