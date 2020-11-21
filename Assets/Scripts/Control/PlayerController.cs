using RPG.Combat;
using RPG.Movement;
using UnityEngine;
using RPG.Attributes;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System.Collections;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
            //
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavMeshProjectionDistance = 1f;
        [SerializeField] float raycastRadius = 1f;

        private void Awake()
        {
            health = GetComponent<Health>();
        }
        void Update()
        {
            if (InteractWithUI()) return;

            if (health.IsDead())
            {
                SetCursor(CursorType.None);
                
                //StartCoroutine(GoToDeadScreenCoroutine());
                //RemovePersistentObjects();
                return;
            }

            //Script nuevo
            if (BlockInteract()) return;

            if (BlockInteract()) return;
            if (InteractWithComponent()) return;
            if (InteractWithMovement()) return;

            SetCursor(CursorType.None);

        }

        private bool BlockInteract()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.tag == "Trap")
                {
                    //print("Bloqueando");
                    return true;

                }
            }

            return false;
        }

        private bool InteractWithUI()
        {
            //is this over a UI gameObject? regresa un booleano
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                print("Over UI");
                return true;
            }
            return false;
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach(IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRayCast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);

            float[] distances = new float[hits.Length];

            for(int i = 0; i<hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }

            Array.Sort(distances, hits);

            return hits;
        }


        private bool InteractWithMovement()
        {
            
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);

            if (hasHit)
            {
                if (!GetComponent<Mover>().CanMoveTo(target)) return false;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(target, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();

            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHit) return false;

            NavMeshHit navMeshHit;
            bool hasCastToNavMEsh = NavMesh.SamplePosition(
                hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);
            if (!hasCastToNavMEsh) return false;

            target = navMeshHit.position;

            return true;
        }
       

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if(mapping.type == type)
                {
                    return mapping;
                }             
            }
            return cursorMappings[0];
        }
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        IEnumerator GoToDeadScreenCoroutine()
        {
            

            yield return new WaitForSeconds(5);
            SceneManager.LoadScene("Muerte");
        }

        private void RemovePersistentObjects()
        {
            foreach (GameObject o in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {
                Destroy(o);
            }


            //GameObject persistentObjects;
            //persistentObjects = GameObject.Find("Saving");
            //persistentObjects.GetComponent<SceneManagement.SavingWrapper>().enabled = false;
            //persistentObjects.SetActive(false);
            //Destroy(persistentObjects);
        }
    }
}
