using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
  [Header("Player Movement")]
  public float movementSpeed = 3f;

  private void Update()
  {
    PlayerMovement();
  }

  void PlayerMovement()
  {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    float movementAmount = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

    var movementInput = (new Vector3(horizontal, 0, vertical)).normalized;

    if (movementAmount > 0)
    {
      transform.position += movementInput * movementSpeed * Time.deltaTime;
      transform.rotation = Quaternion.LookRotation(movementInput);
    }
  }

}
