using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyFinder : MonoBehaviour
{
    [SerializeField]
    private appearance Cloth;
    public ParticleSystem boooooom;
    private Camera Cam;
    private Ray RayMouse;
    GameObject[] enemies;
    public GameObject sphere;
    public GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        portal.transform.localScale = new Vector3(1.2f, 0.1f, 1.2f);
        portal.SetActive(false);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        /* for (int i = 0; i < enemies.Length; i++){
             enemies[i].SetActive(false);
         }*/
        Cam = GetComponent<Camera>();
        sphere.SetActive(false);
    }
    public void WizardMod()
    {
        Vector3 vec = GameObject.FindGameObjectWithTag("Player").transform.position;
        Instantiate(boooooom.gameObject, new Vector3(vec.x,vec.y+1,vec.z), Quaternion.identity);
        sphere.SetActive(true);
        Cloth.WizardMod();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Cam != null)
            {
                RaycastHit hit;
                var mousePos = Input.mousePosition;
                RayMouse = Cam.ScreenPointToRay(mousePos);

                if (Physics.Raycast(RayMouse.origin, RayMouse.direction, out hit, 40))
                {
                    if (hit.collider.tag == "Enemy")
                    {
                        hit.collider.GetComponent<HideEnemyWillFind>().SetDestin();
                        hit.collider.GetComponent<HideEnemyWillFind>().anim.SetBool("Run", true);
                        FindObjectOfType<FoundEnemies>().GetComponent<FoundEnemies>().enabledd = false;
                        StartCoroutine(ChangeScalePortal());
                    }
                    
                }
            }
        }
    }            // Используем сопрограмму для загрузки сцены в фоновом режиме

    IEnumerator ChangeScalePortal()
    {
        Vector3 vec = new Vector3(1.2f, 1.2f, 1.2f);

        portal.SetActive(true);
        while (portal.transform.localScale != vec)
        {
            portal.transform.localScale = Vector3.Lerp(portal.transform.localScale, vec, 5f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

    }
}
