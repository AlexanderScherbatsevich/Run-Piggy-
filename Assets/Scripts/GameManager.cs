using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject prefabStone;
    public TextAsset layoutXML;
    public Layout layout;
    public Transform layoutAnchor;
    public Vector3 layoutCenter;

    private int _rowCount = 4, _columnCount = 8;
    Vector2 _firstPosStone = new Vector2(-13.25f, -5.7f);
    Vector2 _multiplier = new Vector2(3.55f, 3.5f);
    private void Start()
    {
        layout = GetComponent<Layout>();
        layout.ReadLayout(layoutXML.text);
        LayoutGame();
    }

    void LayoutGame()
    {
        //создать пустой игровой объект
        if (layoutAnchor == null)
        {
            GameObject tGO = new GameObject("_LayoutAnchor");
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
    public void FreeSpotOnGround()
    {

    }
}
