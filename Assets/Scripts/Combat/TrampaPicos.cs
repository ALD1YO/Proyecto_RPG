using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;


namespace RPG.Combat
{
    public class TrampaPicos : MonoBehaviour
    {
        [SerializeField] float wakeupTime = 0.0f;
        [SerializeField] float activeTime = 5.0f;
        [SerializeField] float inactiveTime = 5.0f;
        [SerializeField] float emergingDistance;
        [SerializeField] float trapDamage;

        Health target;

        private bool isTrapOn;

        private void Start()
        {
            StartCoroutine(WakeupTime());
        }
       
        public void Posicion(bool isItOn)
        {
            var pos = transform.position;

            if (isItOn)
            {
                transform.position = new Vector3(pos.x, pos.y + emergingDistance, pos.z);
            }
            else
            if (!isItOn)
            {
                transform.position = new Vector3(pos.x, pos.y - emergingDistance, pos.z);
            }


        }

        private IEnumerator WakeupTime()
        {
            yield return new WaitForSeconds(wakeupTime);
            StartCoroutine(SecuenciaTrampa());
        }

        private IEnumerator SecuenciaTrampa()
        {
            yield return new WaitForSeconds(inactiveTime);
            isTrapOn = true;
            Posicion(isTrapOn);

            yield return new WaitForSeconds(activeTime);
            isTrapOn = false;
            Posicion(isTrapOn);

            StartCoroutine(SecuenciaTrampa());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() == null) return;
            target = other.GetComponent<Health>();
            if (target.IsDead()) return;

            target.TakeDamage(gameObject, trapDamage);

        }
    }

}
