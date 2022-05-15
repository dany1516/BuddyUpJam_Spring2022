using System.Collections.Generic;
using UnityEngine;

public class Presentation : MonoBehaviour
{
    [SerializeField] List<PresentationSystem> pointToMove;

    Vector3 startPosition;
    Quaternion startRotation;

    public void CameraToUse(int cameraIndex)
    {
        for(int i = 0; i < pointToMove.Count; i++) 
        {
            if(i == cameraIndex)
            {
                pointToMove[i].canMoveCamera = true;
            }
            else
            {
                pointToMove[i].canMoveCamera = false;
            }
        }
    }

    void Update()
    {
        for(int i = 0; i < pointToMove.Count; i++) 
        {
            MoveCameraAt(i);
        }   
    }

    private void MoveCameraAt(int i)
    {
        if (pointToMove[i].canMoveCamera)
        {
            float t = 0;
            startPosition = transform.position;
            startRotation = transform.rotation;
            t += Time.deltaTime / pointToMove[i].etaTime;
            transform.position = Vector3.Lerp(startPosition, pointToMove[i].pointPrefab.transform.position, t);
            transform.rotation = Quaternion.Lerp(startRotation, pointToMove[i].pointPrefab.transform.rotation, t);
            if(pointToMove[i].pointPrefab.transform.position == transform.position)
            {
                pointToMove[i].canMoveCamera = false;
            }
        }
    }

    [System.Serializable]
    public class PresentationSystem
    {
        public GameObject pointPrefab;
        public float etaTime;
        public bool canMoveCamera = false;
    }
}
