
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourManager : MonoBehaviour
{
    public Transform playerTransform;
    public List<EnemyController> enemyList = new List<EnemyController>();
    public int flagCount;
    
    

    void Start()
    {
        EnemyController[] allEnemys = FindObjectsByType<EnemyController>();
        foreach (EnemyController enemy in allEnemys)
        {
            Debug.Log(enemy.name + " До врага " + DistanseToPlayer(enemy.transform.position.x));
        }
        
    }
    private void FixedUpdate()
    {
        if (enemyList.Count > 0)
        {
            flagCount = enemyList.Count(x => x.canAttack==true);
        }
        if (flagCount==0 && enemyList.Count>0)
        {
            SetCanAttackFlag();
        }
    }
    private void SetCanAttackFlag()
    {
        int randomIndex = UnityEngine.Random.Range(0, enemyList.Count);
        if (enemyList[randomIndex].isAttacking == false)
        {
            enemyList[randomIndex].canAttack = true;
        }

    }
    
    private float DistanseToPlayer(float _enemy)
    {
        if (playerTransform.position.x < _enemy)
        {
            return(Math.Abs(_enemy-playerTransform.position.x));
        }
        else
        {
            return (Math.Abs(playerTransform.position.x - _enemy));
        }
    }
    public void addToList(EnemyController enemy)
    {
        enemyList.Add(enemy);
    }
    public void removeFromList (EnemyController enemy)
    {
        enemyList.Remove(enemy);
    }
}
