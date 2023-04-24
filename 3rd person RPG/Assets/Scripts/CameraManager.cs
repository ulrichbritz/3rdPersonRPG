using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    InputManager inputManager;
    PlayerManager playerManager;

    public Camera playerCam;
    public Transform targetTransform;   //the object the camera follows
    public Transform cameraPivotTransform; //object camera users to pivot
    public Transform cameraTransform; //tranform of actual cam object
    public LayerMask environmentLayer;
    private float defaultPos;

    [Header("Camera Settings")] //these settings can be changed to tweak cam performance
    private float smoothTime = 1f;
    [SerializeField] float leftAndRightRotationSpeed = 220;
    [SerializeField] float upAndDownRotationSpeed = 220;
    [SerializeField] float minPivot = -30;
    [SerializeField] float maxPivot = 60f;
    [SerializeField] float cameraCollisionRaduis = 0.2f; //how much cam jumps off objects it collides with
    public LayerMask collisionLayers; //layers we want cam to collide with



    [Header("Camera Values")]
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraObjectPosition;   //used for camera collisions (moves cam object to this pos)
    [SerializeField] float leftAndRightLookAngle;
    [SerializeField] float upAndDownLookAngle;
    private float defaultCameraZPos; //value used for cam collisions
    private float targetCameraZPos;
    


    public float minCollisionOffset = 0.2f;
    public float cameraCollisionRadius = 0.2f;
    public float cameraFollowSpeed = 0.2f;
    //public float cameraLookSpeed = 2;
    public float cameraPivotSpeed = 2;
    public float lockedPivotPos = 2.25f;        //remove maybe
    public float unlockedPivotPos = 1.65f;      //remove maybe

    public float lookAngle; //look up and down
    public float pivotAngle; //left and right
    [SerializeField] float minPivotAngle = 10f;
    [SerializeField] float maxPivotAngle = 35f;


    public CharacterManager currentLockOnTarget;

    [SerializeField] List<CharacterManager> availableTargets = new List<CharacterManager>();
    public CharacterManager nearestLockOnTarget;
    public CharacterManager leftLockTarget;
    public CharacterManager rightLockTarget;
    public float maxLockOnDistance = 30f;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        playerManager = FindAnyObjectByType<PlayerManager>();
        playerCam = GetComponentInChildren<Camera>();
        cameraTransform = Camera.main.transform;
        defaultPos = cameraTransform.localPosition.z;
    }

    private void Start()
    {
        environmentLayer = LayerMask.NameToLayer("Environment");
        defaultCameraZPos = cameraTransform.transform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        if(targetTransform != null)
        {
            HandleFollowTarget();
            HandleRotateCamera();
            HandleCameraCollisions();
        }
        
    }

    private void HandleFollowTarget()
    {
        Vector3 targetPos = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, smoothTime * Time.deltaTime);
        transform.position = targetPos;
    }

    private void HandleRotateCamera()
    {
        if(inputManager.lockOnFlag == false && currentLockOnTarget == null)
        {

            leftAndRightLookAngle += (inputManager.cameraInputHorizontal * leftAndRightRotationSpeed) * Time.deltaTime;
            upAndDownLookAngle -= (inputManager.cameraInputVertical * upAndDownRotationSpeed) * Time.deltaTime;
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minPivot, maxPivot);

            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;

            //left and right
            cameraRotation.y = leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;

            //up and down
            cameraRotation = Vector3.zero;
            cameraRotation.x = upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            cameraPivotTransform.localRotation = targetRotation;

            /*
            Vector3 rotation;
            Quaternion targetRotation;

            lookAngle = lookAngle + (inputManager.cameraInputHorizontal * cameraLookSpeed);
            pivotAngle = pivotAngle - (inputManager.cameraInputVertical * cameraPivotSpeed);
            pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

            rotation = Vector3.zero;
            rotation.y = lookAngle;
            targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;
            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
            */
        }
        else
        {
            float veclocity = 0;
            Quaternion targetRotation;

            Vector3 direction = currentLockOnTarget.transform.position - transform.position;
            direction.Normalize();
            direction.y = 0;

            targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;

            direction = currentLockOnTarget.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            targetRotation = Quaternion.LookRotation(direction);
            Vector3 eulerAngles = targetRotation.eulerAngles;
            eulerAngles.y = 0;
            cameraPivotTransform.localEulerAngles = eulerAngles;
        }

       
    }

    private void HandleCameraCollisions()
    {
        targetCameraZPos = defaultCameraZPos;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRaduis, direction, out hit, Mathf.Abs(targetCameraZPos), collisionLayers))
        {
            float distanceFromObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetCameraZPos = -(distanceFromObject - cameraCollisionRaduis);
        }

        if(Mathf.Abs(targetCameraZPos) < cameraCollisionRaduis)
        {
            targetCameraZPos = -cameraCollisionRadius;
        }

        cameraObjectPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetCameraZPos, 0.2f);
        cameraTransform.localPosition = cameraObjectPosition;

        /*
        float targetPosition = defaultPos;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();

        if(Physics.SphereCast(cameraPivotTransform.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetPosition = targetPosition - (distance - cameraCollisionOffset);
        }

        if (Mathf.Abs(targetPosition) < minCollisionOffset)
        {
            targetPosition =- minCollisionOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
        */
    }

    public void HandleLockOn()
    {
        availableTargets.Clear();
        float shortestDistanceFromTarget = Mathf.Infinity;
        float shortestDistanceOfLeftTarget = -Mathf.Infinity;
        float shortestDistanceOfRightTarget = Mathf.Infinity;

        Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager enemyCharacterManager = colliders[i].GetComponentInParent<CharacterManager>();

            if(enemyCharacterManager != null)
            {
                Vector3 lockTargetDirection = enemyCharacterManager.transform.position - targetTransform.position;
                float distanceFromTarget = Vector3.Distance(targetTransform.position, enemyCharacterManager.transform.position);
                float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);

                RaycastHit hit;

                if(enemyCharacterManager.transform.root != targetTransform.transform.root && viewableAngle > -180 && viewableAngle < 180 && distanceFromTarget <= maxLockOnDistance)
                {
                    if(Physics.Linecast(playerManager.lockOnTransform.position, enemyCharacterManager.lockOnTransform.transform.position, out hit))
                    {
                        Debug.DrawLine(playerManager.lockOnTransform.position, enemyCharacterManager.lockOnTransform.transform.position);

                        if (hit.transform.gameObject.layer == environmentLayer)
                        {
                            //cannot lock on because something in way
                        }
                        else
                        {
                            availableTargets.Add(enemyCharacterManager);
                        }
                    }               
                }
            }
        }

        for(int n = 0; n < availableTargets.Count; n++)
        {
            float distanceFromTarget = Vector3.Distance(targetTransform.position, availableTargets[n].transform.position);

            if(distanceFromTarget < shortestDistanceFromTarget)
            {
                shortestDistanceFromTarget = distanceFromTarget;

                nearestLockOnTarget = availableTargets[n];
            }

            if (inputManager.lockOnFlag)
            {
                //Vector3 relativeEnemyPos = currentLockOnTarget.transform.InverseTransformPoint(availableTargets[n].transform.position);
                //var distanceFromLeftTarget = currentLockOnTarget.transform.position.x - availableTargets[n].transform.position.x;
                //var distanceFromRightTarget = currentLockOnTarget.transform.position.x + availableTargets[n].transform.position.x;

                Vector3 relativeEnemyPos = inputManager.transform.InverseTransformPoint(availableTargets[n].transform.position);
                var distanceFromLeftTarget = relativeEnemyPos.x;
                var distanceFromRightTarget = relativeEnemyPos.x;

                if (relativeEnemyPos.x <= 0.00 && distanceFromLeftTarget > shortestDistanceOfLeftTarget && availableTargets[n] != currentLockOnTarget)
                {
                    shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                    leftLockTarget = availableTargets[n];
                }              
                else if(relativeEnemyPos.x >= 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget && availableTargets[n] != currentLockOnTarget)
                {
                    shortestDistanceOfRightTarget = distanceFromRightTarget;
                    rightLockTarget = availableTargets[n];
                }
            }
        }

        //if(currentLockOnTarget != null)
            //SetCameraHeight();
    }

    public void ClearLockOnTargets()
    {
        availableTargets.Clear();
        nearestLockOnTarget = null;
        currentLockOnTarget = null;
    }

    private void SetCameraHeight()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 newLockedPos = new Vector3(0, lockedPivotPos);
        Vector3 newUnlockedPos = new Vector3(0, unlockedPivotPos);

        if(currentLockOnTarget != null)
        {
            cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newLockedPos, ref velocity, Time.deltaTime);
        }
        else
        {
            cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newUnlockedPos, ref velocity, Time.deltaTime);
        }
    }

    
}
