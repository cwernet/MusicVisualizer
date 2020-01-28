using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingOrb : MonoBehaviour
{

    Transform _transform;
    Vector3 _transpose;
    //public float _speed, _amp;
    //public Vector3 amplitude;

    //Vector3 _trans;
    //Vector3 _max;
    //Vector3 _min;
    //Vector3 _target;

    //new

    //public float timePeriod = 2;
    public float ySpeed = 1, xSpeed = 1, zSpeed;
    public float yMax = 2f, xMax = 5, zMax = 5;
    //private float timeSinceStart;
    //private Vector3 pivot;
    float timingOffset1, timingOffset2, timingOffset3;

    // Start is called before the first frame update
    void Start()
    {
        timingOffset1 = Random.value * (Mathf.PI / 2);
        timingOffset2 = Random.value * (Mathf.PI / 2);
        timingOffset3 = Random.value * (Mathf.PI / 2);

        _transform = GetComponent<Transform>();
        _transpose = transform.position;

        //_max = transform.localPosition + amplitude;
        //_min = transform.localPosition - amplitude;

        //_target = _max;

        //pivot = transform.position;
        //height /= 2;
        //timeSinceStart = (3 * timePeriod) / 4;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _t2 = _transpose;
        _t2.y = (Mathf.Sin((Time.time + timingOffset1) * ySpeed)) * yMax;
        _t2.x = (Mathf.Sin((Time.time + timingOffset2) * xSpeed)) * xMax;
        _t2.z = (Mathf.Sin((Time.time + timingOffset3) * zSpeed)) * zMax;
        _t2 += _transpose;
        _transform.position = _t2;


        //Vector3 _curr = transform.localPosition;
        //Vector3 _init = _curr;



        //if(_target == _max && _curr == _target)
        //{
        //    _target = _min;
        //}



        //if (_target == _min && _curr == _target)
        //{
        //    _target = _max;
        //}


        //_curr = Vector3.Lerp(_init, _target, Time.deltaTime * _speed);

        //transform.localPosition = _curr;



        //_trans.y +=  _amp * Time.deltaTime * Mathf.Sin(Time.time * _speed);

        //_transform.position = _trans;

        //Vector3 nextPos = transform.position;
        //nextPos.y = pivot.y +  height * Mathf.Sin(((Mathf.PI * 2) / timePeriod) * Time.time/** timeSinceStart*/);
        ////timeSinceStart += Time.deltaTime;
        //transform.position = nextPos;

    }
}
