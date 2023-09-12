using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyInfo", menuName = "Infos/EnemyInfo", order = 0)]
    public class EnemyInfo : ScriptableObject
    {
        public int id;
        public GameObject prefab;
    }

}
