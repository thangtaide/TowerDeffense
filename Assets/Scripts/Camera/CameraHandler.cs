using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using LTA.DesignPattern;
using System;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float zoomSpeed = 5f;
    [SerializeField] CinemachineVirtualCamera cinemachine;
    [SerializeField] float minOrthographicSize = 10f;
    [SerializeField] float maxOrthographicSize = 20f;
    [Range(0, 1f)]
    [SerializeField] float scrollingSizeMultil = .7f;

    Vector3 beginPosition, dragPosition, originPosition;
    float orthographicSize;
    float targetOrthorgraphicSize;
    bool drag = false;
    public bool canMoveByMouse;
    void Start()
    {
        canMoveByMouse = true;
        orthographicSize = cinemachine.m_Lens.OrthographicSize;
        targetOrthorgraphicSize = orthographicSize;
    }

    void Update()
    {
        HandlerMove();
        HandlerZoom();
    }

    void HandlerMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 scrollingSizeMoveLeftDown = new Vector3(Screen.width / 2 * (1 - scrollingSizeMultil), Screen.height / 2 * (1 - scrollingSizeMultil));
        Vector3 scrollingSizeMoveRightUp = new Vector3(Screen.width-scrollingSizeMoveLeftDown.x, Screen.height-scrollingSizeMoveLeftDown.y);

        float tempX = Input.mousePosition.x;
        if (tempX > scrollingSizeMoveRightUp.x)
        {
            x = (tempX-scrollingSizeMoveRightUp.x) / scrollingSizeMoveLeftDown.x;
        }else if(tempX < scrollingSizeMoveLeftDown.x)
        {
            x = (tempX - scrollingSizeMoveLeftDown.x) / scrollingSizeMoveLeftDown.x;
        }
        float tempY = Input.mousePosition.y;
        if (tempY > scrollingSizeMoveRightUp.y)
        {
            y = (tempY - scrollingSizeMoveRightUp.y) / scrollingSizeMoveLeftDown.y;
        }else if (tempY < scrollingSizeMoveLeftDown.y)
        {
            y = (tempY - scrollingSizeMoveLeftDown.y) / scrollingSizeMoveLeftDown.y;
        }

        Vector3 moveDir = new Vector3(x, y);

        transform.position += moveDir * Time.deltaTime * moveSpeed;
    }
    void HandlerZoom()
    {
        targetOrthorgraphicSize -= Input.mouseScrollDelta.y;
        targetOrthorgraphicSize = Mathf.Clamp(targetOrthorgraphicSize, minOrthographicSize, maxOrthographicSize);

        orthographicSize = Mathf.Lerp(orthographicSize,targetOrthorgraphicSize,zoomSpeed*Time.deltaTime);
        cinemachine.m_Lens.OrthographicSize = orthographicSize;
    }


    private void LateUpdate()
    {
        
        if(Input.GetMouseButton(0))
        {
            dragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(!drag)
            {
                drag = true;
                beginPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                originPosition = transform.position;
            }
        }
        else { drag = false; }
        if (canMoveByMouse)
        {
            if (drag)
            {
                transform.position = originPosition + beginPosition - dragPosition;
            }
        }
    }
}
public class CameraHandlerSingleton: SingletonMonoBehaviour<CameraHandler> { }
