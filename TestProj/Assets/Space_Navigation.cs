using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Space_Navigation : MonoBehaviour {

	// Use this for initialization
	private Vector3 C_2D_SCREEN = new Vector3(0,0,10);
	private LineRenderer _ref_pathRenderer;
	private RaycastHit2D _intersectPoint;
	private GameObject _tmpObstacle;
	private Vector2 _tmpIntentionalTarget, _tmp_P_T_collision, _tmp_T_P_collision;
	private Transform _ref_target;
	private float _obstacleRadius;

	public int dI = 10;

	List<Vector2> rPoint;
	/*struct point2d
	{
		float x;
		float y;
		public point2d(float _x, float _y)
		{
			x = _x;
			y = _y;
		}
		public static point2d operator +(point2d a, point2d b)
		{
			return new point2d(a.x + b.x, a.y + b.y);
		}
		public static point2d operator -(point2d a, point2d b)
		{
			return new point2d(a.x - b.x, a.y - b.y);
		}
		public static point2d operator /(point2d a, float b)
		{
			return new point2d(a.x /b, a.y /b);
		}
		public static point2d operator *(point2d a, float b)
		{
			return new point2d(a.x *b, a.y *b);
		}
		public Vector2 toVector2 ()
		{
			return new Vector2 (x, y);
		}
	};*/

	void Start () 
	{
		_ref_pathRenderer = gameObject.GetComponent<LineRenderer> ();
		_ref_target = GameObject.FindGameObjectWithTag ("Target").transform;
		rPoint = new List<Vector2> ();
	}
	Vector2 obstacleRound(Transform obst, float obstRadius, Vector2 inPoint, Vector2 outPoint)
	{
		Vector2 tmp = new Vector2 ((outPoint.x + inPoint.x)/2, (outPoint.y + inPoint.y)/2); // Центральная точка хорды кольца
		return (tmp - (Vector2)obst.position).normalized * obstRadius + (Vector2)obst.position;
	}
	void obstacleRoundRec(Transform obst, float obstRadius, Vector2 inPoint, Vector2 outPoint, int depth)
	{
		if (depth < 3) {
			Vector2 tmp = new Vector2 ((outPoint.x + inPoint.x) / 2, (outPoint.y + inPoint.y) / 2); // Центральная точка хорды кольца
			if(!rPoint.Contains((tmp - (Vector2)obst.position).normalized * obstRadius + (Vector2)obst.position))
			{
				rPoint.Add((tmp - (Vector2)obst.position).normalized * obstRadius + (Vector2)obst.position);
			}
			obstacleRoundRec(obst, obstRadius, outPoint, (tmp - (Vector2)obst.position).normalized * obstRadius + (Vector2)obst.position, depth+1);
			obstacleRoundRec(obst, obstRadius, inPoint, (tmp - (Vector2)obst.position).normalized * obstRadius + (Vector2)obst.position, depth+1);
			/*obstacleRoundRec(obst, obstRadius, (tmp - (Vector2)obst.position).normalized * obstRadius + (Vector2)obst.position, outPoint, depth+1);
			obstacleRoundRec(obst, obstRadius, (tmp - (Vector2)obst.position).normalized * obstRadius + (Vector2)obst.position, inPoint, depth+1);*/
		} else {
			Vector2 tmp = new Vector2 ((outPoint.x + inPoint.x) / 2, (outPoint.y + inPoint.y) / 2); // Центральная точка хорды кольца
			if(!rPoint.Contains((tmp - (Vector2)obst.position).normalized * obstRadius + (Vector2)obst.position))
			{
				rPoint.Add((tmp - (Vector2)obst.position).normalized * obstRadius + (Vector2)obst.position);
			}
		}
	}
	int Comp(Vector2 a, Vector2 b)
	{
		if (a.y < b.y)
			return -1;
		else
			return 1;
		if (a.y == b.y)
			return 0;
	}
	void Update () 
	{
		if(Input.GetMouseButton(0))
		{
			_tmpIntentionalTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition) + C_2D_SCREEN;
			_ref_target.position = _tmpIntentionalTarget;
			_intersectPoint = Physics2D.Linecast(transform.position, _tmpIntentionalTarget);
			if(_intersectPoint.collider != null)
			{
				_tmp_P_T_collision = _intersectPoint.point;
				_intersectPoint = Physics2D.Linecast(_tmpIntentionalTarget, transform.position);
				_tmp_T_P_collision = _intersectPoint.point;
				Vector2 prevP = new Vector2();
				_ref_pathRenderer.SetPosition(0,transform.position);
				_ref_pathRenderer.SetPosition(1, _tmp_P_T_collision);
				_ref_pathRenderer.SetPosition(2, prevP = obstacleRound(_intersectPoint.collider.gameObject.transform, 
				                                               _intersectPoint.collider.gameObject.GetComponent<CircleCollider2D>().radius, 
				                                               _tmp_P_T_collision,
				                                               _tmp_T_P_collision));
				_ref_pathRenderer.SetPosition(2, prevP = obstacleRound(_intersectPoint.collider.gameObject.transform, 
				                                                       _intersectPoint.collider.gameObject.GetComponent<CircleCollider2D>().radius, 
				                                                       _tmp_P_T_collision,
				                                                       _tmp_T_P_collision));

				_ref_pathRenderer.SetPosition(3, _tmp_T_P_collision);
				_ref_pathRenderer.SetPosition(4, _tmpIntentionalTarget);

				obstacleRoundRec(_intersectPoint.collider.gameObject.transform, 
				                 _intersectPoint.collider.gameObject.GetComponent<CircleCollider2D>().radius, 
				                 _tmp_P_T_collision,
				                 _tmp_T_P_collision, 0);

				rPoint.Add(_tmp_P_T_collision);
				rPoint.Add(_tmp_T_P_collision);
				rPoint.Sort(Comp);
				Vector3 p = new Vector3(-1, -1,-1);
				for(int i = 0 ; i < rPoint.Count; i++)
				{
					if(p == new Vector3(-1, -1, -1))
						p=rPoint[i];
					else
					{
						Debug.DrawLine(p, rPoint[i], Color.white);
						p = rPoint[i];
					}
				}
				rPoint.Clear();
				Debug.DrawLine(this.transform.position, _tmp_P_T_collision, Color.green);
				Debug.DrawLine(_tmp_T_P_collision, _tmpIntentionalTarget, Color.green);
			}
			else
				_ref_pathRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition) + C_2D_SCREEN);
		}
	}
}
