using UnityEngine;
using System.Collections;

public class PlanetaryBodyCollider : MonoBehaviour
{
    // This will be assigned by the PlanetaryBody when the GameObject is created
    public PlanetaryBody planetaryBody;
    private PlanetaryManager mgr;

    void Start()
    {
        // Cache the manager instance for later use in the coroutine
        mgr = Object.FindAnyObjectByType<PlanetaryManager>();
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(1); //wait for 1 second before exploding
        mgr.onPlanetaryBodyExplosion?.Invoke(planetaryBody.mass);
        planetaryBody.Explode();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") // Check if the collided object is the ''Player'' or spaceship
        {
            if (planetaryBody != null)
            {
                StartCoroutine(Explosion());
            }
        }
    }
}
