using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverTextGenerator : MonoBehaviour
{
    private GameObject hoverTextInstance;

    private bool initVal = false;
    private object lockObj = new object { };

    /// <summary>
    /// Used to keep track of if we are waiting to open up the hover text
    /// </summary>
    private bool initializing {
        get
        { lock (lockObj)
            {
                return initVal;
            }
        }
        set
        {
            lock(lockObj)
            {
                initVal = value;
            }
        }
    }

    public void GenerateKeywordHoverText(TextMeshProUGUI textBox)
    {
        if (!initializing && !hoverTextInstance)
        {
            initializing = true;
            StartCoroutine(GenerateIfStillHovering(textBox));
        }
    }

    private IEnumerator GenerateIfStillHovering(TextMeshProUGUI textBox)
    {
        float durationOfWait = .7f;
        yield return new WaitForSeconds(durationOfWait);
        if(initializing)
        {
            string hoverText = "";
            List<string> foundKeywords = new List<string>();
            foreach (string word in textBox.text.Split(' '))
            {
                //Remove punctuation to avoid missing a match
                string sanitizedWord = word.Replace(",", "");
                sanitizedWord = word.Replace(".", "");
                sanitizedWord = word.Replace("\n", "");

                //No duplicates
                if(foundKeywords.Contains(sanitizedWord))
                {
                    continue;
                }

                string meaning;
                if (Glossary.Keywords.TryGetValue(sanitizedWord.ToLower(), out meaning))
                {
                    
                    if (hoverText != "")
                    {
                        hoverText += "\n\n";
                    }

                    hoverText += meaning;

                    foundKeywords.Add(sanitizedWord);
                }
            }

            GenerateHoverText(hoverText);
        }
    }

    public void GenerateHoverText(string text)
    {
        if (text != null && !text.Equals(""))
        {
            if(hoverTextInstance)
            {
                Destroy(hoverTextInstance);
            }
            
            hoverTextInstance = Instantiate(Resources.Load<GameObject>("Prefabs/HoverTextBox"), GameObject.Find("Canvas").transform);
            RectTransform hoverTextRectTransform = (RectTransform)hoverTextInstance.transform;
            //TODO
            Rect originatorRect = ((RectTransform)gameObject.transform).rect;
            hoverTextRectTransform.sizeDelta = originatorRect.size;

            SetOffsetPosition(true, hoverTextInstance);
            if (!hoverTextRectTransform.IsFullyVisibleFrom(FindObjectOfType<Camera>()))
            {
                SetOffsetPosition(false, hoverTextInstance);
            }

            hoverTextInstance.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = text;
        }
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

        Vector3 finalPos = new Vector3(xCorner, gameObject.transform.position.y);

        hoverTextInstance.transform.SetPositionAndRotation(finalPos, Quaternion.identity);
    }

    public void DestroyHoverText()
    {
        initializing = false;

        if (hoverTextInstance?.gameObject)
        {
            Destroy(hoverTextInstance.gameObject);
        }
    }
}
