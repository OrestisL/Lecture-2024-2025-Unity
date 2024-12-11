using UnityEngine;
using System.Collections;

namespace EpicToonFX
{
    public class ETFXProjectileScript : MonoBehaviour
    {
        public GameObject impactParticle;
        public GameObject projectileParticle;
        public GameObject muzzleParticle;
        public GameObject[] trailParticles;
        [Header("Adjust if not using Sphere Collider")]
        public float colliderRadius = 1f;
        [Range(0f, 1f)]
        public float collideOffset = 0.15f;

        private Rigidbody rb;
        private Transform myTransform;
        private SphereCollider sphereCollider;

        private float destroyTimer = 0f;
        private bool destroyed = false;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            myTransform = transform;
            sphereCollider = GetComponent<SphereCollider>();

            projectileParticle = Instantiate(projectileParticle, myTransform.position, myTransform.rotation) as GameObject;
            projectileParticle.transform.parent = myTransform;

            if (muzzleParticle)
            {
                muzzleParticle = Instantiate(muzzleParticle, myTransform.position, myTransform.rotation) as GameObject;
                Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
            }

            // Immediately adjust rotation to match initial velocity direction
            RotateTowardsDirection(true);
        }

        void FixedUpdate()
        {
            if (destroyed)
            {
                return;
            }

            float rad = sphereCollider ? sphereCollider.radius : colliderRadius;

            Vector3 dir = rb.linearVelocity; // Use rb.velocity instead of rb.linearVelocity
            float dist = dir.magnitude * Time.deltaTime;

            if (rb.useGravity)
            {
                // Handle gravity separately to correctly calculate the direction.
                dir += Physics.gravity * Time.deltaTime;
                dist = dir.magnitude * Time.deltaTime;
            }

            RaycastHit hit;
            if (Physics.SphereCast(myTransform.position, rad, dir, out hit, dist))
            {
                myTransform.position = hit.point + (hit.normal * collideOffset);

                GameObject impactP = Instantiate(impactParticle, myTransform.position, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;

                if (hit.transform.tag == "Target") // Projectile will affect objects tagged as Target
                {
                    ETFXTarget etfxTarget = hit.transform.GetComponent<ETFXTarget>();
                    if (etfxTarget != null)
                    {
                        etfxTarget.OnHit();
                    }
                }

                foreach (GameObject trail in trailParticles)
                {
                    GameObject curTrail = myTransform.Find(projectileParticle.name + "/" + trail.name).gameObject;
                    curTrail.transform.parent = null;
                    Destroy(curTrail, 3f);
                }
                Destroy(projectileParticle, 3f);
                Destroy(impactP, 5.0f);
                DestroyMissile();
            }
            else
            {
                // Increment the destroyTimer if the projectile hasn't hit anything.
                destroyTimer += Time.deltaTime;

                // Destroy the missile if the destroyTimer exceeds 5 seconds.
                if (destroyTimer >= 5f)
                {
                    DestroyMissile();
                }
            }

            RotateTowardsDirection();
        }

        private void DestroyMissile()
        {
            destroyed = true;

            foreach (GameObject trail in trailParticles)
            {
                GameObject curTrail = myTransform.Find(projectileParticle.name + "/" + trail.name).gameObject;
                curTrail.transform.parent = null;
                Destroy(curTrail, 3f);
            }
            Destroy(projectileParticle, 3f);
            Destroy(gameObject);

            ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
            // Component at [0] is that of the parent i.e. this object (if there is any)
            for (int i = 1; i < trails.Length; i++)
            {
                ParticleSystem trail = trails[i];
                if (trail.gameObject.name.Contains("Trail"))
                {
                    trail.transform.SetParent(null);
                    Destroy(trail.gameObject, 2f);
                }
            }
        }

        private void RotateTowardsDirection(bool immediate = false)
        {
            if (rb.linearVelocity != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(rb.linearVelocity.normalized, Vector3.up);

                if (immediate)
                {
                    myTransform.rotation = targetRotation;
                }
                else
                {
                    float angle = Vector3.Angle(myTransform.forward, rb.linearVelocity.normalized);
                    float lerpFactor = angle * Time.deltaTime; // Use the angle as the interpolation factor
                    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, lerpFactor);
                }
            }
        }
    }
}