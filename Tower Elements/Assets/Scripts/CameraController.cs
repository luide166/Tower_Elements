using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool canMove = false;

    public float moveSpeed = 30f;
    public float panBorderMouseCaption = 2f;
    public float scroolspeed;
    public float minY = 10f;
    public float maxY = 100f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameisOver)
        {
            this.enabled = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            canMove = !canMove;
        }

        
        #region Camera Moviment

        if (!canMove)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderMouseCaption)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);

        }
        else if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <=  panBorderMouseCaption)
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);

        }
        else if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderMouseCaption)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);

        }
        else if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderMouseCaption)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);

        }

        float scrool = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;
        pos.y -= scrool * Time.deltaTime * scroolspeed * 700;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;

        #endregion

    }
}
