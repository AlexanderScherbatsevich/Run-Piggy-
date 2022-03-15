using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System.Xml;

[System.Serializable]
public class SlotDef
{
    public float x;
    public float y;
    public int id;
}

public class Layout : MonoBehaviour
{
    public List<SlotDef> slotDefs;
    //private int _column = 8;
    //private int _row = 4;

    public PT_XMLReader xmlr;
    public PT_XMLHashtable xml;  //используется для ускорения доступа к xml

    public void ReadLayout(string xmlText)
    {
        NumberFormatInfo formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
        xmlr = new PT_XMLReader();
        xmlr.Parse(xmlText);  //загрузить XML
        xml = xmlr.xml["xml"][0];  //определить xml для ускорения доступа к XML

        PT_XMLHashList slotsX = xml["slot"];
        SlotDef tSD;

        for (int i = 0; i < slotsX.Count; i++)
        {
            tSD = new SlotDef();

            //преобразовать некоторые атрибуты в числовые значения
            tSD.x = float.Parse(slotsX[i].att("x"), formatter);
            tSD.y = float.Parse(slotsX[i].att("y"), formatter);
            tSD.id = int.Parse(slotsX[i].att("id"));
            slotDefs.Add(tSD);
        }
    }
}
