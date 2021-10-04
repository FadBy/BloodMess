using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private LineRenderer red;
    [SerializeField]
    private LineRenderer green;
    [SerializeField]
    private float offsetX;
    [SerializeField]
    private float offsetY;
    [SerializeField]
    private float widthLine;
    private Vector3 startPoint;
    private Vector3 endPoint;

    private void Awake()
    {
        green.startWidth = widthLine;
        red.startWidth = widthLine;
    }

    private void OnEnable()
    {
        red.positionCount = 2;
        green.positionCount = 2;
        startPoint = new Vector3(transform.localPosition.x - offsetX, offsetY, 0f);
        endPoint = new Vector3(transform.localPosition.x + offsetX, offsetY, 0f);
        green.SetPositions(new Vector3[]{ startPoint, endPoint });
        red.SetPositions(new Vector3[] { endPoint, endPoint });
    }

    public void ChangeHealth(float maxHealth, float currentHealth)
    {
        ChangeLine(currentHealth / maxHealth);
    }

    private void ChangeLine(float koef)
    {
        red.SetPosition(0, startPoint + (endPoint - startPoint) * koef);
    }



}
