using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TypeClothesChoser : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _player;

    [Header("Main UI")]
    [SerializeField] GameObject _headerButton;
    [SerializeField] GameObject _accessoriesButton;
    [SerializeField] GameObject _bodyButton;

    [Header("Head Accessories")]
    [SerializeField] private GameObject[] _panels;

    public void OnClickHeaderButton()
    {
        _camera.DOMove(new Vector3(-0.83f, 8.17f, 1.98f), 0.7f);
        _panels[0].SetActive(true);
        UIOff();
    }

    public void OnCLickAccessoriesButton()
    {
        _camera.DOMove(new Vector3(-0.7f, 5.2f, -1.2f),0.7f);
        _player.DORotate(new Vector3(0, 315.2f, 0), 0.7f);
        _panels[1].SetActive(true);
        UIOff();
    }

    public void OnCLickGlasesButton()
    {
        UIOff();
        _camera.DOMove(new Vector3(-0.83f, 8.17f, 1.98f), 0.7f);
        _panels[2].SetActive(true);
    }

    public void BackToMain()
    {
        for(int i = 0; i < _panels.Length; i++)
        {
            _panels[i].SetActive(false);
        }
        UIOn();
        _player.DORotate(new Vector3(0, 180, 0), 0.7f);
    }

  
    private void UIOn()
    {
        StartCoroutine(UIButtonOn());
        _camera.DOMove(new Vector3(0.3f, 2.03f, -9.6f), 0.7f);
    }

    private void UIOff()
    {
        _headerButton.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        _accessoriesButton.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        _bodyButton.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
    }

    private IEnumerator UIButtonOn()
    {
        yield return new WaitForSeconds(0.6f);
        _headerButton.transform.DOScale(new Vector3(1,1,1), 0.3f);
        _accessoriesButton.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        _bodyButton.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
    }


    public void GoToMain()
    {
        SceneManager.LoadScene(2);
    }

}
