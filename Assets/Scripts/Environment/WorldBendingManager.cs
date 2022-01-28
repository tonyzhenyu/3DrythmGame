using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
namespace ZY
{
    [ExecuteAlways]
    public class WorldBendingManager : MonoBehaviour
    {
        #region Constants
        public const string ENABLE_BENDING = "_ENABLE_BENDING";
        #endregion

        public bool debugMode = false;

        #region AwakeSetting
        private void Awake()
        {
            if (Application.isPlaying)
            {
                Shader.EnableKeyword(ENABLE_BENDING);
            }
            else
            {
                Shader.DisableKeyword(ENABLE_BENDING);
            }
        }
        #endregion

        #region UpdateControll
        private void Update()
        {
            if (!debugMode)
            {
                Shader.EnableKeyword(ENABLE_BENDING);
            }
            else
            {
                Shader.DisableKeyword(ENABLE_BENDING);
            }
        }
        #endregion

        #region SetCameraCulling
        private void OnEnable()
        {
            RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
            RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
        }

        private void OnDisable()
        {
            RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
            RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
        }

        private static void OnBeginCameraRendering(ScriptableRenderContext ctx, Camera cam)
        {
            cam.cullingMatrix = Matrix4x4.Ortho(-99, 99, -99, 99, 0.001f, 999) * cam.worldToCameraMatrix;
        }
        private static void OnEndCameraRendering(ScriptableRenderContext ctx, Camera cam)
        {
            cam.ResetCullingMatrix();
        }
        #endregion
    }

}
