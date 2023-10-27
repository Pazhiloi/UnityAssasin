using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourControllerScript : MonoBehaviour
{
   public EnvironmentChecker environmentChecker;

   private void Update() {
    environmentChecker.CheckObstacle();
   }
}
