using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourControllerScript : MonoBehaviour
{
  public EnvironmentChecker environmentChecker;
  bool playerInAction;
  public Animator animator;
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

    animator.CrossFade(action.AnimationName, 0.2f);
    yield return null;

    var animationState = animator.GetNextAnimatorStateInfo(0);
    if (!animationState.IsName(action.AnimationName))
    {
      Debug.Log("Animations Name is Incorrect");
    }
    yield return new WaitForSeconds(animationState.length);

    playerInAction = false;
  }
}
