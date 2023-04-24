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
    
    private Vector3 _offset;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    
    private int _targetPointIndex = -1;
    private float _travelTime;
    private float _currentTime;

    private void Awake()
    {
        var offsetY = transform.localScale.y / 2;
        _offset = new Vector3(0, offsetY, 0);
    }

    private void Start()
    {
        SetNextTargetPoint();
        CalculateTravelTime();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        
        MoveToTargetPoint();
        
        if (_currentTime > _travelTime + _waitingTimeAtPoint)
        {
            SetNextTargetPoint();
            CalculateTravelTime();
            _currentTime = 0;
        }
    }

    private void SetNextTargetPoint()
    {
        _targetPointIndex = (_targetPointIndex + 1) % _points.Count; 
        
        _startPosition = transform.position;
        _targetPosition = _points[_targetPointIndex].position + _offset;
    }

    private void CalculateTravelTime()
    {
        var distance = Vector3.Distance(_startPosition, _targetPosition);
        _travelTime = distance / _unitSpeed;
    }

    private void MoveToTargetPoint()
    {
        var progress = _currentTime / _travelTime;
        var newPosition = Vector3.Lerp(_startPosition, _targetPosition, progress);
        transform.position = newPosition;
    }
}
