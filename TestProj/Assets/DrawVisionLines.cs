using UnityEngine;
using System.Collections;

public class DrawVisionLines : MonoBehaviour {

    Transform[] Vertices = new Transform[10]; //Массив вершин
    int countOfVertices;
    public GameObject Player;
    Vector2 lastPlayerPosition;
    Vector2 playerPosition;
    float maxAngle = -5;
    float minAngle = 5;

	void Start () 
    {
        countOfVertices = this.transform.GetChildCount();
        for(int i = 0; i < countOfVertices; i++)
        {
            Vertices[i] = this.transform.GetChild(i); //Получаем вершины
        }
        //Player = GameObject.FindGameObjectWithTag("Player");
	}
	
    void OnGUI()
    {
        GUI.Label(new Rect(100, 100, 100, 100), minAngle.ToString());
        GUI.Label(new Rect(100, 80, 100, 100), maxAngle.ToString());
        Debug.DrawLine(Player.transform.position, new Vector2(1, 1), Color.black, 15, true);
    }

	void Update () 
    {
        playerPosition = Player.transform.position;//Потом изменить на относительные координаты
        //if (lastPlayerPosition.x != Player.transform.position.x || lastPlayerPosition.y != Player.transform.position.y)
        //{ //Строим новые прямые, если изменилось положение игрока (относительно препятствия)
            minAngle = 5;
            maxAngle = -5;
            for(int i = 0; i < countOfVertices; i++)
            {
                float angle;
                if (Vertices[i].position.x - playerPosition.x == 0) 
                    angle = Mathf.PI / 2;
                else
                    angle = Mathf.Atan((Vertices[i].position.y - playerPosition.y) / (Vertices[i].position.x - playerPosition.x));
                if (Vertices[i].position.x < 0)
                {
                    angle = Mathf.PI - angle;
                }
                if(angle < 0)
                {
                    angle = 2 * Mathf.PI + angle;
                } // находим наклоны прямых, проходящих через игрока и все вершины препятствия
                if (angle < minAngle)
                    minAngle = angle; //выбираем самую высокую и самую низкую прямые
                if (angle > maxAngle)
                    maxAngle = angle;
            }
            Debug.DrawLine(Player.transform.position, new Vector2(Player.transform.position.x + Mathf.Cos(minAngle), Player.transform.position.y + Mathf.Sign(minAngle)), Color.black, 15, false);
            Debug.DrawLine(Player.transform.position, new Vector2(Player.transform.position.x + Mathf.Cos(maxAngle), Player.transform.position.y + Mathf.Sign(maxAngle)), Color.black, 15, false);
        //}

       // lastPlayerPosition = Player.transform.position;

        Debug.DrawLine(Player.transform.position, new Vector2(1, 1), Color.black, 15, true);
	}
}
