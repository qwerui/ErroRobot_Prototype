using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    public GraphicRaycaster raycaster;

    PointerEventData pointerEventData;
    List<RaycastResult> results;

    PhaseManager phaseManager;

    EventTrigger currentTrigger = null;

    RectTransform rectTransform;
    Canvas canvas;
    public float moveSpeed = 1f;

    Vector2 moveDirection;
    Vector2 inputDirection;
    Vector2 currentPosition;

    void ShowPointer() => gameObject.SetActive(true);
    void HidePointer() => gameObject.SetActive(false);

    private void Awake() 
    {
        TryGetComponent<RectTransform>(out rectTransform);
        results = new List<RaycastResult>();
        pointerEventData = new PointerEventData(null);
    }

    private void Start() 
    {
        currentPosition = rectTransform.anchoredPosition;
        phaseManager = GameObject.FindObjectOfType<PhaseManager>();
        phaseManager.OnWaveStart += HidePointer;
        phaseManager.OnWaveEnd += ShowPointer;
    }

    private void OnEnable() 
    {
        currentPosition = new Vector2(640, 360);    
    }

    private void OnDisable() 
    {
        currentTrigger = null;    
    }

    //Raycast로 이벤트 발생, Build Phase일때만 활성화
    private void Update() 
    {
        results.Clear();
        pointerEventData.position = Screen.width / 1280f * rectTransform.anchoredPosition;
        
        raycaster.Raycast(pointerEventData, results);

        if(results.Count <= 0)
        {
            currentTrigger?.OnPointerExit(pointerEventData);
            currentTrigger=null;
            return;
        }

        results[0].gameObject.TryGetComponent<EventTrigger>(out EventTrigger hitTrigger);
        
        if(hitTrigger != currentTrigger)
        {
            currentTrigger?.OnPointerExit(pointerEventData);
            currentTrigger = hitTrigger;
            currentTrigger?.OnPointerEnter(pointerEventData);
        }
    }

    private void LateUpdate() 
    {
        currentPosition += moveSpeed * Time.deltaTime * moveDirection;
        currentPosition.x = Mathf.Clamp(currentPosition.x, 0, 1280);
        currentPosition.y = Mathf.Clamp(currentPosition.y, 0, 720);
        rectTransform.anchoredPosition = currentPosition;
    }

    public void SetDirection(Vector2 direction, InputEvent inputEvent)
    {
        inputDirection = direction * moveSpeed;
        moveDirection = inputDirection.normalized;
    }

    public void Click()
    {
        currentTrigger?.OnPointerClick(pointerEventData);
    }
}
