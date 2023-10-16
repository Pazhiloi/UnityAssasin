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
  private void Update()
  {

    rotX += Input.GetAxis("Mouse Y");
    rotY += Input.GetAxis("Mouse X");

    var targetRotation = Quaternion.Euler(rotX, rotY, 0);

    transform.position = target.position - targetRotation * new Vector3(0, 0, gap);
    transform.rotation = targetRotation;
  }
}
