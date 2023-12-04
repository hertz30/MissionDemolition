using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject projectilePrefab;
    public float velocityMult = 10f;
    [Header("Dynamic")]
    public UnityEngine.Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;
    public GameObject launchPoint;
    public GameObject projLinePrefab;
    void Awake(){
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }
    void Update(){
        if(!aimingMode) return;
        UnityEngine.Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        UnityEngine.Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        UnityEngine.Vector3 mouseDelta = mousePos3D-launchPos;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if(mouseDelta.magnitude>maxMagnitude){
            mouseDelta.Normalize();
            mouseDelta*=maxMagnitude;
        }
        UnityEngine.Vector3 projPos = launchPos+mouseDelta;
        projectile.transform.position = projPos;
        if(Input.GetMouseButtonUp(0)){
            aimingMode = false;
            Rigidbody projRB = projectile.GetComponent<Rigidbody>();
            projRB.isKinematic = false;
            projRB.collisionDetectionMode=CollisionDetectionMode.Continuous;
            projRB.velocity = -mouseDelta*velocityMult;
            FollowCam.SWITCH_VIEW(FollowCam.eView.slingshot);
            FollowCam.POI = projectile;
            MissionDemolition.SHOT_FIRED();
            Instantiate<GameObject>(projLinePrefab, projectile.transform);
            projectile=null;
        }

    }
    void OnMouseEnter(){
        launchPoint.SetActive(true);
    }
    void OnMouseDown(){
        aimingMode = true;
        projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }
    void OnMouseExit(){
        launchPoint.SetActive(false);
    }
}
