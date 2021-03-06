using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using CartoonFX;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private List<Image> _enemiesStatus = new List<Image>();
    public Sprite _sleep;
    public Sprite _agressive;
    public int RandomEnemy;
    [SerializeField] private MovingBattlePeron _person;
    public List<Animator> EnemyAnim = new List<Animator>();
    public Transform PointShoot;
    [SerializeField] private BattleController inv;
     [Range(0.001f, 1)]
    public float speedMouse;
    public  List<Vector3> StartPosEnemy = new List<Vector3>();
    public List<Transform> _enemiesPosition = new List<Transform>();
    public NavMeshAgent agent;
    public Animator AnimCharacter;
    public  List<Transform> _enemies = new List<Transform>();
    public GameObject particleShag;
     private Ray RayMouse;
     [SerializeField] private Camera _camera;
     public EnemyController LastHited;
     private int _count;
    

    private void Start()
    {
        ChangeStatus();

        for(int i = 0; i < _enemiesPosition.Count; i++)
        {
            StartPosEnemy[i] = new Vector3(_enemiesPosition[i].position.x, _enemiesPosition[i].position.y, _enemiesPosition[i].position.z);
        }

    }

    public void ChangeStatus()
    {
       if(_enemies.Count > 0)
       {
            RandomEnemy = Random.Range(0, _enemies.Count);
            for(int i = 0; i < _enemiesStatus.Count; i++)
            {
               _enemiesStatus[i].sprite = _sleep;
            }
            _enemiesStatus[RandomEnemy].sprite = _agressive;
       }
     
    }

     void FixedUpdate()
    {
       
        if (Input.GetButtonDown("Fire1"))
        {
            if (_camera!= null)
            {
                RaycastHit hit;
                var mousePos = Input.mousePosition;
                RayMouse = _camera.ScreenPointToRay(mousePos);

                if (Physics.Raycast(RayMouse.origin, RayMouse.direction, out hit, 40))
                {
                    if (_enemies.Contains(hit.transform))
                    {   
                        EnemyController Local = hit.transform.gameObject.GetComponent<EnemyController>();
                        
                            LastHited = Local;
                            StartCoroutine(_person.ClickOnEnemy(hit.transform.position));
                            inv.bombSlider.value = 0;
                            inv.Bomb.interactable = false;
                    
                       
                        
                        
                    }
                }
            }
            else { Debug.Log("No camera"); }
        }
    }
   




    public IEnumerator EnemyGiveUron(int damage)
    {   
        if(_enemies[RandomEnemy].GetComponent<EnemyController>().IsDied !=  true)
        {
            yield return new WaitForSeconds(2f);
            EnemyAnim[RandomEnemy].SetBool("Go", true);
            while (true)
            {
                _enemies[RandomEnemy].transform.position = Vector3.MoveTowards( _enemies[RandomEnemy].transform.position, PointShoot.position, speedMouse);
                Vector3 quat = Vector3.RotateTowards( _enemies[RandomEnemy].transform.forward, PointShoot.position -  _enemies[RandomEnemy].transform.position, 20 * Time.deltaTime, 0.0f);
                 _enemies[RandomEnemy].transform.rotation = Quaternion.LookRotation(quat);
                yield return new WaitForFixedUpdate();
                if (Vector3.Distance( _enemies[RandomEnemy].transform.position, PointShoot.position) < 0.01f)
                {
                   
                    EnemyAnim[RandomEnemy].SetBool("Go", false);
                    EnemyAnim[RandomEnemy].SetTrigger("Attack");
                    inv.HP_Person_Controller(damage);
                    inv.animPerson.SetTrigger("takeDamage");
                    StartCoroutine(BackEnemyToStartPos());
                    yield break;
                }
            }
        }
        else
        {
            EnemyAnim[RandomEnemy].SetTrigger("die");
            inv.StartFightPanel();
        }
          
        
    }
    IEnumerator BackEnemyToStartPos()
    {
        yield return new WaitForSeconds(2f);
        EnemyAnim[RandomEnemy].SetBool("Go", true);
        while (true)
        {
            _enemies[RandomEnemy].transform.position = Vector3.MoveTowards( _enemies[RandomEnemy].transform.position, StartPosEnemy[RandomEnemy], speedMouse);
            Vector3 quat = Vector3.RotateTowards( _enemies[RandomEnemy].transform.forward, StartPosEnemy[RandomEnemy] -  _enemies[RandomEnemy].transform.position, 10 * Time.deltaTime, 0.0f);
            _enemies[RandomEnemy].transform.rotation = Quaternion.LookRotation(quat);
            yield return new WaitForFixedUpdate();

            if (Vector3.Distance( _enemies[RandomEnemy].transform.position, StartPosEnemy[RandomEnemy]) < 0.1f)
            {
                EnemyAnim[RandomEnemy].SetBool("Go", false);
                _enemies[RandomEnemy].transform.rotation = Quaternion.LookRotation(PointShoot.position -  _enemies[RandomEnemy].transform.position);
               inv.ClickOnGem.interactable = true;
               if(_count < 1)
            {
                if(inv.HP_Person > 0)
               {
                  
                  _count++;
                  inv.StartFightPanel();
               }
            }
             
            }
            
          
        }
          
        
    }


    public void RemoveEnemy(int index)
    {
        
            _enemies.RemoveAt(index);
            StartPosEnemy.RemoveAt(index);
           _enemiesPosition.RemoveAt(index);
           EnemyAnim.RemoveAt(index);
           _enemiesStatus.RemoveAt(index);
     
        
       
    }


    
}
