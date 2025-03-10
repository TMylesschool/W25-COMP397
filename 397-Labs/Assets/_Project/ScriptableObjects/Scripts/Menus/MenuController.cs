using UnityEngine;
using UnityEngine.UI;

namespace Platformer397
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Button playBtn;
        [SerializeField] private Button loadBtn;
        [SerializeField] private Button optionBtn;
        [SerializeField] private Button quitBtn;

        private void Start()
        {
            playBtn.onClick.AddListener(() => SceneController.Instance.ChangeScene("Gameplay"));
        }
    }
}
