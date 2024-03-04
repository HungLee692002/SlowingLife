using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TransitionType
{
    Warp,
    Scene
}
public class Transition : MonoBehaviour
{
    [SerializeField] TransitionType transitionType;

    [SerializeField] string sceneNameToTransition;

    [SerializeField] Vector3 targetPosition;

    Transform destination;

    // Start is called before the first frame update
    void Start()
    {
        destination = transform.GetChild(1);
    }

    internal void InitiateTransistion(Transform toTransistion)
    {
        Cinemachine.CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();



        switch (transitionType)
        {
            case TransitionType.Warp:
                {
                    currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(
                        toTransistion,
                        destination.position - toTransistion.position);

                    toTransistion.position = destination.position;
                    break;
                }
            case TransitionType.Scene:
                {
                    currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(
                        toTransistion,
                        targetPosition - toTransistion.position);
                    GameSceneManager.instance.SwitchScene(sceneNameToTransition, targetPosition);
                    break;
                }
        }
    }
}
