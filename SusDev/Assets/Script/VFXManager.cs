using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VFXManager : MonoBehaviour
{
    //fog properties
    public GameObject fog;
    private Material fogMat;
    private Color FogColor;
    private Color fogColorVariant;
    private float fogDensity;
    public int fogCancelNum = 3;

    //post-processing porperties
    private float _whiteBalanceTempIni;
    private float _whiteBalanceTintIni;
    private float _whiteBalanceTempTarget;
    private float _whiteBalanceTintTarget;
    private float _whiteBalanceTempInterval;
    private float _whiteBalanceTintInterval;
    /*public Volume volume;*/
    private VolumeProfile pp;
    private WhiteBalance wb;
    //bird flock
    public GameObject _birdFlock;
    private float instantiateXmax;
    private float instantiateZmax;
    // Start is called before the first frame update
    void Start()
    {
        fogMat = fog.GetComponent<Renderer>().material;
        //setting up fog
        fogDensity = 0.4f;
        FogColor = new Color(1f, 1f, 0.4f, 0.4f);
        fogColorVariant = new Color((1-FogColor.r) / fogCancelNum, (1 - FogColor.g) / fogCancelNum, (1 - FogColor.b) / fogCancelNum, -1* FogColor.a / fogCancelNum);
        fogMat.SetColor("_FogColor", FogColor);
        fogMat.SetFloat("_FogDenisty", fogDensity);
        //seting up postprocessing properties
        _whiteBalanceTempIni = -20f;
        _whiteBalanceTintIni = -20f;
        _whiteBalanceTempTarget = 20f;
        _whiteBalanceTintTarget = 10f;
        _whiteBalanceTempInterval = (_whiteBalanceTempTarget - _whiteBalanceTempIni) / 10f;
        _whiteBalanceTintInterval = (_whiteBalanceTintTarget - _whiteBalanceTintIni) / 10f;
        pp = transform.GetChild(0).GetComponent<Volume>().profile;
        if (pp.TryGet<WhiteBalance>(out var tmp))
        {
            wb = tmp;
            wb.temperature.value = _whiteBalanceTempIni + _whiteBalanceTempInterval;
            wb.tint.value = _whiteBalanceTintIni + _whiteBalanceTintInterval;
        }
        //setting up bird cordinate
        instantiateXmax = 120f;
        instantiateZmax = 108f;
    }
    public void UpdateEnvironment(int change)
    {
        CancelFog(change);
        ConfigureFilter(change);
        if (GameManager.total_environment >= 4)
        {
            InstantiateBirds(change);
        }
    }
    public void CancelFog(int i)
    {
        if(FogColor.a > 0f)
        {
            Vector4 targetFog = FogColor + i*fogColorVariant;
            Color targetFogColor = new Color(targetFog.x, targetFog.y, targetFog.z, targetFog.w);
            fogDensity -= 0.1f;
            StartCoroutine(FogFade(targetFogColor, 1f));
        }
    }
    private IEnumerator FogFade(Color target, float duration)
    {
        float time = 0;
        Color startValue = FogColor;
        while (time < duration)
        {
            FogColor = Color.Lerp(startValue, target, time / duration);
            fogMat.SetColor("_FogColor", FogColor);
            time += Time.deltaTime / duration;
            yield return null;
        }
        FogColor = target;
    }
    private void ConfigureFilter(int input)
    {
        wb.temperature.Interp(wb.temperature.value, wb.temperature.value + input * _whiteBalanceTempInterval, 1f);
        wb.tint.Interp(wb.tint.value, wb.tint.value + input * _whiteBalanceTintInterval, 1f);
    }
    private void InstantiateBirds(int input)
    {
        int i = 0;
        while(i < input)
        {
            GameObject obj = Instantiate(_birdFlock, transform.GetChild(1).transform);
            obj.transform.position = new Vector3(Random.Range(instantiateXmax/4 , instantiateXmax*3/4), 10, Random.Range(instantiateZmax / 4, instantiateZmax * 3 / 4));
            i++;
        }
    }
    // Update is called once per frame
/*    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TestingIndex.Environment < 10)
            {
                UpdateEnvironment(1);
            }
        }
    }*/
}
