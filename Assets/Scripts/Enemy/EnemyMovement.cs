using UnityEngine;

public class EnemyMovement 
{
    private Vector3 _direction = new Vector3();
    private Quaternion _targetRotation;
    private Transform _transform;
    private float _targetX;
    private float _targetZ;

    public void Init(Transform transform)
    {
        _transform = transform;
    }


    public void MoveTo(Vector3 _target,float speed,float rotationSpeed)
    {
        _direction = _target - _transform.position;
        _targetRotation = Quaternion.LookRotation(_direction);
        Quaternion lookAtRotationOnly_Y = Quaternion.Euler(_transform.rotation.eulerAngles.x, _targetRotation.eulerAngles.y, _transform.rotation.eulerAngles.z);
        _transform.rotation = Quaternion.Lerp(_transform.rotation, lookAtRotationOnly_Y, rotationSpeed * Time.deltaTime);
        _transform.position = Vector3.MoveTowards(_transform.position, new Vector3(_target.x, _transform.position.y, _target.z), speed * Time.deltaTime);
    }


}
