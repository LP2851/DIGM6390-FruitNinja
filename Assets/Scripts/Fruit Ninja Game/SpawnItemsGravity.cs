using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class SpawnItemsGravity : MonoBehaviour
{
    public float spawnTime=1; //Spawn Time
    public GameObject apple, bomb, orange, bananas, blueApple, grapes, lemon, peach, pear, pinapple, watermelon;
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
        //pick a random fruit
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
                    System.Diagnostics.Debug.WriteLine("bomb");
                    break;

                case 2:
                    prefab = apple;
                    System.Diagnostics.Debug.WriteLine("apple");
                    break;

                case 3:
                    prefab = orange;
                    System.Diagnostics.Debug.WriteLine("orange");
                    break;

                case 4:
                    prefab = bananas;
                    System.Diagnostics.Debug.WriteLine("bananas");
                    break;

                case 5:
                    prefab = blueApple;
                    System.Diagnostics.Debug.WriteLine("blueapple");
                    break;

                case 6:
                    prefab = grapes;
                    System.Diagnostics.Debug.WriteLine("grapes");
                    break;

                case 7:
                    prefab = lemon;
                    System.Diagnostics.Debug.WriteLine("lemon");
                    break;

                case 8:
                    prefab = peach;
                    System.Diagnostics.Debug.WriteLine("peach");
                    break;

                case 9:
                    prefab = pear;
                    System.Diagnostics.Debug.WriteLine("pear");
                    break;
                case 10:
                    prefab = pinapple;
                    System.Diagnostics.Debug.WriteLine("pineapple");
                    break;

                case 11:
                    prefab = watermelon;
                    System.Diagnostics.Debug.WriteLine("watermelon");
                    break;

                default:
                    System.Diagnostics.Debug.WriteLine("nothing");
                    break;
            }


        //Spawn prefab add random position
        GameObject go = Instantiate(prefab,new Vector3(Random.Range(minX
            ,maxX + 1),transform.position.y, 3f),Quaternion.Euler(0,0, Random.Range (-
            90F, 90F))) as GameObject;
        if (go.GetComponent<Fruit2D>().canResize)
        {
            float streak = GameObject.FindWithTag("Player").GetComponent<NinjaPlayer>().streak;
            float size = (1.0f - ((streak-1) * sizeDecreaseStep));
            size = (size < minSize) ? minSize : size;
            go.transform.localScale = new Vector3(size, size, size);

        }
        
        //If x position is over 0 go left
        if (go.transform.position.x > 0)
        {
            go.GetComponent<Gravity>().ApplyForce(new Vector2(-leftRightForce, upForce));
            //go.GetComponent<Rigidbody2D>().AddForce(new Vector2(-
                //leftRightForce,upForce));
        }
        //Else go right
        else
        {
            go.GetComponent<Gravity>().ApplyForce(new Vector2(leftRightForce, upForce));
            //go.GetComponent<Rigidbody2D>().AddForce(new Vector2(leftRightForce,upForce));
        }
        //Start the spawn again
        StartCoroutine("Spawn");
    }
}
