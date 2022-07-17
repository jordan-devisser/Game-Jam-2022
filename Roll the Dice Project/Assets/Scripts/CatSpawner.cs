using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    [SerializeField] GameObject catPrefab;
    [SerializeField] Sprite[] catSprites;

    List<GameObject> catsRendered = new List<GameObject>();

    // NEEDS:
    // - GenerateCats(left bound x, right bound x) 
    //          - Creates four rows of cats between the given values ( with some extra cats either side?)
    //          - Picks a cat sprite at random
    // - DestroyCatsBelowRow(row) - Loop through array of existing cat objects, destroying those below a certain row

    public void GenerateCats(float x, float y)
    {
        // select random number of cats to create
        int numOfCats = Random.Range(1, 4);

        for (int i = 0; i < numOfCats; i++)
        {
            for (float j = 0.2f; j <= 1.0f; j += 0.2f)
            {
                /** select a random scale
                float randomScale = Random.Range(0.5f, 4f);

                catPrefab.GetComponent<Transform>().localScale = new Vector3(randomScale, randomScale, 0);
                **/
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
