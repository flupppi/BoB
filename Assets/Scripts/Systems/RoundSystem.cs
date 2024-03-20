using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSystem : MonoBehaviour {
    [SerializeField] private SpawnerSystem m_spawnerSystem;
    [SerializeField] private UpgradeSystem m_upgradeSystem;
    private int m_maxRounds;
    private int m_currentRound = 0;
    private int m_enemyCount = 0;
    private RoundStatus m_status = RoundStatus.UpgradePhase;

    public event Action OnFinish;
    public event Action<int> OnRoundStart;
    public event Action OnUpgradePhaseStart;

    public RoundStatus Status {
        get => m_status;
        private set => m_status = value;
    }

    public int Round => m_currentRound;

    [ContextMenu("Start next Round")]
    public bool StartNextRound() {
        if (Status == RoundStatus.Started || Status == RoundStatus.Finished)
            return false;

        Status = RoundStatus.Started;

        // Vllt Countdown?

        // Spawn wave and get enemies
        m_spawnerSystem.SpawnNextWave();
        m_enemyCount = m_spawnerSystem.WaveEnemies.Length;

        // Register to enemy dead delegate/event
        foreach (var enemy in m_spawnerSystem.WaveEnemies) {
            if (enemy.TryGetComponent<HealthComponent>(out HealthComponent enemyHealthComponent))
            {
                enemyHealthComponent.OnDeath += () => m_enemyCount--;
            }
        }

        OnRoundStart?.Invoke(m_currentRound);
        m_currentRound++;
        
        return true;
    }

    [ContextMenu("End current Round")]
    public bool EndRound() {
        if (Status == RoundStatus.UpgradePhase || Status == RoundStatus.Finished)
            return false;

        // Last Round is finished
        if (m_currentRound == m_maxRounds)
        {
            Status = RoundStatus.Finished;
            OnFinish?.Invoke();
            return true;
        }

        Status = RoundStatus.UpgradePhase;

        OnUpgradePhaseStart?.Invoke();

        // Enable Upgrade table
        m_upgradeSystem.Enable();

        return true;
    }

    public bool AllEnemiesDead() {
        return m_enemyCount == 0;
    }

    public void DestroyDeadEnemies() {
        foreach (var enemy in m_spawnerSystem.WaveEnemies)
        {
            Destroy(enemy);
        }
    }

    void Start() {
        m_maxRounds = m_spawnerSystem.WaveCount;
        OnUpgradePhaseStart += DestroyDeadEnemies;
        m_upgradeSystem.OnCloseWindow += () => StartNextRound();

        OnRoundStart += (x) => Debug.Log($"Round {x} started");
        OnUpgradePhaseStart += () => Debug.Log("Round finished");
        OnFinish += () => Debug.LogError("Finished");

        StartNextRound();
    }

    void Update() {
        if (AllEnemiesDead()) {
            EndRound();
        }
    }
}

public enum RoundStatus {
    Started,
    UpgradePhase,
    Finished
}