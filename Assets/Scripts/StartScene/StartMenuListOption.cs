using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace StartMenu
{
    //메인 메뉴의 리스트 요소 객체 

    public class StartMenuListOption : MonoBehaviour
    {
        new Transform transform;
        Image selectedImage;
        EventTrigger eventTrigger;

        private void Awake()
        {
            TryGetComponent<Transform>(out transform);
            TryGetComponent<EventTrigger>(out eventTrigger);
            selectedImage = transform.GetChild(0).GetComponent<Image>();

            if(eventTrigger != null)
            {
                EventTrigger.Entry mouseEnterEntry = new EventTrigger.Entry();
                mouseEnterEntry.eventID = EventTriggerType.PointerEnter;
                mouseEnterEntry.callback.AddListener(data=>Select());

                EventTrigger.Entry mouseExitEntry = new EventTrigger.Entry();
                mouseExitEntry.eventID = EventTriggerType.PointerExit;
                mouseExitEntry.callback.AddListener(data=>Deselect());

                eventTrigger.triggers.Add(mouseEnterEntry);
                eventTrigger.triggers.Add(mouseExitEntry);
            }
        }

        ///<summary>
        ///메뉴 선택
        ///</summary>
        public void Select()
        {
            selectedImage.enabled = true;
        }

        ///<summary>
        ///메뉴 선택 해제
        ///</summary>
        public void Deselect()
        {
            selectedImage.enabled = false;
        }

        ///<summary>
        ///메뉴의 기능 실행, 반드시 Override
        ///</summary>
        public virtual void Execute()
        {
            UnityEngine.Assertions.Assert.IsNotNull(null, $"Execute must override : {gameObject.name}");
        }
    }

}

