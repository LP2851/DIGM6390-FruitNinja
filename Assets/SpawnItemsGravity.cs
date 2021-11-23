using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemsGravity : MonoBehaviour
{
    public float spawnTime=1; //Spawn Time
    public GameObject apple, bomb;
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
        //Wait spawnTime
        yield return new WaitForSeconds(spawnTime);
        //Spawn prefab is apple
        GameObject prefab = apple;
        //If random is over 30
        if (Random.Range(0,100) < 30)
        {
            //Spawn prefab is bomb
            //you code here later in task 4
            //prefab = bomb;

        }
        //Spawn prefab add random position
        GameObject go = Instantiate(prefab,new Vector3(Random.Range(minX
            ,maxX + 1),transform.position.y, 3f),Quaternion.Euler(0,0, Random.Range (-
            90F, 90F))) as GameObject;
        
        float streak = GameObject.FindWithTag("Player").GetComponent<NinjaPlayer>().streak;
        float size = (1.0f - (streak * sizeDecreaseStep));
        size = (size < minSize) ? minSize : size;
        go.transform.localScale = new Vector3(size, size, size);

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
