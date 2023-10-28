using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourControllerScript : MonoBehaviour
{
  public EnvironmentChecker environmentChecker;
  bool playerInAction;
  public Animator animator;
  public PlayerScript playerScript;

  [Header("Parkour Action Area")]
  public List<NewParkourAction> newParkourActions;



  private void Update()
  {

    if (Input.GetButton("Jump") && !playerInAction)
    {
      var hitData = environmentChecker.CheckObstacle();

      if (hitData.hitFound)
      {
        foreach (var action in newParkourActions)
        {
          if (action.CheckIfAvailable(hitData, transform))
          {
            StartCoroutine(PerformParkourAction(action));
            break;
          }
        }
      }

    }
  }

  IEnumerator PerformParkourAction(NewParkourAction action)
  {
    playerInAction = true;
    playerScript.SetControl(false);

    animator.CrossFade(action.AnimationName, 0.2f);
    yield return null;

    var animationState = animator.GetNextAnimatorStateInfo(0);
    if (!animationState.IsName(action.AnimationName))
    {
      Debug.Log("Animations Name is Incorrect");
    }

    float timerCounter = 0f;

    while (timerCounter <= animationState.length)
    {
      timerCounter += Time.deltaTime;
      if (action.LookAtObstacle)
      {
       transform.rotation = Quaternion.RotateTowards(transform.rotation, action.RequiredRotation, playerScript.rotSpeed * Time.deltaTime);
      }
      if (action.AllowTargetMatching)
      {
        CompareTarget(action);
      }
      yield return null;
    }

    playerScript.SetControl(true);
    playerInAction = false;
  }

  private void CompareTarget(NewParkourAction action){
    animator.MatchTarget(action.ComparePosition, transform.rotation, action.CompareBodyPart, new MatchTargetWeightMask(action.ComparePositionWeight, 0), action.CompareStartTime, action.CompareEndTime);
  }
}
