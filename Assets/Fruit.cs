using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    Vector2 minPos = new Vector2(-7, -4);
    Vector2 maxPos = new Vector2(7,4);

    [SerializeField]
    Sprite[] fruitSprites;

    [SerializeField]
    GameObject soundPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Eat();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Eat()
    {
        if(fruitSprites != null)
        {
            GetComponent<SpriteRenderer>().sprite = fruitSprites[Random.Range(0, fruitSprites.Length)];
        }
        
        transform.position = new Vector2(Mathf.FloorToInt(Random.Range(minPos.x,maxPos.x)), Mathf.FloorToInt(Random.Range(minPos.y, maxPos.y)));

        Instantiate(soundPrefab);
    }
}
