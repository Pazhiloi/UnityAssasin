using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
  [Header("Player Movement")]
  public float movementSpeed = 6f;
  public float rotSpeed = 450f;
  public MainCameraController MCC;
  public EnvironmentChecker environmentChecker;
  Quaternion requiredRotation;
  bool playerControl = true;

  [Header("Player Animator")]
  public Animator animator;

  [Header("Player Collision & Gravity")]
  public CharacterController CC;
  public float surfaceCheckRadius = 0.1f;
  public Vector3 surfaceCheckOffset;
  public LayerMask surfaceLayer;
  bool onSurface;
  public bool playerOnLedge{get;set;}
 [SerializeField] private  float fallingSpeed;
 [SerializeField] private  Vector3 moveDir;

  private void Update()
  {
    PlayerMovement();
    if (!playerControl)
    {
      return;
    }

    if (onSurface)
    {
      fallingSpeed = -0.5f;

      playerOnLedge = environmentChecker.CheckLedge(moveDir);
      if (playerOnLedge)
      {
        Debug.Log("Player on Ledge");
      }
    }else{
      fallingSpeed += Physics.gravity.y * Time.deltaTime;
    }
    var velocity = moveDir * movementSpeed;
    velocity.y = fallingSpeed;
    SurfaceCheck();
  }

  void PlayerMovement()
  {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

    var movementInput = (new Vector3(horizontal, 0, vertical)).normalized;

    var movementDirection = MCC.flatRotation * movementInput;
    CC.Move(movementDirection * movementSpeed * Time.deltaTime);
    if (movementAmount > 0)
    {
      requiredRotation = Quaternion.LookRotation(movementDirection);
    }
    moveDir = movementDirection;

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

  public void SetControl(bool hasControl){
    this.playerControl = hasControl;
    CC.enabled = hasControl;

    if (!hasControl)
    {
      animator.SetFloat("movementValue", 0f);
      requiredRotation = transform.rotation;
    }
  }
}
