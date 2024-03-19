using UnityEngine;

public class DeadState : IState
{
    private readonly EnemyBrain brain; 

    public DeadState(EnemyBrain brain)
    {
        this.brain = brain;
    }

    public void OnEnter()
    {
        EnemyBrain.Destroy(brain.gameObject);// Zerstörung des Spielobjekts des Feindes
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }

    public Color GizmoColor()
    {
        return Color.black; // Rückgabe der Farbe Schwarz für die Anzeige im Editor
    }
}
