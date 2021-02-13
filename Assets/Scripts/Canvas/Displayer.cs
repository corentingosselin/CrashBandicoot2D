using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Canvas
{
    public class Displayer : MonoBehaviour
    {
        protected Text displayText;

        private void Start()
        {
            displayText = GetComponent<Text>();
            
        }

        private int direction = -1;
        private bool isMoving = false;
        private bool prevent = false;
        private int maxVectorCanvas = 25;
        private int countVectorCanvas = 0;
        
        private void Update()
        {
            if (isMoving)
            {
                MoveCanvas();
            }
        }


        public void DisplayInfo(int amount)
        {
            displayText.text = amount.ToString();
            if (isMoving || prevent)  return;
            direction = -1;
            isMoving = true;
            prevent = true;
            StartCoroutine(ResetCanvas());
        }


        private IEnumerator ResetCanvas()
        {
            yield return new WaitForSeconds(3F);
            direction = 1;
            isMoving = true;
        }
        
        
        //make the canvas going down
        private void MoveCanvas()
        {
            displayText.rectTransform.position = displayText.transform.TransformPoint(0,direction * countVectorCanvas,0);
            countVectorCanvas++;
            if (countVectorCanvas >= maxVectorCanvas)
            {
                countVectorCanvas = 0;
                isMoving = false;
                if (direction == 1)
                    prevent = false;
            }

        }
    }
}