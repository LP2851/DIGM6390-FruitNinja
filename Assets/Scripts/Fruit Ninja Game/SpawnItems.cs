using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{

    public float spawnTime=1; //Spawn Time
    public GameObject apple, bomb, orange, bananas, blueApple, grapes, lemon, peach, pear, pineapple, watermelon;
    public float upForce = 750; //Up force
    public float leftRightForce = 200; //Left and right force
    public float maxX = -7; //Max x spawn position
    public float minX = 7; //Min x spawn position
    
    public float sizeDecreaseStep = 0.05f;
    public float minSize = 0.5f;

    
    
    void Start()
    {
        //Start the spawn update
        StartCoroutine("Spawn");
    }
    IEnumerator Spawn()
    {
        int RandomOption;
        RandomOption = Random.Range(1, 12);
        
        //Wait spawnTime
        yield return new WaitForSeconds(spawnTime);
        //Spawn prefab is apple
        GameObject prefab = apple;
        //If random is over 30
        if (Random.Range(0,100) < 30)
        {
            //Spawn prefab is bomb
            //you code here later in task 4
            prefab = bomb;

        }
        
        
        //changes the prefab to a random fruit
        switch (RandomOption)
        {
            case 1:
                prefab = bomb;
                break;

            case 2:
                prefab = apple;
                break;

            case 3:
                prefab = orange;
                break;

            case 4:
                prefab = bananas;
                break;

            case 5:
                prefab = blueApple;
                break;

            case 6:
                prefab = grapes;
                break;

            case 7:
                prefab = lemon;
                break;

            case 8:
                prefab = peach;
                break;

            case 9:
                prefab = pear;
                break;
            case 10:
                prefab = pineapple;
                break;

            case 11:
                prefab = watermelon;
                break;

            default:
                break;
        }
        //Spawn prefab add random position
        GameObject go = Instantiate(prefab, new Vector3(Random.Range(minX
            , maxX + 1), transform.position.y, 0f), Quaternion.Euler(0, 0, Random.Range(-
            90F, 90F)));
        
        if (go.GetComponent<Fruit2D>().canResize)
        {
            float streak = NinjaPlayer.instance.streak;
            float size = (1.0f - ((streak-1) * sizeDecreaseStep));
            size = (size < minSize) ? minSize : size;
            go.transform.localScale = new Vector3(size, size, size);
        }
        
        
        //If x position is over 0 go left
        if (go.transform.position.x > 0)
        {
            go.GetComponent<Rigidbody2D>().AddForce(new Vector2(-
                leftRightForce,upForce));
        }
        //Else go right
        else
        {
            go.GetComponent<Rigidbody2D>().AddForce(new Vector2(leftRightForce,upForce));
        }
        //Start the spawn again
        StartCoroutine("Spawn");
    }
}
