using System.Collections;
using UnityEngine;

namespace BowMaster.Core
{
    public class CameraFollow : MonoBehaviour
    {
        public ObjVar camTarget;
        public float smoothSpeed = 0.125f;
        public Vector3 offSet;
        public Color[] CamColors;
        public GameObject[] scenes;
        bool introPlayed = false;
        Transform m_transform;
        public GameEvent onIntroFinished;
        Camera cam;
        private void Start()
        {
            m_transform = transform;
            cam = GetComponent<Camera>();
            OnChangeScenario(Random.Range(0,3));
        }
        private void FixedUpdate()
        {
            if (!introPlayed || camTarget._object == null)
                return;
            Vector3 desiredPos = camTarget._object.position + offSet;
            desiredPos.y = Mathf.Clamp(desiredPos.y, 0.0f, 10.0f);
            desiredPos.z = -10.0f;
            desiredPos.x = Mathf.Clamp(desiredPos.x, -10.0f, 50.0f);
            Vector3 smoothPos = Vector3.Lerp(m_transform.position, desiredPos, smoothSpeed);
            m_transform.position = smoothPos;

            float orthoSize = 5 * (camTarget._object.position.y * 0.5f);
            orthoSize = Mathf.Clamp(orthoSize, 5.0f, 8.0f);
            float smoothSize = Mathf.Lerp(cam.orthographicSize, orthoSize, smoothSpeed);
            cam.orthographicSize = smoothSize;
        }

        public void OnChangeScenario(int no)
        {
            for (int i = 0; i < scenes.Length; i++)
            {
                scenes[i].SetActive(false);
            }
            scenes[no].SetActive(true);
            cam.backgroundColor = CamColors[no];
        }
        public void PlayIntro()
        {
           
            StartCoroutine(ShowIntro());
        }
        IEnumerator ShowIntro()
        {
            cam.orthographicSize = 3.5f;
            GameObject enemy = GameObject.FindWithTag("enemy");
            if (enemy != null)
            {
                m_transform.position = new Vector3(enemy.transform.position.x, 1.0f, -10.0f);
            }
            else
            {
                introPlayed = true;
                yield break;
            }
            yield return new WaitForSeconds(0.5f);

            while (cam.orthographicSize < 5.0f)
            {
                cam.orthographicSize += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForFixedUpdate();
            float progress = 0.0f;
            Vector3 startPos = m_transform.position;
            Vector3 targetPos = new Vector3(0, 1.0f, -10.0f);
            while(progress < 1)
            {
                progress += Time.deltaTime*0.5f;
                m_transform.position = Vector3.Lerp(startPos, targetPos, progress);
                yield return null;
            }
            transform.position = targetPos;
            introPlayed = true;
            onIntroFinished.Raise();
        }
    }
}

