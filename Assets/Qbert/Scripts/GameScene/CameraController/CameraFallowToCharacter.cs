using System;
using System.Collections;
using System.Linq;
using System.Xml.Serialization;
using Assets.Qbert.Scripts.GameScene.Levels;
using Assets.Qbert.Scripts.GameScene.MapLoader;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene
{
    public class CameraFallowToCharacter : MonoBehaviour
    {
        public Transform cameraFallow;
        public LevelController levelController;
        public MapFieldGenerator mapGenerator;
        public Camera cameraMain;

        public float zoomCameraSize;
        public float durationSetSizeCameraToCharacter;
        public float cameraSizeCorrector;
        public float speedMoveCamera;

        private Transform fallowTarget;

        private Vector3 centerToMap;

        public void SetTarget(Transform target)
        {
            fallowTarget = target;
        }

        private void ConnectEvens()
        {
           // mapGenerator.OnFieldIsCreate += OnMapGeneratorCreateMap;
        }

        public void OnMapGeneratorCreateMap()
        {
            //ResizeMap();
            //StartCoroutine(StartAnimation());
           // StartResizeCameraSizeToCharacter();
        }

        public void StartResizeCameraSizeToCharacter(Action OnEnd)
        {
            StopAllCoroutines();
            StartCoroutine(StartResizeCameraSizeToCharacterCoroutine(OnEnd));
        }

        private IEnumerator StartResizeCameraSizeToCharacterCoroutine(Action OnEnd)
        {
            yield return new WaitForSeconds(0.2f);

            var characrer = levelController.qbert;

            var startCameraPositino = cameraFallow.transform.position;
            float startCameraSize = cameraMain.orthographicSize;

            yield return StartCoroutine(this.ChageFloatToTime(
                durationSetSizeCameraToCharacter, false, value =>
                {
                    cameraMain.orthographicSize = Mathf.Lerp(
                        startCameraSize, zoomCameraSize, value);

                    cameraFallow.transform.position = Vector3.Slerp(startCameraPositino,
                       characrer.NoOffsetpos, value);

                }));

            if (OnEnd != null)
            {
                OnEnd();
            }
        }

        public void CameraBackSizeAndToCenterMap()
        {
            StopAllCoroutines();
            StartCoroutine(CameraBackSizeAndToCenterMapCoroutine());
        }

        private IEnumerator CameraBackSizeAndToCenterMapCoroutine()
        {
            var cameraSizeTarget = GetSizeCameraViewAllMap();
            var cameraSizeStart = cameraMain.orthographicSize;
            var startCameraPositino = cameraMain.transform.position;

            yield return StartCoroutine(this.ChageFloatToTime(
                durationSetSizeCameraToCharacter, false, value =>
                {
                    cameraMain.orthographicSize = Mathf.Lerp(
                        cameraSizeStart, cameraSizeTarget, value);

                    cameraFallow.transform.position = Vector3.Lerp(startCameraPositino,
                       centerToMap, value);

                }));
        }

        public void ResizeCameraShowAllMap()
        {
            SetCameraCenter();
            cameraMain.orthographicSize = GetSizeCameraViewAllMap();
            centerToMap = cameraMain.transform.position;
        }

        public void SetCameraCenter()
        {
            float minY = float.MaxValue;
            float maxY = float.MinValue;

            foreach (var cube in mapGenerator.map)
            {
                if (cube.transform.position.y < minY)
                    minY = cube.transform.position.y;

                if (cube.transform.position.y > maxY)
                    maxY = cube.transform.position.y;
            }

            cameraFallow.position = new Vector3(
                cameraFallow.position.x , (maxY  + minY) / 2, cameraFallow.position.z);
        }


        private float GetSizeCameraViewAllMap()
        {
            int sizeMapHight = GetMinMapSize();
            return sizeMapHight*cameraSizeCorrector;
        }

        private int GetMinMapSize()
        {
            int minY = 0;
            int maxY = 0;
            int minX = 0;
            int maxX = 0;

            foreach (var cube in mapGenerator.map)
            {
                if (cube.cubeInMap.y < minY)
                    minY = cube.cubeInMap.y;

                if (cube.cubeInMap.y > maxY)
                    maxY = cube.cubeInMap.y;

                if (cube.cubeInMap.x < minX)
                    minX = cube.cubeInMap.x;

                if (cube.cubeInMap.x < maxX)
                    maxX = cube.cubeInMap.x;

            }

            int sizeWidth = Mathf.Abs(minX - maxX);
            int sizeHight = Mathf.Abs(minY - maxY);

            return sizeWidth > sizeHight ? sizeWidth : sizeHight;
        }

        public void StareFallow()
        {
            StopAllCoroutines();
            StartCoroutine(FaloowCaroutine());
        }

        private IEnumerator FaloowCaroutine()
        {
            var characrer = levelController.qbert;

            while (true)
            {
                if (characrer)
                {
                    Vector3 target = fallowTarget != null ? fallowTarget.position : characrer.NoOffsetpos;

                    cameraFallow.transform.position = Vector3.Lerp(cameraFallow.transform.position,
                       target, Time.deltaTime * speedMoveCamera);
                }
                yield return null;
            }
        }


        void Awake()
        {
            ConnectEvens();
        }
    }
}