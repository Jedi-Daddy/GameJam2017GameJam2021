using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ui
{
    public class SunScript : MonoBehaviour
    {
        private float dissapearNumber = 1.5f;
        private float Speed = 500f;
        public int Key;
        private Vector3 DefaultPosition;
        private Vector3 ActivePosition;
        private Vector3 Rotation;
        private float journeyLength;

        public bool needAppearAnimation;
        public bool needDissapearAnimation;
        private float startTime = Time.time;

        private RectTransform _rectTransform;

        public void Initialize(Vector3 activePosition, Vector3 rotation)
        {
            DefaultPosition = new Vector3(activePosition.x * dissapearNumber, activePosition.y * dissapearNumber);
            ActivePosition = activePosition;
            Rotation = rotation;
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.localPosition = DefaultPosition;
            _rectTransform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

            journeyLength = Vector3.Distance(activePosition, DefaultPosition);
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
            //var amountToMove = Speed * Time.deltaTime;
            //_rectTransform.transform.Translate(ActivePosition * amountToMove);
            //_rectTransform.localPosition = ActivePosition; 
            
            needAppearAnimation = true;
            startTime = Time.time;
        }
        public void Dissapear()
        {
            //var amountToMove = Speed * Time.deltaTime;
            //transform.Translate(Vector3.forward * amountToMove);
            //_rectTransform.transform.Translate(DefaultPosition * amountToMove);
            //_rectTransform.localPosition = DefaultPosition;

            needDissapearAnimation = true;
            startTime = Time.time;
        }

        public void SetDefaultPosition(Vector3 pos)
        {
            _rectTransform.localPosition = pos;
        }
    }
}