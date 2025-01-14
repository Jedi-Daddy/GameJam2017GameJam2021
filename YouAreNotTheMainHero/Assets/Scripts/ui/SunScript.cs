﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ui
{
    public class SunScript : MonoBehaviour
    {
        private float dissapearNumber = 1.5f;
        public float Speed = 800f;
        public int Key;
        private Vector3 DefaultPosition;
        private Vector3 ActivePosition;
        private float journeyLength;

        public bool needAppearAnimation;
        public bool needDissapearAnimation;
        private float startTime;

        private RectTransform _rectTransform;

        public GameObject Lock;

        public void Initialize(Vector3 activePosition)
        {
            DefaultPosition = new Vector3(activePosition.x * dissapearNumber, activePosition.y * dissapearNumber);
            ActivePosition = activePosition;
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.localPosition = DefaultPosition;
            journeyLength = Vector3.Distance(activePosition, DefaultPosition);
            Lock.SetActive(false);
        }

        public void Update()
        {
            if (needAppearAnimation)
            {
                float distCovered = (Time.time - startTime) * Speed; 
                float fractionOfJourney = distCovered / journeyLength;
                _rectTransform.localPosition = Vector3.Lerp(DefaultPosition, ActivePosition, fractionOfJourney);
                if (_rectTransform.localPosition == ActivePosition)
                    needAppearAnimation = false;
            }
            if (needDissapearAnimation)
            {
                float distCovered = (Time.time - startTime) * Speed;
                float fractionOfJourney = distCovered / journeyLength;
                _rectTransform.localPosition = Vector3.Lerp(ActivePosition, DefaultPosition, fractionOfJourney);
                if (_rectTransform.localPosition == DefaultPosition)
                    needDissapearAnimation = false;

            }
        }


        public void Appear()
        {
            needAppearAnimation = true;
            startTime = Time.time;
        }
        public void Dissapear()
        {
            needDissapearAnimation = true;
            startTime = Time.time;
        }

        public void SetDefaultPosition(Vector3 pos)
        {
            _rectTransform.localPosition = pos;
        }
    }
}