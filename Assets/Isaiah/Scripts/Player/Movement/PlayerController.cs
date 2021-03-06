using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class PlayerController : MonoBehaviour
{
    //Player Camera variables
    public float cameraX;
    public float cameraY;

    public enum CameraDirection { x, z }
    public CameraDirection cameraDirection = CameraDirection.x;
    public float cameraHeight = 20f;
    public float cameraDistance = 7f;
    public Camera playerCamera;
    public GameObject targetIndicatorPrefab;

    //Player Controller variables
    public float speed = 5.0f;
    public float gravity = 14.0f;
    public float maxVelocityChange = 10.0f;
    public AudioSource walk;

    //Private variables
    public Rigidbody r;
    GameObject targetObject;
    public Animator anim;

    //Mouse cursor Camera offset effect
    Vector2 playerPosOnScreen;
    Vector2 cursorPosition;
    Vector2 offsetVector;

    //Plane that represents imaginary floor that will be used to calculate Aim target position
    Plane surfacePlane = new Plane();

    void Awake()
    {
        r = GetComponent<Rigidbody>();
        r.freezeRotation = true;
        r.useGravity = false;

        //Instantiate aim target prefab
        if (targetIndicatorPrefab)
        {
            targetObject = Instantiate(targetIndicatorPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        }

        //Hide the cursor
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        //Setup camera offset
        Vector3 cameraOffset = Vector3.zero;
        if (cameraDirection == CameraDirection.x)
        {
            cameraOffset = new Vector3(cameraDistance, cameraHeight, 0);
        }
        else if (cameraDirection == CameraDirection.z)
        {
            cameraOffset = new Vector3(0, cameraHeight, cameraDistance);
        }

        Vector3 targetVelocity = Vector3.zero;
        //Calculates how fast we should be moving
        if (cameraDirection == CameraDirection.x)
        {
            targetVelocity = new Vector3(Input.GetAxis("Vertical") * (cameraDistance >= 0 ? -1 : 1), 0, Input.GetAxis("Horizontal") * (cameraDistance >= 0 ? 1 : -1));
        }
        else if (cameraDirection == CameraDirection.z)
        {
            targetVelocity = new Vector3(Input.GetAxis("Horizontal") * (cameraDistance >= 0 ? -1 : 1), 0, Input.GetAxis("Vertical") * (cameraDistance >= 0 ? -1 : 1));  
        }
        targetVelocity *= speed;

        anim.SetFloat("Vertical", -targetVelocity.x);
        anim.SetFloat("Horizontal", targetVelocity.z);

        anim.SetFloat("Speed", targetVelocity.sqrMagnitude);

        if (targetVelocity.magnitude > 0.01)
        {
            if (!walk.isPlaying)
            {
                walk.Play();
            }
        }
        else
        {
            walk.Stop();
        }

        //Applies a force that attempts to reach our target velocity
        Vector3 velocity = r.velocity;

        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z *1.5f, -maxVelocityChange *1.5f, maxVelocityChange*1.5f);
        velocityChange.y = 0;

        r.AddForce(velocityChange, ForceMode.VelocityChange);

        //Applies gravity manually for more tuning control
        r.AddForce(new Vector3(0, -gravity * r.mass, 0));

       

        //Mouse cursor offset effect
        playerPosOnScreen = playerCamera.WorldToViewportPoint(transform.position);
        cursorPosition = playerCamera.ScreenToViewportPoint(Input.mousePosition);
        offsetVector = cursorPosition - playerPosOnScreen;

        //Camera follow
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, transform.position + cameraOffset, Time.deltaTime * 7.4f);
        playerCamera.transform.LookAt(transform.position);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraX, cameraY, 0);

        //Aim target position and rotation

        targetObject.transform.position = GetAimTargetPos();
        targetObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
    }

    Vector3 GetAimTargetPos()
    {
        //Update surface plane
        surfacePlane.SetNormalAndPosition(Vector3.up, transform.position);

        //Create a ray from the Mouse click position
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        //Initialise the enter variable
        float enter = 0.0f;

        if (surfacePlane.Raycast(ray, out enter))
        {
            //Get the point that is clicked
            Vector3 hitPoint = ray.GetPoint(enter);

            //Move your cube GameObject to the point where you clicked
            return hitPoint;
        }

        //No raycast hit, hide the aim target by moving it far away
        return new Vector3(-5000, -5000, -5000);
    }
}