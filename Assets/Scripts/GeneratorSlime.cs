using System.Collections;
using UnityEngine;

public class GeneratorSlime : MonoBehaviour
{
    public float RadiusRespawn = 1f;
    public GameObject Slime;

    private GameObject _target;

    public int Count = 1;

    // Use this for initialization
    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        
        if (!_target) return;
        
        StartCoroutine(GenerateSlime());
    }


    // Update is called once per frame

    private IEnumerator GenerateSlime()
    {
        for (int i = 0; i < Count; i++)
        {
            GameObject slimeObj = Instantiate(Slime, transform);

            SlimeController slimeController = slimeObj.GetComponent<SlimeController>();
            slimeController.Target = _target;

            Vector3 posVector3 = transform.position;
            posVector3.x += Random.Range(-RadiusRespawn, RadiusRespawn);
            posVector3.z += Random.Range(-RadiusRespawn, RadiusRespawn);
            slimeObj.transform.position = posVector3;

            yield return new WaitForSeconds(2);
        }
    }
}