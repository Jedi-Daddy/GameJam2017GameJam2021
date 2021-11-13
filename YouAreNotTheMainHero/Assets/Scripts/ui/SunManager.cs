using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.ui
{
    public class SunManager : MonoBehaviour
    {
        public Dictionary<int, SunScript> SunCollection;

        public Dictionary<int, Vector3> positions = new Dictionary<int, Vector3>
        {
            {0, new Vector3(-738, -406, 0)},
            {1, new Vector3(-856, 0, 0)},
            {2, new Vector3(-738, 432, 0)},
            {3, new Vector3(10, 430, 0)},
            {4, new Vector3(768, 418, 0)},
            {5, new Vector3(862, -12, 0)},
            {6, new Vector3(768, -437, 0)},
        };

        public Dictionary<int, Vector3> rotation = new Dictionary<int, Vector3>
        {
            {0, new Vector3(0f, 0f, 48f)},
            {1, new Vector3(0f, 0f, 0f)},
            {2, new Vector3(0f, 0f, -20f)},
            {3, new Vector3(0f, 0f, -100f)},
            {4, new Vector3(0f, -180f, -48f)},
            {5, new Vector3(0f, -180f, 0f)},
            {6, new Vector3(0f, -180f, 20f)},
        };

        public int currentPosition;
        public int startPosition;
        public bool enableListen;

        public UnityEvent<int> ShadowScript;

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
            EventDispatcher.OnSunUpdated(this, new IntEventArgs(startPosition));
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var normalizedMousePosition = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0);

                var nextPosition = GetNextPosition(normalizedMousePosition);
                SunCollection[currentPosition].Dissapear();
                SunCollection[nextPosition].Appear();
                EventDispatcher.OnSunUpdated(this, new IntEventArgs(nextPosition));
                currentPosition = nextPosition;
            }
        }

        int GetNextPosition(Vector3 mousePos)
        {
            if (mousePos.x >= 0.5)
            {
                if (currentPosition == 6)
                    return 0;
                return currentPosition + 1;
            }
            else
            {
                if (currentPosition == 0)
                    return 6;
                return currentPosition - 1;

            }
        }
    }
}