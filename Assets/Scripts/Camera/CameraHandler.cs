using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using LTA.DesignPattern;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float zoomSpeed = 5f;
    [SerializeField] CinemachineVirtualCamera cinemachine;
    [SerializeField] float minOrthographicSize = 10f;
    [SerializeField] float maxOrthographicSize = 20f;

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
        Vector3 moveDir = new Vector3(x, y).normalized;

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
