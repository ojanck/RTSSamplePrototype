using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LP.FDG.InputManager
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance;
        private RaycastHit hit;
        private List<Transform> selectedUnit = new List<Transform>();
        private bool isDragging = false;
        private Vector3 mousePos;

        void Start()
        {
            instance = this;
        }            

        private void OnGui()
        {
            if (isDragging)
            {
                Rect rect = MultiSelect.GetScreenRect(mousePos, Input.mousePosition);
                MultiSelect.DrawScreenRect(rect, new Color(0f, 0f, 0f, 0.25f));
                MultiSelect.DrawScreenRectBorder(rect, 3, Color.blue);
            }
        }

        public void HandleUnitMovement()
        {            
            mousePos = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out hit))
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    switch (layerHit.value)
                    {
                        case 8:
                            SelectUnit(hit.transform, Input.GetKey(KeyCode.LeftShift));
                            break;
                        default:
                            isDragging = true;
                            DeselectUnit();
                            break;
                    }
                }
            }

            if (!Input.GetMouseButtonUp(0))
            {
                foreach (Transform child in Player.PlayerManager.instance.playerUnits)
                {
                    foreach (Transform unit in child)
                    {
                        if (isWithinSelectionBounds(unit))
                        {
                            SelectUnit(unit, true);
                        }
                    }
                }
                isDragging = false;
            }
        }

        private void SelectUnit(Transform unit, bool canMultiselect = false)
        {
            if(!canMultiselect)
            {
                DeselectUnit();
            }
            selectedUnit.Add(unit);
            unit.Find("Highlight").gameObject.SetActive(true);
        }

        private void DeselectUnit()
        {
            for (int i = 0; i < selectedUnit.Count; i++)
            {
                selectedUnit[i].Find("Highlight").gameObject.SetActive(false);
            }
            selectedUnit.Clear();

        }

        private bool isWithinSelectionBounds(Transform tf)
        {
            if(isDragging)
            {
                return false;
            }

            Camera cam = Camera.main;
            Bounds vpBounds = MultiSelect.GetVPBounds(cam, mousePos, Input.mousePosition);
            return vpBounds.Contains(cam.WorldToViewportPoint(tf.position));
        } 

    }    
}


