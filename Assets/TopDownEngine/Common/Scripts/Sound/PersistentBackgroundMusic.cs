using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MoreMountains.TopDownEngine
{
    [AddComponentMenu("TopDown Engine/Sound/Persistent Background Music")]
    public class PersistentBackgroundMusic : MMPersistentSingleton<PersistentBackgroundMusic>
    {
        [Tooltip("La música del menú y créditos")]
        public AudioClip MenuMusic;

        [Tooltip("La música de gameplay")]
        public AudioClip GameplayMusic;

        [Tooltip("Nombres de escenas donde debe sonar el MenuMusic")]
        public string[] MenuScenes;

        [Tooltip("Debe hacer loop la música?")]
        public bool Loop = true;

        public AudioSource _audioSource;
        private AudioClip _currentClip;

        protected override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += OnSceneLoaded;

            PlayMusicForCurrentScene(SceneManager.GetActiveScene().name);
        }

        protected virtual void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        protected void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            PlayMusicForCurrentScene(scene.name);
        }

        protected void PlayMusicForCurrentScene(string sceneName)
        {
            AudioClip targetClip = IsMenuScene(sceneName) ? MenuMusic : GameplayMusic;

            if (_currentClip == targetClip)
                return;

            _currentClip = targetClip;

            _audioSource.Stop();
            _audioSource.clip = _currentClip;
            _audioSource.loop = Loop;
            _audioSource.Play();
        }

        protected bool IsMenuScene(string sceneName)
        {
            foreach (string name in MenuScenes)
            {
                if (name == sceneName)
                    return true;
            }
            return false;
        }
    }
}

