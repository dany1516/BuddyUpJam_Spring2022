using UnityEngine;

public class MovePiece : MonoBehaviour
{
    Vector3 mOffset;
    SFXHandler audioHandler;
    float zCoord;

    void Start() 
    {
        audioHandler = FindObjectOfType<SFXHandler>();
    }

    void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        audioHandler.PlayPickUpSFX();
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mOffset;
    }

    void OnMouseUp() 
    {
        var isConnected = FindObjectOfType<JigsawLogic>().SnapPiece(this.gameObject);
        if(isConnected)
        {
            audioHandler.PlayConnectSFX();
            return;
        }
        audioHandler.PlayPutDownSFX();
    }
}
