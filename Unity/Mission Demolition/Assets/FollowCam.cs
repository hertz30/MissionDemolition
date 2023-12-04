using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;
    [Header("Inscribed")]
    public float easing = 0.05f;
    public UnityEngine.Vector2 minXY = UnityEngine.Vector2.zero;
    [Header("Dynamic")]
    public float camZ;
    void Awake(){
        camZ = this.transform.position.z;
    }
    // Start is called before the first frame update
    void FixedUpdate() {
     UnityEngine.Vector3 destination = UnityEngine.Vector3.zero;
     if(POI!=null){
        Rigidbody poiRigid = POI.GetComponent<Rigidbody>();
        if((poiRigid!=null)&&poiRigid.IsSleeping()){
            POI=null;
        }
        if(POI!=null){
            destination=POI.transform.position;
        }
     destination.x = Mathf.Max(minXY.x, destination.x);
     destination.y = Mathf.Max(minXY.y, destination.y);
     destination.z = camZ;
     transform.position = destination;
     Camera.main.orthographicSize = destination.y + 10;
     destination = UnityEngine.Vector3.Lerp(transform.position, destination,
easing);
        }
    }
        
    }
