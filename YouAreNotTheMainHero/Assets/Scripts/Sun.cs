using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Sun : MonoBehaviour
    {
        public Dictionary<int, Vector3> positions = new Dictionary<int, Vector3>
        {
            {1, new Vector3(-738, -406, 0)},
            {2, new Vector3(-856, 0, 0)},
            {3, new Vector3(-738, 432, 0)},
            {4, new Vector3(768, 418, 0)},
            {5, new Vector3(862, -12, 0)},
            {6, new Vector3(768, -437, 0)},
            {7, new Vector3(-8, -406, 0)},
        }; 
        
        public Dictionary<int, Vector3> rotation = new Dictionary<int, Vector3>
        {
            {1, new Vector3(0, 0, 48f)},
            {2, new Vector3(0, 0, 0)},
            {3, new Vector3(0, 0, -20f)},
            {4, new Vector3(0, -180f, -48)},
            {5, new Vector3(0, -180f, 0)},
            {6, new Vector3(0, -180f, 20f)},
            {7, new Vector3(0, 0, 100f)},
        };

        public int currentPosition;
        public int startPosition=1;
        private RectTransform _rectTransform;
        public bool enableListen;


        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.localPosition = positions[startPosition];
            _rectTransform.rotation = Quaternion.Euler(rotation[startPosition].x, rotation[startPosition].y, rotation[startPosition].z);
            currentPosition = startPosition;
            enableListen = false;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && enableListen)
            {
                var normalizedMousePosition = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0);

                var nextPosition = GetNextPosition(normalizedMousePosition);
                _rectTransform.localPosition = positions[nextPosition];
                _rectTransform.rotation = Quaternion.Euler(rotation[nextPosition].x, rotation[nextPosition].y, rotation[nextPosition].z);
                currentPosition = nextPosition;
            }
        }

        int GetNextPosition(Vector3 mousePos)
        {
            if (mousePos.x >= 0.5)
            {
                if (currentPosition == 7)
                    return 1;
                return currentPosition + 1;
            }
            else
            {
                if (currentPosition == 1)
                    return 7;
                return currentPosition - 1;

            }
        }
    }
}