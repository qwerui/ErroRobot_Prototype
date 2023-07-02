using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [System.Serializable]
    public class EnemyWave
    {
        public List<Wave> waveSets = new List<Wave>();
        public List<EnemySet> GetEnemyLists(int wave)
        {
            wave--; //1스테이지 index는 0

            if(wave < 0 || wave >= waveSets.Count)
            {
                return null;
            }

            Wave tempWave = waveSets[wave];
            int index = Random.Range(0, tempWave.wave.Count);
            
            if(tempWave.wave.Count == 0)
            {
                return null;
            }
            
            var tempEnemySet = tempWave.wave[index].enemyList;
            return tempEnemySet;
        }
    }

    //이 아래로 json 구조를 위한 클래스
    [System.Serializable]
    public class Wave
    {
        int waveIndex;
        public List<EnemyList> wave = new List<EnemyList>();
    }

    [System.Serializable]
    public class EnemyList
    {
        public List<EnemySet> enemyList = new List<EnemySet>();
    }

    [System.Serializable]
    public class EnemySet
    {
        public int enemyId;
        public int count;
    }
}
