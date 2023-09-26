using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class SlowCC : CrowdControl
{
    float speedReducer;
    float reducedValue;

    /// <summary>
    /// 슬로우 CC 감소율이 가장 큰 것만 적용
    /// </summary>
    /// <param name="time">지속 시간</param>
    /// <param name="percentage">감소율 < 1</param>
    public SlowCC(float time, float percentage)
    {
        consistenceTime = time;
        speedReducer = 1-percentage;
    }

    public override void OnStart(EnemyBase enemy)
    {
        reducedValue = enemy.Speed * speedReducer;

        foreach(CrowdControl crowdControl in enemy.crowdControls)
        {
            //이미 슬로우가 적용되어 있는 경우
            if(crowdControl is SlowCC)
            {
                var alreadyCC = crowdControl as SlowCC;
                if(alreadyCC.speedReducer <= speedReducer)
                {
                    alreadyCC.OnEnd(enemy);
                    
                    enemy.Speed -= reducedValue;
                    base.OnStart(enemy);
                }
                else
                {
                    return;
                }
            }
        }

        //슬로우가 처음 적용될 경우
        enemy.Speed -= reducedValue;
        base.OnStart(enemy);
    }

    public override void OnEnd(EnemyBase enemy)
    {
        enemy.Speed += reducedValue;
        base.OnEnd(enemy);
    }
}
