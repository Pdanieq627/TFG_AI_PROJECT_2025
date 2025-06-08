using UnityEngine;

public class CameraFollowBasic : MonoBehaviour
{
    [Tooltip("Offset en Z habitualmente a -10 para una c�mara 2D.")]
    public Vector3 offset = new Vector3(0, 0, -10);

    [Tooltip("Tiempo de suavizado (0 = sin suavizado).")]
    public float smoothSpeed = 0.125f;

    private Transform target;

    void Start()
    {
        //// Busca el jugador por etiqueta; aseg�rate de que tu prefab tiene la etiqueta "Player"
        //GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        //if (playerGO != null)
        //{
        //    target = playerGO.transform;
        //}
        //else
        //{
        //    Debug.LogError("No se encontr� ning�n GameObject con tag 'Player'. �Olvidaste asignarle la etiqueta?");
        //}
    }

    void LateUpdate()
    {
        if (target == null)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null)
            {
                target = playerGO.transform;
            }
            else
            {
                return; // A�n no existe el jugador
            }
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}