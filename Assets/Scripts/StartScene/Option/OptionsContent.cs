using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StartMenu
{
    ///<summary>
    ///옵션 창 요소 객체
    ///</summary>
    ///<remarks>
    ///필요 오버라이딩 함수 : void Execute(), void Execute(Vector2 direction);
    ///</remarks>
    public class OptionsContent : MonoBehaviour
    {
        Image backgroundImage;

        public void Activate() => backgroundImage.enabled = true;
        public void Deactivate() => backgroundImage.enabled = false;
        
        protected virtual void Awake() 
        {
            backgroundImage = GetComponent<Image>();    
        }

        ///<summary>
        ///옵션 창 초기화. 주로 상태에 대해 초기화함
        ///</summary>
        public virtual void Init()
        {
            //예시: 적용 하지 않은 인덱스를 원상복귀
        }

        ///<summary>
        ///옵션 창 실행
        ///</summary>
        public virtual void Execute()
        {
            //override
        }

        ///<summary>
        ///옵션 창 실행 (화살표 키)
        ///</summary>
        public virtual void Execute(float direction)
        {
            //override
        }
    }
}
