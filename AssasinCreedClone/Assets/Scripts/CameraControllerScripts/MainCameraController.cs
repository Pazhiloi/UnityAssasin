using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
  [Header("Camera Controller")]
  public Transform target;
  public float gap = 3f;
  public float rotSpeed = 3f;

  float rotX;
  float rotY;
  public float minVerAngle = -14f;
  public float maxVerAngle = 45f;
  public Vector2 framingBalance;

  private void Start() {
    Cursor.lockState = CursorLockMode.Locked;
  }
  private void Update()
  {

    rotX += Input.GetAxis("Mouse Y") * rotSpeed;
    rotX = Mathf.Clamp(rotX, minVerAngle, maxVerAngle);
    rotY += Input.GetAxis("Mouse X") * rotSpeed;

    var targetRotation = Quaternion.Euler(rotX, rotY, 0);

    var focusPos = target.position + new Vector3(framingBalance.x, framingBalance.y);

    transform.position = focusPos - targetRotation * new Vector3(0, 0, gap);
    transform.rotation = targetRotation;
  }
}
