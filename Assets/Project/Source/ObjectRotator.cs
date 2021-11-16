using UnityEngine;

namespace Project.Source
{
    public class ObjectRotator : MonoBehaviour
    {
        [SerializeField] private AnimationCurve horizontalRotationCurve;


        private void Update()
        {
            var rotation = horizontalRotationCurve.Evaluate(Time.timeSinceLevelLoad);

            var angle = transform.eulerAngles;
            angle.z = rotation;

            transform.eulerAngles = angle;
        }
    }
}