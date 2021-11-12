using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ui
{
    public class SunManager : MonoBehaviour
    {
        public Dictionary<int, SunScript> SunCollection;

        public Dictionary<int, Vector3> positions = new Dictionary<int, Vector3>
        {
            {1, new Vector3(-738, -406, 0)},
            {2, new Vector3(-856, 0, 0)},
            {3, new Vector3(-738, 432, 0)},
            {4, new Vector3(768, 418, 0)},
            {5, new Vector3(862, -12, 0)},
            {6, new Vector3(768, -437, 0)},
            {7, new Vector3(-10, -430, 0)},
        };

        public Dictionary<int, Vector3> rotation = new Dictionary<int, Vector3>
        {
            {1, new Vector3(0f, 0f, 48f)},
            {2, new Vector3(0f, 0f, 0f)},
            {3, new Vector3(0f, 0f, -20f)},
            {4, new Vector3(0f, -180f, -48f)},
            {5, new Vector3(0f, -180f, 0f)},
            {6, new Vector3(0f, -180f, 20f)},
            {7, new Vector3(0f, 0f, 100f)},
        };

        public int currentPosition;
        public int startPosition = 1;
        public bool enableListen;

        void Start()
        {
            SunCollection = new Dictionary<int, SunScript>();
            var suns = GetComponentsInChildren<SunScript>();
            foreach (var sun in suns)
            {
                SunCollection.Add(sun.Key, sun);
                sun.Initialize(positions[sun.Key], rotation[sun.Key]);

            }
            SunCollection[startPosition].SetDefaultPosition(positions[startPosition]); 
            currentPosition = startPosition;
            enableListen = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && enableListen)
            {
                var normalizedMousePosition = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0);

                var nextPosition = GetNextPosition(normalizedMousePosition);
                SunCollection[currentPosition].Dissapear();
                SunCollection[nextPosition].Appear();
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