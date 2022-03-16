using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class TrailController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem MainTrail;
        [SerializeField] private ParticleSystem Fog;

        public void TrailOn()
        {
            var p1 = MainTrail.emission;
            var p2 = Fog.emission;

            p1.enabled = true;
            p2.enabled = true;
        }

        public void TrailOff()
        {
            var p1 = MainTrail.emission;
            var p2 = Fog.emission;

            p1.enabled = false;
            p2.enabled = false;
        }
    }
}
