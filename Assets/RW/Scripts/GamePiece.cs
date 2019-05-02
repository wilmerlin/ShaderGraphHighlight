using UnityEngine;


public class GamePiece : MonoBehaviour
{
    // difference between this Transform and mouse down position
    private Vector3 mouseDownOffset;

    // screen z
    private float zDepth;

    // rotation degrees per second
    [SerializeField]
    private float rotationSpeed = 720;

    // our desired rotation
    private Quaternion targetRotation;

    // is the GamePiece currently rotating?
    private bool isRotating = false;

    // is the GamePiece currently being moved?
    private bool isActive = false;

    // default y value 
    private const float yValue = 0.014f;

    private void OnMouseDown()
    {
        // calculate screen Z value for this GameObject
        zDepth = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // store the offset between the mouse down position and the GameObject position
        mouseDownOffset = gameObject.transform.position - MouseToWorldPoint();
    }

    // return the mouse position as a world space position
    private Vector3 MouseToWorldPoint()
    {
        // mouse screen point
        Vector3 mouseScreenPoint = Input.mousePosition;

        // use the screen Z calculated on mouse down
        mouseScreenPoint.z = zDepth;

        // convert to world space
        return Camera.main.ScreenToWorldPoint(mouseScreenPoint);
    }

    // drag the GameObject along the xz plane
    void OnMouseDrag()
    {

        // move object with mouse
        transform.position = mouseDownOffset + MouseToWorldPoint();

        // keep GamePiece on xz plane 
        transform.position = new Vector3(transform.position.x, yValue, transform.position.z);
    }

    // set a target rotation of 15 degrees clockwise
    public void RotateCounterClockwise()
    {
        isRotating = true;

        float newY = Mathf.RoundToInt(transform.rotation.eulerAngles.y - 15f);
        targetRotation = Quaternion.Euler(0f, newY, 0f);
    }

    // set a target rotation of 15 degress counter clockwise
    public void RotateClockwise()
    {
        isRotating = true;

        float newY = Mathf.RoundToInt(transform.rotation.eulerAngles.y + 15f);
        targetRotation = Quaternion.Euler(0f, newY, 0f);
    }

    // rotate the GamePiece to its target rotation
    void RotateToTarget()
    {

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // if we are close enough, then complete the rotation
        if (Mathf.Abs(Quaternion.Angle(targetRotation, transform.rotation)) < 1f)
        {
            transform.rotation = targetRotation;
            isRotating = false;
        }
    }

    // activate the GamePiece
    public void OnMouseEnter()
    {
        isActive = true;
    }

    // deactivate the GamePiece
    public void OnMouseExit()
    {
        isActive = false;
    }

    private void Update()
    {
        // finish rotation motion
        if (isRotating)
        {
            RotateToTarget();
        }

        // trigger a CW or CCW rotation
        if (isActive && !isRotating)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                RotateCounterClockwise();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                RotateCounterClockwise();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                RotateClockwise();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                RotateClockwise();
            }
        }
    }
}
