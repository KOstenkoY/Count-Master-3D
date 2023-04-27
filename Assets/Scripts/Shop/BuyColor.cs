using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BuyColor : MonoBehaviour
{
    [SerializeField] private Transform _arrow;
    [SerializeField] private List<Material> _materialList = new List<Material>();

    private int _colorCost = 1;

    private static int _currentColorIndex = 2;

    private int _randomColor;
    private static float _targetColorDegrees = -67.5f;

    private float _duration;

    public static System.Action<Material> OnColorBought;

    private void Awake()
    {
        _arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _targetColorDegrees));
    }

    public void TurnWheel()
    {
        if (Player.Instance.BuyItem(_colorCost))
        {
            gameObject.GetComponent<Button>().interactable = false;

            _randomColor = Random.Range(2, 8);
            _targetColorDegrees -= _randomColor * 45;

            _currentColorIndex += _randomColor;
            _currentColorIndex %= _materialList.Count;
            
            _duration = Random.Range(0.6f, 1);

            _arrow.DORotate(new Vector3(0, 0, _targetColorDegrees), _duration);

            StartCoroutine(WaitUntilFinishRitation());
        }

        OnColorBought?.Invoke(_materialList[_currentColorIndex]);
    }

    private IEnumerator WaitUntilFinishRitation()
    {
        yield return new WaitForSeconds(_duration);

        gameObject.GetComponent<Button>().interactable = true;
    }

}
