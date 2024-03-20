using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBrain : MonoBehaviour
{
    protected StateMachine stateMachine; // State Machine des Gegners
    public HealthComponent healthComponent;
    public NavMeshAgent navMeshAgent;
    public float attackRange = 1;
    public float attackCooldown = 1;
    public float movementSpeed = 5;
    [HideInInspector] public Transform target;
    [HideInInspector] public float distanceToTarget = Mathf.Infinity;
    [HideInInspector] public bool attacking = false;
    [HideInInspector] public bool readyToAttack = true;

    void Update()
    {
        stateMachine.Tick(); // Aktualisierung der State Machine in jedem Frame
        distanceToTarget = Vector3.Distance(target.position, transform.position);
    }
    public void InvokeResetAttack()
    {
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    public void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    private void OnDrawGizmos()
    {
        if (stateMachine != null) // Überprüfen, ob die Zustandsmaschine vorhanden ist
        {
            Gizmos.color = stateMachine.GetGizmoColor(); // Festlegen der Farbe für die Darstellung im Editor
            Gizmos.DrawSphere(transform.position + Vector3.up * 3, 0.4f); // Zeichnen einer Kugel zur Visualisierung des Zustands im Editor
        }
    }
}
