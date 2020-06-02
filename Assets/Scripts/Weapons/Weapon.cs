using UnityEngine;

/// <summary>
/// Spawn projectiles, Fire Basic Projectiles, Fire Charged Projectiles and a timer to stop the player from shooting like crazy (fireRate)
/// </summary>
public class Weapon : MonoBehaviour
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------

    #region ---------------------------- PROPERTIES
    public float fireRate;
    public float chargeDuration;
    public GameObject[] projectilePrefabs;
    public Transform projectileSpawnPoint;

    public int ProjectileIndex { get { return _ProjectileIndex; } set { _ProjectileIndex = value; } }
    private int _ProjectileIndex;

    protected Projectile basicProjectile;
    protected Projectile chargedProjectile;
    protected bool canFire;
    protected float t;
    #endregion

    #region ---------------------------- UNITY CALLBACKS
    protected virtual void Awake() 
    {
        canFire = true;
    }

    protected virtual void OnEnable()
    {
        SpawnProjectile();
    }

    protected virtual void Update()
    {
        if (!canFire)
        {
            t += Time.deltaTime;
            if (t >= fireRate)
            {
                canFire = true;
                SpawnProjectile();
                t = 0.0f;
            }
        }
    }
    #endregion

    #region ---------------------------- METHODS
    protected virtual void SpawnProjectile()
    {
        if (projectilePrefabs == null || projectileSpawnPoint == null)
        {
            Debug.LogError(gameObject.name + " missing prefab or spawnPoint!");
            return;
        }
        if (basicProjectile == null && projectilePrefabs[_ProjectileIndex] != null)
        {
            basicProjectile = Instantiate(projectilePrefabs[_ProjectileIndex], projectileSpawnPoint.position,
            projectileSpawnPoint.rotation, projectileSpawnPoint).GetComponent<Projectile>();
            basicProjectile.SetActive(false);
        }
    }

    public virtual void FireBasic()
    {
        if (basicProjectile != null && canFire)
        {
            basicProjectile.SetActive(true);
            CameraShake.Shake(0.075f, 0.085f, 3f);
            //Debug.Log("Weapon: Calling Projectile Fire()");
            basicProjectile.Fire();
            basicProjectile = null;
            canFire = false;
        }
    }

    public virtual void FireCharged()
    {
        if (basicProjectile != null && canFire)
        {
            if (chargedProjectile == null && 2 <= projectilePrefabs.Length && projectilePrefabs[2] != null)
            {
                chargedProjectile = Instantiate(projectilePrefabs[2], projectileSpawnPoint.position,
                projectileSpawnPoint.rotation, projectileSpawnPoint).GetComponent<Projectile>();
                chargedProjectile.Fire();
            }
            CameraShake.Shake(0.2f, 0.5f, 3f);
            chargedProjectile = null;
        }
    }
    #endregion
}
