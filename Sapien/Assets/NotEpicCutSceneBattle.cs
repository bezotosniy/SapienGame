using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NotEpicCutSceneBattle : MonoBehaviour
{
     [Header("Sounds")]
   [SerializeField] private Sound[] _sounds;
    [SerializeField] private Transform _playerController;
   [SerializeField] private GameObject _player;
   [SerializeField] private Animator _animator;
   [SerializeField] private  NavMeshAgent _agent;
   [SerializeField] private Transform _enemy;
   [SerializeField] private Transform _point;
   private bool _Islooking = true;
   [SerializeField] private AudioSource _music;

   [Header("Particles")]
   [Space(20f)]

   [SerializeField] private ParticleSystem _finalMagic;
   [SerializeField] private ParticleSystem _portal;
   [SerializeField] private ParticleSystem _lightning;
   [SerializeField] private ParticleSystem _lightningFlashes;

   private void Start()
   {
        StartCoroutine(AnimationPlayer());
        
   }
    
   private void Update()
   {
       if(_Islooking)
       {
            _player.transform.LookAt(_point);
       }
       
       
       
   }

   private IEnumerator AnimationPlayer()
   {
      //ParticleSystem Flashes =  Instantiate(_lightningFlashes, new Vector3(-41.66f, 6f, -88.03168f), Quaternion.Euler(new Vector3(90, 0, 0)));
      yield return new WaitForSeconds(1.6f);
      _portal.Play();
      yield return new WaitForSeconds(0.4f);
      Instantiate(_lightning, _player.transform.position, Quaternion.identity);
      _sounds[0].PlaySound();
      _sounds[1].PlaySound();
      yield return new WaitForSeconds(0.2f);
      _sounds[2].PlaySound();
      _player.SetActive(true);
      _agent.SetDestination(_point.position);
      yield return new WaitForSeconds(2.3f);
     _Islooking = false;
      _player.transform.LookAt(_enemy);
      _animator.SetTrigger("FinalMagic");
     yield return new WaitForSeconds(0.5f);
     _sounds[2].StopSound();
      yield return new WaitForSeconds(1.3f);
      _sounds[3].PlaySound();
      _finalMagic.Play();
      yield return new WaitForSeconds(4f);
      _sounds[4].PlaySound();
      yield return new WaitForSeconds(1f);
      _animator.SetTrigger("Stay");
      yield return new WaitForSeconds(3);
      _music.Stop();
      
   }


}
