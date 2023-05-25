using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree
{
    ///<summary>
    ///<para>Behavior Tree에 사용되는 데이터 집합, 생성자에서 필드를 Set을 사용해 초기화 필수</para>
    ///<para>CreateAsset 속성을 사용해 에셋 생성해 사용</para>
    ///</summary>
    [CreateAssetMenu(fileName = "Blackboard", menuName = "AI/Blackboard", order = 0)]
    public class Blackboard : ScriptableObject 
    {
        protected Dictionary<string, DataEntity> dict = new Dictionary<string, DataEntity>();

        ///<summary>
        ///데이터 가져오기
        ///</summary>
        public T Get<T> (string key)
        {
            var result = dict[key]?.Get() as DataEntity<T>;
            if(result == null)
            {
                return default(T);
            }
            return result.Value;
        }

        ///<summary>
        ///데이터 넣기
        ///</summary>
        public void Set<T>(string key, T value)
        {
            var entity = new DataEntity<T>();
            entity.Value = value;
            dict[key] = entity;
        }

        ///<summary>
        ///복사본 생성
        ///</summary>
        public Blackboard Clone()
        {
            return Instantiate(this);
        }
    }
}
