using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    List<GameObject> parts;
    [SerializeField]
    GameObject partPrefab;

    [SerializeField]
    Sprite head;
    [SerializeField]
    Sprite straightBody;
    [SerializeField]
    Sprite turnedBody;
    [SerializeField]
    Sprite tail;

    [SerializeField]
    float tickLength = .6f;
    float time = 0;

    Vector2 direction;

    int startingParts = 4;

    Fruit fruit;

    // Start is called before the first frame update
    void Start()
    {
        parts = new List<GameObject>();
        fruit = FindAnyObjectByType<Fruit>();
        direction = Vector2.right;
        while (startingParts > 0)
        {
            parts.Add(Instantiate(partPrefab, transform));
            startingParts--;
            Tick();
        }

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > tickLength)
        {
            time -= tickLength;
            if (startingParts > 0)
            {
                parts.Add(Instantiate(partPrefab, transform));
                startingParts--;
            }
            Tick();
            
        }

        if (Input.GetKey(KeyCode.UpArrow) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }


    private void Tick()
    {
        if (parts[0].transform.position + (Vector3)direction == fruit.transform.position)
        {
            parts.Add(Instantiate(partPrefab, transform));
            fruit.Eat();
            if (tickLength > 0.05f)
            {
                tickLength -= 0.05f;
            }
        }

        for (int i = parts.Count-1; i > 0; i--)
        {
            parts[i].transform.position = parts[i - 1].transform.position;
        }
        parts[0].transform.position += (Vector3)direction;

        

        if(parts.Count < 3)
        {
            return;
        }

        for(int i = 0; i < parts.Count; i++)
        {
            parts[i].GetComponent<SpriteRenderer>().sprite = DetermineSprite(i);
            parts[i].transform.rotation = Quaternion.Euler(0, 0, DetermineRotation(i));
        }
    }

    private Sprite DetermineSprite(int index)
    {
        if (index == 0)
        {
            return head;
        }
        if (index == parts.Count - 1)
        {
            return tail;
        }
        Vector2 dir = parts[index + 1].transform.position - parts[index - 1].transform.position;
        if (dir.x == 0 || dir.y == 0)
        {
            return straightBody;
        }else
        {
            return turnedBody;
        }
    }

    private float DetermineRotation(int index)
    {
        Vector2 dir;

        if(index == 0)
        {
            dir = parts[index + 1].transform.position - parts[index].transform.position;
            if(dir == Vector2.up)
            {
                return 180;
            }
            if (dir == Vector2.down)
            {
                return 0;
            }
            if (dir == Vector2.right)
            {
                return 90;
            }
            if (dir == Vector2.left)
            {
                return 270;
            }

        }

        if (index == parts.Count -1)
        {
            dir = parts[index].transform.position - parts[index -1].transform.position;
            if (dir == Vector2.up)
            {
                return 180;
            }
            if (dir == Vector2.down)
            {
                return 0;
            }
            if (dir == Vector2.right)
            {
                return 90;
            }
            if (dir == Vector2.left)
            {
                return 270;
            }

        }

        dir = parts[index + 1].transform.position - parts[index - 1].transform.position;
        if (dir.x == 0)
        {
            return 0;
        }else if (dir.y == 0)
        {
            return 90;
        }
        

        Vector2 toNext = parts[index + 1].transform.position - parts[index].transform.position;
        Vector2 toPrevious =  parts[index - 1].transform.position - parts[index].transform.position;

        dir = toNext + toPrevious;

        if(dir.x > 0 && dir.y < 0)
        {
            return 0;
        }
        if (dir.x < 0 && dir.y < 0)
        {
            return 270;
        }
        if (dir.x < 0 && dir.y > 0)
        {
            return 180;
        }
        if (dir.x > 0 && dir.y > 0)
        {
            return 90;
        }

        Debug.LogError("Missing case");
        return -1;

    }
}
