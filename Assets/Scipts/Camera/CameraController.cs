using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //camera controller thirld person
    public Transform target;
    public Vector3 offset;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
    public float distance = 10f;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public int yMinLimitAngle = -80;
    public int yMaxLimitAngle = 80;
    public int zoomRate = 40;
    public float zoomDampening = 5.0f;
    public LayerMask collisionLayers = -1;
    public bool lockToRearOfTarget = false;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private float correctedDistance;
    private bool rotateBehind = false;
    private bool mouseSideButton = false;
    private float pbuffer = 0.0f;
    private float yMinLimitAngleDifference;
    private float x = 0.0f;
    private float y = 0.0f;
    private float targetHeight = 0.0f;
    private Rigidbody targetRigidbody;
    private Vector3 preTargetPosition;
    private float preMouseX;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        xDeg = angles.x;
        yDeg = angles.y;
        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;
        targetHeight = target.position.y + offset.y;
        if (target)
            targetRigidbody = target.GetComponent<Rigidbody>();
        
        if (lockToRearOfTarget)
            rotateBehind = true;
        

        yMinLimit += yMinLimitAngle;
        yMaxLimit += yMaxLimitAngle;
        yMinLimitAngleDifference = -yMinLimit + yMaxLimit;
    }

    void LateUpdate()
    {
        if (!target) return;
        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;


        y = ClampAngle(y, yMinLimit, yMaxLimit);
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate *
                           Mathf.Abs(desiredDistance);
        desiredDistance = Mathf.Clamp(desiredDistance, minZoom, maxZoom);
        correctedDistance = desiredDistance;
        Vector3 position = target.position -
                           (rotation * Vector3.forward * desiredDistance + new Vector3(0, -targetHeight, 0));
        RaycastHit collisionHit;
        Vector3 cameraTargetPosition =
            new Vector3(target.position.x, target.position.y + targetHeight, target.position.z);
        bool isCorrected = false;
        if (Physics.Linecast(cameraTargetPosition, position, out collisionHit, collisionLayers))
        {
            correctedDistance = Vector3.Distance(cameraTargetPosition, collisionHit.point) - 0.1f;
            isCorrected = true;
        }

        currentDistance = !isCorrected || correctedDistance > currentDistance
            ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomDampening)
            : correctedDistance;
        position = target.position -
                   (rotation * Vector3.forward * currentDistance + new Vector3(0, -targetHeight, 0));
        preTargetPosition = target.position;
        transform.rotation = rotation;
        transform.position = position;
    }
    
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }


}
