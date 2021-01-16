using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Radius : MonoBehaviour
{
    private EnemyAI _enemyAI;
    // Start is called before the first frame update
    void Start()
    {
        _enemyAI = transform.GetComponentInParent<EnemyAI>();
        if (_enemyAI == null)
            Debug.LogError("Enemy AI is null.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _enemyAI.ChangeEnemyState(EnemyAI.EnemyState.Attack);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _enemyAI.ChangeEnemyState(EnemyAI.EnemyState.Chase);
        }
    }
}
