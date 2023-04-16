using System;
using UnityEngine;

public class TowerAttackRange : MonoBehaviour
{
    public SphereCollider AttackRangeCollider;
    private Tower _tower;
    
    // Start is called before the first frame update
    void Start()
    {
        AttackRangeCollider = GetComponent<SphereCollider>();
        _tower = GetComponentInParent<Tower>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _tower.EnemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _tower.EnemiesInRange.Remove(other.gameObject);
        }
    }
}
