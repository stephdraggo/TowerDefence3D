using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDefense.Managers
{
    public class PlatformManager : MonoBehaviour
    {
        [SerializeField]
        GameObject TowerPrefab;

        private void OnMouseUpAsButton()
        {
            Instantiate(TowerPrefab, transform.position, Quaternion.identity);
            Debug.Log("Ouch Stop pressing me");
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
