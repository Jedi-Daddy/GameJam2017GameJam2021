using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ui
{
    public class SunScript : MonoBehaviour
    {
        private float dissapearNumber = 2f;
        public int Key;
        private Vector3 DefaultPosition;
        private Vector3 ActivePosition;
        private Vector3 Rotation;

        private RectTransform _rectTransform;

        public void Initialize(Vector3 defaultposition, Vector3 activePosition, Vector3 rotation)
        {
            //DefaultPosition = defaultposition;
            DefaultPosition = new Vector3(activePosition.x * dissapearNumber, activePosition.y * dissapearNumber);
            ActivePosition = activePosition;
            Rotation = rotation;
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.localPosition = DefaultPosition;
            _rectTransform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        }
        public void Appear()
        {
            _rectTransform.localPosition = ActivePosition;
        }
        public void Dissapear()
        {
            _rectTransform.localPosition = DefaultPosition;
        }
    }
}