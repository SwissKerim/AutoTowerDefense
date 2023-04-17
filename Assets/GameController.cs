using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    public int EnemyAlive = 0;

    public int _waveNumber = 1;

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
    private const float EnemyWaveIntervalSec = 5f;
    private const float GameLoopIntervalSec = 2f;

    private EnemySpawner _enemySpawner;

    private void Start()
    {
        _enemySpawner = GetComponentInChildren<EnemySpawner>();
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            if (_enemyNumber < _enemyNumberPerWave)
            {
                _enemySpawner.SpawnEnemy(_enemyDamageFactor, _enemyHpFactor, _enemyArmorFactor);
                _enemyNumber++;
                EnemyAlive++;

                yield return new WaitForSeconds(EnemySpawnIntervalSec);
            }

            if (EnemyAlive <= 0)
            {
                // Next Wave
                yield return new WaitForSeconds(EnemyWaveIntervalSec);
                
                _waveNumber++;
                
                // Increase factors every 10 waves
                if (_waveNumber % 10 == 0)
                {
                    IncreaseFactors();
                }

                _enemyNumber = 0;
                EnemyAlive = 0;
                
                // Increase the number of enemies per wave by a factor and round it to the nearest integer
                _enemyNumberPerWave = (int)Math.Round(_enemyNumberPerWave * (1 + _enemyNumberPerWaveFactor));
            }
            
            yield return new WaitForSeconds(GameLoopIntervalSec);
        }
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