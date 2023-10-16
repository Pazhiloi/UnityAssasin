using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
  [Header("Camera Controller")]
  public Transform target;
  public float gap = 3f;
  float rotX;
  float rotY;
  public float minVerAngle = -14f;
  public float maxVerAngle = 45f;
  private void Update()
  {

    rotX += Input.GetAxis("Mouse Y");
    rotX = Mathf.Clamp(rotX, minVerAngle, maxVerAngle);
    rotY += Input.GetAxis("Mouse X");

    var targetRotation = Quaternion.Euler(rotX, rotY, 0);

    transform.position = target.position - targetRotation * new Vector3(0, 0, gap);
    transform.rotation = targetRotation;
  }
}
