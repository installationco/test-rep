using UnityEngine;
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
        for (int i = 0; i < 360; i++) // 360/1 = 360
        {
            if (_tmpRaycastHit = Physics2D.Linecast(new Vector2(0, 0), _tmpRotateVector)) // Чтобы работало, нужно повесить на препятствие
            {                                                                             // коллайдер
                if (_tmpPrevCollision == false)
                    _sightLinePoints.Add(_tmpRaycastHit.point);
                _tmpPrevCollision = true;
                _tmpPrevRaycastHit = _tmpRaycastHit;
            }
            else
            {
                if (_tmpPrevCollision == true)
                    _sightLinePoints.Add(_tmpPrevRaycastHit.point);
                _tmpPrevCollision = false;
            }
            _tmpRotateVector = MathToolkit.RotateVectorbyAngle(_tmpRotateVector, 1); // Поворачиваем вектор на 1 градус
        }
        foreach (Vector2 p in _sightLinePoints)             // Тестверсия, игрок по умолчанию стоит в (0,0)
            Debug.DrawLine(new Vector3(0, 0, 0), p*100.0F); // Удлиняем, для наглядности
        _sightLinePoints.Clear();
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
