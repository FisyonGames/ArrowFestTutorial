using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")]
public class LockCameraX : CinemachineExtension
{
    public float xPos = 0;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if(stage == CinemachineCore.Stage.Body)
        {
            Vector3 pos = state.RawPosition;
            pos.x = xPos;
            state.RawPosition = pos;
        }
    }
}
