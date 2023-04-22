using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _points;

    [SerializeField]
    private float _unitSpeed;

    [SerializeField]
    private float _waitingTimeAtPoint;
    
    private Vector3 _targetPosition;
    private Vector3 _offset;
    private int _pointIndex;
    private float _currentTime;

    private void Awake()
    {
        var offsetY = transform.localScale.y / 2;
        _offset = new Vector3(0, offsetY, 0);
    }

    private void Start()
    {
        if (_points.Count != 0)
        {
            _targetPosition = _points[_pointIndex].position;
        }
    }

    private void Update()
    {
        if (_currentTime > _waitingTimeAtPoint)
        {
            MoveToNextPoint();
        }
        else
        {
            _currentTime += Time.deltaTime;
        }
    }

    private void MoveToNextPoint()
    {
        var currentPosition = transform.position;
        var distance = Vector3.Distance(currentPosition, _targetPosition);
        var travelTime = distance / _unitSpeed;
        var progress = 1 / travelTime;

        var newPosition = Vector3.Lerp(currentPosition, _targetPosition + _offset, progress);
        transform.position = newPosition;

        if (IsUnitOnTargetPosition())
        {
            _currentTime = 0;
            
            if (_pointIndex == _points.Count)
            {
                _pointIndex = 0;
            }
            
            _targetPosition = _points[_pointIndex].position;
            _pointIndex++;
        }
    }

    private bool IsUnitOnTargetPosition()
    {
        return transform.position == _targetPosition + _offset;
    }
}
