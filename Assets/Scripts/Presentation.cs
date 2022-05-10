using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presentation : MonoBehaviour
{
    [SerializeField] List<PresentationSystem> pointToMove;

    bool canMoveCamera;
    // Start is called before the first frame update
    void Start()
    {
        canMoveCamera = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            canMoveCamera = true;
        }
        if(canMoveCamera)
        {
            var step = 5 * Time.deltaTime;
            var step2 = 180 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pointToMove[0].pointPrefab.transform.position, step);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, pointToMove[0].pointPrefab.transform.rotation, step2);
            if(Vector3.Distance(transform.position, pointToMove[0].pointPrefab.transform.position) < 0.001)
            {
                
            }
        }
    }

    [System.Serializable]
    public class PresentationSystem
    {
        public GameObject pointPrefab;
        public float etaTime;
    }
}
