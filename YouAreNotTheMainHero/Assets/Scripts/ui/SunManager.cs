using System;
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
            {0, new Vector3(-960, -530, 0)},
            {1, new Vector3(-960, 20, 0)},
            {2, new Vector3(-960, 530, 0)},
            {3, new Vector3(960, 530, 0)},
            {4, new Vector3(960, 20, 0)},
            {5, new Vector3(960, -530, 0)},
        };

        public int currentPosition;
        public int startPosition;
        public bool enableListen;

        public bool IsClockwise;

        public UnityEvent<int> ShadowScript;

        private void OnEnable()
        {
            EventDispatcher.OnSunLock += OnSunLock;
            EventDispatcher.OnSunUnlock += OnSunUnlock;
        }

        private void OnDisable()
        {
            EventDispatcher.OnSunLock -= OnSunLock;
            EventDispatcher.OnSunUnlock -= OnSunUnlock;
        }

        void Start()
        {
            SunCollection = new Dictionary<int, SunScript>();
            var suns = GetComponentsInChildren<SunScript>();
            foreach (var sun in suns)
            {
                SunCollection.Add(sun.Key, sun);
                sun.Initialize(positions[sun.Key]);

            }
            SunCollection[startPosition].SetDefaultPosition(positions[startPosition]); 
            currentPosition = startPosition;
            EventDispatcher.OnSunStarted(this, new IntEventArgs(startPosition));
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && enableListen)
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
                IsClockwise = true;
                if (currentPosition == 5)
                    return 0;
                return currentPosition + 1;
            }
            else
            {
                IsClockwise = false;
                if (currentPosition == 0)
                    return 5;
                return currentPosition - 1;

            }
        }

        public void OnSunLock(object sender, EventArgs args)
        {
            SunCollection[currentPosition].Lock.SetActive(true);
            enableListen = false;
        }

        public void OnSunUnlock(object sender, EventArgs args)
        {
            SunCollection[currentPosition].Lock.SetActive(false);
            enableListen = true;
        }
    }
}