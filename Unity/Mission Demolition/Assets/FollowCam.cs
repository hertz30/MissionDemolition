using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Numerics;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static private FollowCam S;
    static public GameObject POI;
    public enum eView { none, slingshot, castle, both }; 
    [Header("Inscribed")]
    public float easing = 0.05f;
    public UnityEngine.Vector2 minXY = UnityEngine.Vector2.zero;
    public GameObject viewBothGO;
    public GameObject slingshotGO;
    [Header("Dynamic")]
    public float camZ;
    public eView nextView = eView.slingshot;
    void Awake(){
        camZ = this.transform.position.z;
        S=this;
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
    
    public void SwitchView(eView newView){
        if(newView==eView.none){
            newView=nextView;
        }
        switch(newView){
            case eView.slingshot:
                POI=slingshotGO;
                nextView=eView.both;
                break;
            case eView.both:
                POI = viewBothGO;
                nextView = eView.castle;
                break;
            case eView.castle:
                POI=MissionDemolition.GET_CASTLE();
                nextView=eView.slingshot;
                break;
        }
    }
    public void SwitchView(){
        SwitchView(eView.none);
    }
    static public void SWITCH_VIEW(eView newView){
        S.SwitchView(newView);
    }

    }
