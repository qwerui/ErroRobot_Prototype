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
        
        ///<summary>
        ///옵션 창 실행
        ///</summary>
        public virtual void Execute()
        {
            //반드시 override
        }

        ///<summary>
        ///옵션 창 실행 (화살표 키)
        ///</summary>
        public virtual void Execute(Vector2 direction)
        {
            //반드시 override
        }
    }
}
