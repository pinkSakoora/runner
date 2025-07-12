using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera Cam;
    public Transform Subject;

    Vector2 _startPosition;
    float _startZ;

    Vector2 _travel => (Vector2)Cam.transform.position - _startPosition;

    float _distanceFromSubject => transform.position.z - Subject.position.z;
    float _clippingPlane => Cam.transform.position.z + (_distanceFromSubject > 0 ? Cam.farClipPlane : Cam.nearClipPlane);
    float _parallaxFactor => Mathf.Abs(_distanceFromSubject) / _clippingPlane;

    void Start()
    {
        _startPosition = transform.position;
        _startZ = transform.position.z;
    }

    void LateUpdate()
    {
        Vector2 newPos = _startPosition + _travel*_parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, _startZ);
    }
}
