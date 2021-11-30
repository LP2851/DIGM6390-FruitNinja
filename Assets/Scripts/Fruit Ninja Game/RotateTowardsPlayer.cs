using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private bool doRotate, doMoveTowards;

    Vector3 tF, fD; // facing, transform towards the player

    void Start() {
        player = GameObject.FindWithTag("Player");
        tF = transform.up;
    }

    void Update() {
        if (doRotate)
        {
            fD = player.transform.position - this.transform.position;
            float angle = Vector2.Angle(tF, fD);
    
            // Debug Stuffs
            Debug.DrawRay(transform.position, tF*2, Color.green);
            Debug.DrawRay(transform.position, fD, Color.red);
    
            Vector3 crossP = Vector3.Cross(tF, fD);
            int clockwise =1;
            if (crossP.z < 0) clockwise = -1;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f,  clockwise * angle);
        }

        // Move Towards- no interia
        if(doMoveTowards)
            this.transform.Translate(transform.up * 2f * Time.deltaTime);
    }
}
