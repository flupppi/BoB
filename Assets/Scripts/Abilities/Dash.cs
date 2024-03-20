using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class Dash : AbilityBase
{    
    public float dashSpeed;
    
    public override void Activate(GameObject parent) {
        MonoBehaviour behaviour = parent.GetComponent<MonoBehaviour>();
        behaviour.StartCoroutine(PerformDash(parent));
    }

     private IEnumerator PerformDash(GameObject parent)
    {
        CharacterController characterController = parent.GetComponent<CharacterController>();
        Vector3 dashDirection = parent.transform.forward;

        float elapsedTime = 0f;
        while (elapsedTime < activeTime)
        {
            float distanceToMove = dashSpeed * Time.deltaTime;
            characterController.Move(dashDirection * distanceToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
