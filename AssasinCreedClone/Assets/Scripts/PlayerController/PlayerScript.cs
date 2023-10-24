using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
  [Header("Player Movement")]
  public float movementSpeed = 6f;
  public float rotSpeed = 450f;
  public MainCameraController MCC;
  Quaternion requiredRotation;
  [Header("Player Animator")]
  public Animator animator;

  [Header("Player Collision & Gravity")]
  public CharacterController CC;
  public float surfaceCheckRadius = 0.1f;
  public Vector3 surfaceCheckOffset;
  public LayerMask surfaceLayer;
  bool onSurface;

  private void Update()
  {
    PlayerMovement();
    SurfaceCheck();
  }

  void PlayerMovement()
  {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

    var movementInput = (new Vector3(horizontal, 0, vertical)).normalized;

    var movementDirection = MCC.flatRotation * movementInput;

    if (movementAmount > 0)
    {
      CC.Move(movementDirection * movementSpeed * Time.deltaTime);
      requiredRotation = Quaternion.LookRotation(movementDirection);
    }

    transform.rotation = Quaternion.RotateTowards(transform.rotation, requiredRotation, rotSpeed * Time.deltaTime);

    animator.SetFloat("movementValue", movementAmount, 0.2f, Time.deltaTime);
  }
  void SurfaceCheck(){
    onSurface = Physics.CheckSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius, surfaceLayer);
  }

   private void OnDrawGizmosSelected() {
    Gizmos.color = Color.yellow;
    Gizmos.DrawSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius);
  }
}
