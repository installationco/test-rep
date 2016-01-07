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
    public int viewAngle = 0;                                 //Угол поворота персонажа
    public int maxPossibleAngle = 80;                           //Возможный угол обзора персонажа
    private const float _lengthMultiplier = 20;

    void Start()
    {
        objInSight = new List<GameObject>();
        _sightLinePoints = new List<Vector2>();
    }
    void Update()
    {
        _tmpPrevCollision = false;

        for (int i = viewAngle - maxPossibleAngle; i < viewAngle + maxPossibleAngle + 1; i += 1)
        {
            if (_tmpRaycastHit = Physics2D.Linecast(transform.position, MathToolkit.VectorbyPointAngle(transform.position, i, _lengthMultiplier))) // Чтобы работало, нужно повесить на препятствие
            {
                if (_tmpPrevCollision == false)
                    Debug.DrawLine(transform.position, MathToolkit.VectorbyPointAngle(transform.position, i, _lengthMultiplier));
                _tmpPrevCollision = true;
            }
            else
            {
                if (_tmpPrevCollision == true)
                {
                    Debug.DrawLine(transform.position, MathToolkit.VectorbyPointAngle(transform.position, i, _lengthMultiplier));
                    _tmpPrevCollision = false;
                }
                if (i == viewAngle - maxPossibleAngle || i == viewAngle + maxPossibleAngle)
                    Debug.DrawLine(transform.position, MathToolkit.VectorbyPointAngle(transform.position, i, _lengthMultiplier), Color.magenta);
            }
        }
        Debug.DrawLine(transform.position, MathToolkit.VectorbyPointAngle(transform.position, viewAngle, _lengthMultiplier), Color.red);
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
