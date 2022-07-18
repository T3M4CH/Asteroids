using UnityEngine.SceneManagement;
using Game.Settings.Interfaces;
using Game.Settings.Enums;
using UnityEngine.UI;
using UnityEngine;
using Zenject;
using TMPro;

namespace Game.UI
{
    public class MonoMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI schemeText;
        [SerializeField] private MonoPauseInput pauseInput;
        [SerializeField] private GameObject gameStarter;
        [SerializeField] private Button[] buttons;

        private IInputSettings _inputSettings;
        private static bool _notFirstStart;

        [Inject]
        private void Construct(IInputSettings inputSettings)
        {
            _inputSettings = inputSettings;
        }
        
        public void SetupRestart()
        {
            Time.timeScale = 0;
            pauseInput.OnPause.Invoke(false);
            pauseInput.gameObject.SetActive(false);
            buttons[0].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(false);
        }

        private void ChangeInputScheme()
        {
            var inputScheme = _inputSettings.EInputScheme;
            inputScheme = inputScheme == EInputScheme.Keyboard
                ? EInputScheme.KeyboardMouse
                : EInputScheme.Keyboard;

            _inputSettings.EInputScheme = inputScheme;

            schemeText.text = inputScheme.ToString();
        }

        private void StartGame()
        {
            if (_notFirstStart)
            {
                SceneManager.LoadScene(0);
            }

            _notFirstStart = true;
            gameStarter.SetActive(true);
            gameObject.SetActive(false);
        }

        private void Resume()
        {
            Time.timeScale = 1;
            pauseInput.OnPause.Invoke(true);
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            buttons[0].gameObject.SetActive(true);
            schemeText.text = _inputSettings.EInputScheme.ToString();
        }

        private void Start()
        {
            Time.timeScale = 1;
            if (_notFirstStart)
            {
                gameStarter.SetActive(true);
                gameObject.SetActive(false);
            }
            
            buttons[0].onClick.AddListener(Resume);
            buttons[0].gameObject.SetActive(false);
            buttons[1].onClick.AddListener(StartGame);
            buttons[2].onClick.AddListener(ChangeInputScheme);
            buttons[3].onClick.AddListener(Application.Quit);
        }
    }
}