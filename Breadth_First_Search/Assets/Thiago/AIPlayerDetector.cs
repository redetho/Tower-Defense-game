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
            detectorOrigin = this.gameObject.transform;
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
            Collider[] colliders = Physics.OverlapSphere(
                detectorOrigin.position + detectorOriginOffset,
                detectorSize.x / 2,
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
                Gizmos.DrawWireSphere(detectorOrigin.position + detectorOriginOffset, detectorSize.x / 2);

                if (playerDetected)
                {
                    Gizmos.color = gizmoDetectedColor;
                    Gizmos.DrawSphere(detectorOrigin.position + detectorOriginOffset, detectorSize.x / 2);
                }
            }
        }

    }
}