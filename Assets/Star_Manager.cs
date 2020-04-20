using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_Manager : MonoBehaviour
{

    public int NumberOfStars;
    public GameObject StarPrefab;

    public Transform StarExtreams;

    TagMananger TM;

    List<GameObject> stars;

    // Start is called before the first frame update
    void Start()
    {
        TM = FindObjectOfType<TagMananger>();
        stars = new List<GameObject>();
    }


    public void SetStars()
    {
        if (stars.Count != 0)
        {
            foreach (GameObject starPut in stars)
                Destroy(starPut);
                stars.Clear();
        }


        for(int i = 0; i < NumberOfStars; i++)
            stars.Add(Instantiate(StarPrefab));

        foreach (GameObject starPut in stars)
            starPut.transform.position = new Vector3(Random.Range(this.transform.position.x, StarExtreams.transform.position.x), Random.Range(this.transform.position.y, StarExtreams.transform.position.y));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
