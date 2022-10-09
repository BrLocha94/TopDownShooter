using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header("External references")]
    [SerializeField]
    private Transform target;

    [Header("Camera behaviour configs")]
    [SerializeField]
    private float limitLeft = -1f;
    [SerializeField]
    private float limitRight = 1f;
    [SerializeField]
    private float limitUp = 1f;
    [SerializeField]
    private float limitDown = -1f;

    private void Update()
    {
        float positionX = (target.position.x < limitLeft) ? limitLeft : (target.position.x > limitRight) ? limitRight : target.position.x;
        float positionY = (target.position.y > limitUp) ? limitUp : (target.position.y < limitDown) ? limitDown : target.position.y;

        transform.localPosition = new Vector3(positionX, positionY, transform.localPosition.z);

    }
}
