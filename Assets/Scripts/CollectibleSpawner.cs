using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public Vector3 areaSize = new Vector3(9, 0, 9);
    public float interval = 1.5f;
    public Material matRed, matBlue, matGreen;

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnOne();
            timer = interval;
        }
    }

    void SpawnOne()
    {
        Vector3 pos = new Vector3(
            Random.Range(-areaSize.x, areaSize.x),
            0.5f,
            Random.Range(-areaSize.z, areaSize.z)
        );

        GameObject obj = Instantiate(collectiblePrefab, pos, Quaternion.identity);

        var rend = obj.GetComponent<Renderer>();
        var collectible = obj.GetComponent<Collectible>();

        int r = Random.Range(0, 3);
        if (r == 0)
        {
            rend.material = matRed;
            collectible.color = CollectibleColor.Red;   
        }
        else if (r == 1)
        {
            rend.material = matBlue;
            collectible.color = CollectibleColor.Blue;  
        }
        else
        {
            rend.material = matGreen;
            collectible.color = CollectibleColor.Green; 
        }
    }
}
