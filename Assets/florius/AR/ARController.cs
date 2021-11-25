using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace florius.AR
{
    public class ARController : MonoBehaviour
    {
        public ARSessionOrigin ARSessionOrigin;
        public ARTrackedImageManager TrackedImageManager;
        public Transform Content;

        private bool _isContentPlaced;

        private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
        {
            if (_isContentPlaced) return;

            foreach (var trackedImage in args.updated)
            {
                if (trackedImage.trackingState != TrackingState.Tracking) continue;

                ARSessionOrigin.MakeContentAppearAt(
                    Content, trackedImage.transform.position, trackedImage.transform.localRotation
                );

                Content.gameObject.SetActive(true);

                _isContentPlaced = true;

                break;
            }
        }

        private void OnEnable()
        {
            TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
            Content.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
    }
}