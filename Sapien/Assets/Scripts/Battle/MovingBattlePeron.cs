using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;
using CartoonFX;
using DG.Tweening;

    public class MovingBattlePeron : MonoBehaviour
    {
        public BattleController inv;
        private NavMeshAgent agent;
        public Transform point;
        public Animator Anim;
        [Space]
        public GameObject particleShag;
        public GameObject StartParticle, background, Uron, DestroyBullet;
        public Transform instantiateParticle;
        [Range(0.1f, 10)]
        public float speedBulllet;
        public Transform[] Vrag;
        [SerializeField] private ParticleSystem _blood;
        [SerializeField] private GiveDamageAnimation _animation;
        [SerializeField] private EnemiesController _enemiesController;
        [SerializeField] private GameObject _enemyDamageParticle;
        [SerializeField] private BattleController _battleController;
        [SerializeField] private Sound _damageEnemy;
        [SerializeField] private Sound _explotionBullet;
        [SerializeField] private Sound _whenBulletMoveSound;
        [SerializeField] private GameObject _bomb;
       
               
    
        
        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
           
        }
        public IEnumerator ClickOnEnemy(Vector3 enemyPoint)
        {
            _enemiesController.CanAttack = false;
            GameObject bk = Instantiate(background, transform);
            Anim.SetTrigger("IsAttack");

            yield return new WaitForSeconds(1);
            GameObject bullet = Instantiate(Uron, instantiateParticle);
            StartCoroutine(MoveBullet(bullet, enemyPoint));
            transform.rotation.SetLookRotation(bullet.transform.position);
            
            yield return new WaitForSeconds(2);
            Destroy(bk);
            
        }

        IEnumerator MoveBullet(GameObject bullet, Vector3 point)
        {
            _whenBulletMoveSound.PlaySound();
            while (true)
            {
                Vector3 quat = Vector3.RotateTowards( bullet.transform.forward, point - bullet.transform.position, 120 *Time.deltaTime,0.0f);
                bullet.transform.rotation = Quaternion.LookRotation(quat);
                bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, point,speedBulllet * Time.deltaTime);
                if (Vector3.Distance(bullet.transform.position, point) < 0.01f)
                {
                    
                    Debug.Log("I am here");
                    _damageEnemy.PlaySound();
                    Vector3 StartPosition = bullet.transform.position + new Vector3(0,1,0);
                    float StartRotation = Quaternion.LookRotation(transform.position - (bullet.transform.position + new Vector3(0,1,0))).eulerAngles.y;
                    GameObject particle = Instantiate(_enemyDamageParticle, StartPosition, Quaternion.identity) as GameObject;
                    particle.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.01f);
                    particle.SetActive(true);
                    particle.GetComponent<CFXR_ParticleText_Runtime>().rotation = StartRotation;
                    particle.GetComponent<CFXR_ParticleText_Runtime>().text = inv.CurrentUron.ToString();
                    particle.GetComponent<CFXR_ParticleText_Runtime>().GenerateText(inv.CurrentUron.ToString());
                    if(_battleController.IsBomb)
                    {
                        for(int i = 0; i < _enemiesController._enemies.Count; i++)
                        {
                            Vrag[i].GetComponent<EnemyController>().OnBombClick();
                            _battleController.IsBomb = false;
                            
                        }
                        StartCoroutine(_battleController.FinishGame());
                         GameObject ParticleBomb = Instantiate(_bomb, transform.position + new Vector3(1,0,3), Quaternion.identity);
                    }
                    else
                    {
                        if(!_battleController.infinitely)
                        {
                            inv.EnemyDie();
                        }
                        else
                        {
                            StartCoroutine(_battleController.FinishGame());
                        }
                    }
                    
                   Instantiate(DestroyBullet,new Vector3(point.x, point.y, point.z), Quaternion.identity);
                    Destroy(bullet);
                    _explotionBullet.PlaySound();
                    yield break;
                }
                yield return new WaitForFixedUpdate();
                
            }
        }
        
        void Update()
        {
            if (Vector3.Distance(transform.position, point.position) < .5f)
            {
                agent.enabled = false;
                Anim.SetBool("IsAttack", false);

                Vector3 quat = Vector3.RotateTowards(transform.forward, new Vector3(Vrag[_enemiesController.RandomEnemy].position.x,transform.position.y,Vrag[_enemiesController.RandomEnemy].position.z)-transform.position,100 * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(quat);
            }
        }
    }
