using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTextGenerator : MonoBehaviour
{

    private GameObject hoverTextInstance;

    public void GenerateHoverText(string text)
    {
        hoverTextInstance = Instantiate(Resources.Load<GameObject>("Prefabs/HoverTextBox"), GameObject.FindObjectOfType<Canvas>().transform);
        RectTransform hoverTextRectTransform = (RectTransform)hoverTextInstance.transform;

        SetOffsetPosition(true, hoverTextInstance);
        if (!hoverTextRectTransform.IsFullyVisibleFrom(FindObjectOfType<Camera>())) {
            SetOffsetPosition(false, hoverTextInstance);
        }

        hoverTextInstance.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = text;
    }

    private void SetOffsetPosition(bool right, GameObject hoverText)
    {
        Vector3[] corners = new Vector3[4];
        ((RectTransform)gameObject.transform).GetWorldCorners(corners);

        Vector3[] hoverTextCorners = new Vector3[4];
        ((RectTransform)hoverText.transform).GetWorldCorners(hoverTextCorners);

        //Used to create space between the spawner and the textbox
        float offset = Mathf.Abs((hoverTextCorners[2] - hoverTextCorners[0]).x) / 2;
        offset += .1f; //Flat amount
        float xCorner = right ? corners[2].x + offset : corners[0].x - offset;
        print(xCorner);

        Vector3 finalPos = new Vector3(xCorner, gameObject.transform.position.y);

        hoverTextInstance.transform.SetPositionAndRotation(finalPos, Quaternion.identity);
    }

    public void DestroyHoverText()
    {
        Destroy(hoverTextInstance.gameObject);
    }
}
