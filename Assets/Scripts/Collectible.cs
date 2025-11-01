using UnityEngine;

public enum CollectibleColor { Red, Blue, Green }

public class Collectible : MonoBehaviour
{
    public CollectibleColor color;
    public GameObject pickupEffectPrefab; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            SpawnPickupEffect();

            
            GameManager.Instance.HandleCollectiblePickup(this);

            
            Destroy(gameObject);
        }
    }

    void SpawnPickupEffect()
    {
        if (pickupEffectPrefab != null)
        {
            
            GameObject fx = Instantiate(pickupEffectPrefab, transform.position, Quaternion.identity);

            
            var ps = fx.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var main = ps.main;
                if (color == CollectibleColor.Red)
                    main.startColor = Color.red;
                else if (color == CollectibleColor.Blue)
                    main.startColor = Color.blue;
                else
                    main.startColor = Color.green;
            }

            
            Destroy(fx, 1.5f);
        }
    }
}
