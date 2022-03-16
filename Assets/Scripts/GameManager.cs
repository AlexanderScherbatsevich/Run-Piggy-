using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GameManager : MonoBehaviour
{
    public static GameManager S;
    public Transform navMesh;
    public GameObject prefabStone;
    public TextAsset layoutXML;
    public Layout layout;
    public Transform layoutAnchor;
    public List <Vector2> freeSpot;

    private NavMeshSurface2d navMeshSurface2D;
    private Vector3 _layoutCenter;
    private int _rowCount = 4, _columnCount = 8;
    private Vector2 _firstPosStone = new Vector2(-13.25f, -5.7f);
    private Vector2 _multiplier = new Vector2(3.55f, 3.5f);

    private void Awake()
    {
        S = this;
        FreeSpotOnGround();
    }
    private void Start()
    {
        navMeshSurface2D = GetComponent<NavMeshSurface2d>();
        
        layout = GetComponent<Layout>();
        layout.ReadLayout(layoutXML.text);
        //LayoutGame();
    }

    void LayoutGame()
    {
        //создать пустой игровой объект
        if (layoutAnchor == null)
        {
            GameObject tGO = new GameObject("_LayoutAnchor");
            tGO.transform.SetParent(navMesh, true);
            layoutAnchor = tGO.transform;
        }

        //разложить камни
        foreach (SlotDef tSD in layout.slotDefs)
        {
            //prefabStone.transform.parent = layoutAnchor;

            //GameObject go = Instantiate<GameObject>(prefabStone);
            //go.transform.SetParent(layoutAnchor, true);
            //Vector2 pos = Vector2.zero;
            //pos.x = tSD.x;
            //pos.y = tSD.y;
            //go.transform.position = pos;

            //prefabStone.transform.localPosition = new Vector2(tSD.x, tSD.y);  //установить позицию в соответствии с SlotDef

        }

        Vector2 tPos = _firstPosStone;
        for (int i = 0; i < _rowCount; i++)
        {
            tPos = _firstPosStone;
            _firstPosStone.y += _multiplier.y;
            _firstPosStone.x += 0.3f;
            for (int j = 0; j < _columnCount; j++)
            {
                GameObject go = Instantiate<GameObject>(prefabStone);
                go.transform.SetParent(layoutAnchor, true);
                Vector2 pos = Vector2.zero;
                pos.x = tPos.x;
                tPos.x += _multiplier.x;
                pos.y = tPos.y;
                go.transform.position = pos;
            }
        }
    }
    public List<Vector2> FreeSpotOnGround()
    {

        Vector2 firstFreeSpot = _firstPosStone - (_multiplier / 2f);
        Vector2 tPos = firstFreeSpot;
        for (int i = 0; i < 5; i++)
        {
            tPos = firstFreeSpot;
            firstFreeSpot.y += _multiplier.y;

            firstFreeSpot.x += 0.3f;
            for (int j = 0; j < 9; j++)
            {
                freeSpot.Add(tPos);

                //GameObject go = Instantiate<GameObject>(prefabStone);
                //go.transform.position = tPos;

                tPos.x += _multiplier.x;                             
            }
        }
        return freeSpot;
    }
}
