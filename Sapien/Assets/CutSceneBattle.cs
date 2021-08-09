using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class CutSceneBattle : MonoBehaviour
{
   [SerializeField] private Transform _playerController;
   [SerializeField] private Transform _player;
   [SerializeField] private Animator _animator;
   [SerializeField] private  NavMeshAgent _agent;
   [SerializeField] private Transform _firstPoint;
   [SerializeField] private Transform _secondPoint;
   [SerializeField] private Transform _enemy;
   [SerializeField] private Transform _point;
   private bool _Islooking = true;
   [SerializeField] private AudioSource _music;

   [Header("Particles")]
   [Space(20f)]
   [SerializeField] private ParticleSystem _powerAccumulation;
   [SerializeField] private ParticleSystem _finalMagic;
   [SerializeField] private ParticleSystem _portal;
   [SerializeField] private ParticleSystem[] _magicCircle;
   [SerializeField] private ParticleSystem[] _flames;
   
   


   private void Start()
   {
        StartCoroutine(AnimationPlayer());
   }

   private void Update()
   {
      if(_Islooking)
      {
         _player.LookAt(_firstPoint);
      }
      else
      {
         _player.LookAt(_point); 
      }
      
   }
   
   private IEnumerator AnimationPlayer()
   {
      yield return new WaitForSeconds(2);
      _portal.Play();
      yield return new WaitForSeconds(2);
      _agent.SetDestination(_firstPoint.position);
      yield return new WaitForSeconds(4f);
      _Islooking = false;
      _animator.SetTrigger("IsIdle");
      yield return new WaitForSeconds(0.7f);
      _powerAccumulation.Play();
      ActivateParticles(_magicCircle, true);
      yield return new WaitForSeconds(6.3f);
      _powerAccumulation.transform.DOMoveY(25, 0.4f);
      yield return new WaitForSeconds(0.5f);
      _powerAccumulation.Stop();
      ActivateParticles(_flames,true);
      yield return new WaitForSeconds(2);
      _playerController.DOMove(new Vector3(-41.48f, 4.503f, -87.51f), 11f);
      yield return new WaitForSeconds(8f);
      _animator.SetBool("Warmap", true);
      yield return new WaitForSeconds(4.7f);
      _animator.SetBool("Warmap", false);
      _player.DOMove(new Vector3(-39.39f, 4.503f, -86.56f), 5f);
      yield return new WaitForSeconds(2);
      _animator.SetTrigger("FinalMagic");
      yield return new WaitForSeconds(1.3f);
      _finalMagic.Play();
      yield return new WaitForSeconds(5f);
      ActivateParticles(_magicCircle, false);
      ActivateParticles(_flames, false);
      _animator.SetTrigger("Stay");
      yield return new WaitForSeconds(3);
      _music.Stop();
      
   }



   private void ActivateParticles(ParticleSystem[] particles, bool IsPlay)
   {
      for(int i = 0; i < particles.Length; i++)
      {
         if(IsPlay)
         {
            particles[i].Play();
         }
         else
         {
            particles[i].Stop();
         }
           
      }
   }

   
}
