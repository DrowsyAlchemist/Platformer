//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public class EnemiesWinTrigger : MonoBehaviour
//{
//    [SerializeField] private UnityEvent _enemiesWin;

//    public event UnityAction EnemiesWin
//    {
//        add => _enemiesWin.AddListener(value);
//        remove => _enemiesWin.RemoveListener(value);
//    }

//    private Enemy[] _enemies;

//    private void OnEnable()
//    {
//        _enemies = GetComponentsInChildren<Enemy>();

//        if (_enemies.Length == 0)
//            throw new System.Exception("There are no enemies.");

//        foreach (var enemy in _enemies)
//        {
//            //enemy.IsWin += OnEnemiesWin;
//            EnemiesWin += enemy.OnEnemiesWin;
//        }
//    }

//    private void OnEnemiesWin()
//    {
//        _enemiesWin.Invoke();
//    }
//}
