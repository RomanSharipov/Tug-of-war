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

    public void MoveTo(Vector3 _target, float speed, float rotationSpeed,float speedX)
    {
        _transform.LookAt(new Vector3(_target.x,_transform.position.y,_target.z));
        //_direction = _target - _transform.position;
        //_targetRotation = Quaternion.LookRotation(_direction);
        //Quaternion lookAtRotationOnly_Y = Quaternion.Euler(_transform.rotation.eulerAngles.x, _targetRotation.eulerAngles.y, _transform.rotation.eulerAngles.z);
        //_transform.rotation = Quaternion.Lerp(_transform.rotation, lookAtRotationOnly_Y, rotationSpeed * Time.deltaTime);

        _targetX = Mathf.MoveTowards(_transform.position.x, _target.x, speedX * Time.deltaTime);
        _targetZ = Mathf.MoveTowards(_transform.position.z, _target.z, speed * Time.deltaTime);
        _target = new Vector3(_targetX, _transform.position.y, _targetZ);
        _transform.position = new Vector3(_target.x, _target.y, _target.z);
    }

}
