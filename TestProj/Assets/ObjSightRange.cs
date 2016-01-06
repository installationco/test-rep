﻿using UnityEngine;
using System.Collections.Generic;

public class ObjSightRange : MonoBehaviour
{

    public List<GameObject> objInSight;                         //для того, что этот объект видит
    private Vector2 _tmpRotateVector = new Vector2(0, 100);     //Вектор, которым будим кастить и вращать
    private GameObject _tmpPrevObj;                             //Чтобы не делать проверку на добавление объекта дважды
    private bool _tmpPrevCollision = false;                     // - //
    private List<Vector2> _sightLinePoints;                     // Крайние точки объектов видимости для данного объекта
    private RaycastHit2D _tmpRaycastHit, _tmpPrevRaycastHit;
    void Start()
    {
        objInSight = new List<GameObject>();
        _sightLinePoints = new List<Vector2>();
    }
    void Update()
    {
        for (int i = 0; i < 360; i+=2)
        {
            if (_tmpRaycastHit = Physics2D.Linecast(transform.position, MathToolkit.VectorbyPointAngle(transform.up, i) * 100)) // Чтобы работало, нужно повесить на препятствие
            {
                if (_tmpPrevCollision == false)
                    Debug.DrawLine(transform.position, MathToolkit.VectorbyPointAngle(transform.up, i) * 100);
                _tmpPrevCollision = true;
            }
            else
            {
                if (_tmpPrevCollision == true)
                {
                    Debug.DrawLine(transform.position, MathToolkit.VectorbyPointAngle(transform.up, i) * 100);
                    _tmpPrevCollision = false;
                }
            }
        }
    }
    public List<GameObject> GetSightObjects() // Получить все Объекты, которые видны данному
    {
        List<GameObject> ret = new List<GameObject>();
        for (int i = 0; i < 360; i++)
        {
            if (_tmpRaycastHit = Physics2D.Linecast(new Vector2(0, 0), _tmpRotateVector))
                if (!ret.Contains(_tmpRaycastHit.collider.gameObject))
                    ret.Add(_tmpRaycastHit.collider.gameObject);
            _tmpRotateVector = MathToolkit.RotateVectorbyAngle(_tmpRotateVector, 1);
        }
        return ret;
    }
    public bool isGObjInSight(GameObject obj) //Объект в поле видимости этого объекта
    {
        List<GameObject> tmp = this.GetSightObjects();
        return tmp.Contains(obj);
    }
}
