using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourControllerScript : MonoBehaviour
{
  public EnvironmentChecker environmentChecker;
  bool playerInAction;
  public Animator animator;



  private void Update()
  {

    if (Input.GetButton("Jump") && !playerInAction)
    {
      var hitData = environmentChecker.CheckObstacle();

      if (hitData.hitFound)
      {
        StartCoroutine(PerformParkourAction());
      }

    }
  }

  IEnumerator PerformParkourAction()
  {
    playerInAction = true;
    animator.CrossFade("JumpUp", 0.2f);
    yield return null;

    var animationState = animator.GetNextAnimatorStateInfo(0);
    yield return new WaitForSeconds(animationState.length);

    playerInAction = false;
  }
}
