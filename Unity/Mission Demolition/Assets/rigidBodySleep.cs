using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[RequireComponent( typeof(Rigidbody) )] 
public class rigidBodySleep : MonoBehaviour
{
    private int sleepCountdown = 4;
    private Rigidbody rigid;
    void Awake(){
        rigid = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
    if(sleepCountdown>0){
        rigid.Sleep();
        sleepCountdown--;
    }    
    }
}
