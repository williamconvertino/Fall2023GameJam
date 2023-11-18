using System.Collections.Generic;
using UnityEngine;

namespace Player.Input.ButtonPress
{
    public abstract class PlayerButtonPress : MonoBehaviour
    {
        protected KeyCode[] KeyCodeList;

        private void Start()
        {
            PopulateKeyCodeList();
        }

        protected abstract void PopulateKeyCodeList();

        public bool ButtonPressed()
        {
            foreach (KeyCode keyCode in KeyCodeList)
            {
                if (UnityEngine.Input.GetKeyDown(keyCode))
                {
                    return true;
                }
            }

            return false;
        }
    }
}