using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiece : MonoBehaviour
{
    Vector3 mOffset;
    float zCoord;
    bool inPlace = false;

    void OnMouseDown()
    {
        if(!inPlace)
        {
            zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        }  
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        if(!inPlace)
        {
            transform.position = GetMouseAsWorldPoint() + mOffset;
        } 
    }

    void OnMouseUp() 
    {
        inPlace = FindObjectOfType<JigsawLogic>().SnapPiece(this.gameObject);
    }
}