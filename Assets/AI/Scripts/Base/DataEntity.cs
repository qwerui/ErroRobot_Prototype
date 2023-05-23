using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree
{
    public abstract class DataEntity
    {
        public abstract DataEntity Get();
    }

    public class DataEntity<T> : DataEntity
    {
        T value;

        public T Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        public override DataEntity Get()
        {
            return this;
        }
    }

}
