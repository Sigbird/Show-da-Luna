using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowAquario : MonoBehaviour {
    public GameObject Follow;
    public float Threshold = 3f;
    public float MoveAmount = 2f;

    public float DampTime = 0.2f;
    public float MaxCamVelocity = 2f;   

    public float ScenarioMinX;
    public float ScenarioMaxX;
    public float ScenarioMinY;    
    public float ScenarioMaxY;

    private Camera cam;
    private Vector2 ViewPortCenter = new Vector2(0.5f, 0.5f);
    private Vector2 velocity = Vector2.zero;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();

        Vector2 bottomLeft = cam.ViewportToWorldPoint(Vector2.zero);
        Vector2 topRight = cam.ViewportToWorldPoint(Vector2.one);

        minX = ScenarioMinX - bottomLeft.x;
        maxX = ScenarioMaxX - topRight.x;
        minY = ScenarioMinY - bottomLeft.y;
        maxY = ScenarioMaxY - topRight.y;
    }
       
    void LateUpdate () {              
        Vector2 cameraCenter = cam.ViewportToWorldPoint((Vector3)ViewPortCenter);

        Vector2 newPosSmooth = Vector2.SmoothDamp(cameraCenter, Follow.transform.position,
            ref velocity, DampTime, MaxCamVelocity, Time.deltaTime);

        float xClamp = Mathf.Clamp(newPosSmooth.x, minX, maxX);
        float yClamp = Mathf.Clamp(newPosSmooth.y, minY, maxY);

        transform.position = new Vector3(xClamp, yClamp, transform.position.z);                       
    }       
}
