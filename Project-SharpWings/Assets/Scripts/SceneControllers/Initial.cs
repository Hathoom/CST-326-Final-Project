using TMPro;
using UnityEngine;

namespace SceneControllers
{
    public class Initial : MonoBehaviour
    {
        private char _initial;
        private TextMeshProUGUI _initialText;

        private void Awake()
        {
            _initialText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            _initial = _initialText.text[0];
        }

        public void UpInitial()
        {
            _initial = (char) ((_initial + 1) % 90 + 65);
            UpdateInitial();
        }

        public void DownInitial()
        {
            _initial = (char) ((_initial - 1) % 90 + 65);
            UpdateInitial();
        }

        private void UpdateInitial() => _initialText.text = _initial.ToString();
        
        public char GetInitial() => _initial;
    }
}
