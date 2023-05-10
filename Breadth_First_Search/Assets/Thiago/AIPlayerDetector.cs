using System.Collections;
using UnityEngine;

namespace SVS.AI
{
    public class AIPlayerDetector : MonoBehaviour
    {
        [SerializeField] private bool playerDetected;
        public bool PlayerDetected => playerDetected;

        [SerializeField] private Transform detectorOrigin;
        [SerializeField] private Vector3 detectorSize = Vector3.one;
        [SerializeField] private Vector3 detectorOriginOffset = Vector3.zero;

        [SerializeField] private float detectionDelay = 0.3f;

        [SerializeField] private LayerMask detectorLayerMask;

        [SerializeField] private Color gizmoIdColor = Color.green;
        [SerializeField] private Color gizmoDetectedColor = Color.red;
        [SerializeField] private bool showGizmo = true;

        private GameObject target;

        public GameObject Target
        {
            get => target;
            private set
            {
                target = value;
                playerDetected = target != null;
            }
        }

        private void Start()
        {
            StartCoroutine(DetectionCoroutine());
        }

        private IEnumerator DetectionCoroutine()
        {
            yield return new WaitForSeconds(detectionDelay);
            PerformDetection();
            StartCoroutine(DetectionCoroutine());
        }

        private void PerformDetection()
        {
            Collider[] colliders = Physics.OverlapBox(
                detectorOrigin.position + detectorOriginOffset,
                detectorSize / 2,
                detectorOrigin.rotation,
                detectorLayerMask);

            if (colliders.Length > 0)
            {
                Target = colliders[0].gameObject;
            }
            else
            {
                Target = null;
            }
        }

        private void OnDrawGizmos()
        {
            if (showGizmo && detectorOrigin != null)
            {
                Gizmos.color = gizmoIdColor;
                Gizmos.matrix = detectorOrigin.localToWorldMatrix;
                Gizmos.DrawWireCube(detectorOriginOffset, detectorSize);

                if (playerDetected)
                {
                    Gizmos.color = gizmoDetectedColor;
                    Gizmos.DrawCube(detectorOriginOffset, detectorSize);
                }
            }
        }
    }
}