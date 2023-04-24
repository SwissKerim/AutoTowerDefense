using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int WaveNumber = 1;

    private int _enemyNumber = 0;
    private int _enemyNumberPerWave = 5;

    // Factors
    private double _enemyNumberPerWaveFactor = 0.25d;
    private double _enemyDamageFactor = 0.25d;
    private double _enemyHpFactor = 0.25d;
    private double _enemySpeedFactor = 0.05d;
    private double _enemyAttackSpeedFactor = 0.01d;
    private double _enemyAttackRangeFactor = 0.01d;
    private double _enemyArmorFactor = 0.05d;

    private const float EnemySpawnIntervalSec = 1f;
    private const float EnemyWaveIntervalSec = 20f;
    private const float GameLoopIntervalSec = 2f;

    private EnemySpawner[] _enemySpawner;

    [SerializeField] private TextMeshProUGUI WaveNumberText;

    private void Start()
    {
        WaveNumber = 1;
        WaveNumberText.text = WaveNumber.ToString();
        
        _enemySpawner = GetComponentsInChildren<EnemySpawner>();
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            if (_enemyNumber < _enemyNumberPerWave)
            {
                SpawnSuicideEnemy();
                yield return new WaitForSeconds(EnemySpawnIntervalSec);
            }

            Debug.Log("Next Wave");

            // Next Wave
            IncreaseWave();
            yield return new WaitForSeconds(EnemyWaveIntervalSec);
        }
    }

    private void SpawnSuicideEnemy()
    {
        Debug.Log("Spawn Suicide Enemy");
        foreach (EnemySpawner enemySpawner in _enemySpawner)
        {
            enemySpawner.SpawnSuicideEnemy(_enemyDamageFactor, _enemyHpFactor, _enemyArmorFactor);
            _enemyNumber++;
        }
    }

    private void IncreaseWave()
    {
        WaveNumber++;
        WaveNumberText.text = WaveNumber.ToString();

        // Increase factors every 10 waves
        if (WaveNumber % 10 == 0)
        {
            IncreaseFactors();
        }

        _enemyNumber = 0;

        // Increase the number of enemies per wave by a factor and round it to the nearest integer
        _enemyNumberPerWave = (int)Math.Round(_enemyNumberPerWave * (1 + _enemyNumberPerWaveFactor));
    }

    private void IncreaseFactors()
    {
        _enemyDamageFactor += 0.25d;
        _enemyHpFactor += 0.25d;
        _enemySpeedFactor += 0.05d;
        _enemyAttackSpeedFactor += 0.01d;
        _enemyAttackRangeFactor += 0.01d;
        _enemyArmorFactor += 0.05d;
        _enemyNumberPerWaveFactor += 0.25d;
    }
}