using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    [SerializeField] GameObject catPrefab;
    [SerializeField] Sprite[] catSprites;

    List<GameObject> catsRendered = new List<GameObject>();

    public void GenerateCats(float x, float y)
    {
        // select random number of cats to create
        int numOfCats = Random.Range(1, 4);

        for (int i = 0; i < numOfCats; i++)
        {
            for (float j = 0.2f; j <= 1.0f; j += 0.2f)
            {
                // select new cat at random
                catPrefab.GetComponent<SpriteRenderer>().sprite = catSprites[Random.Range(0, catSprites.Length)];

                GameObject cat = Instantiate(catPrefab, new Vector3(x + j, y + j, 0), Quaternion.identity);

                Destroy(cat, 10);
            }
        }
    }

    public IEnumerator DestroyCatsBelowRow(GameObject cat)
    {
        Destroy(cat);
        yield return new WaitForSeconds(5);
    }
}
