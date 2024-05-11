using System.Persistence;
using UnityEngine;

namespace System {
    public class Postbox : MonoBehaviour, IInteractable, IBind<PostboxData> {
        [SerializeField] private Sprite emptyPostbox;
        [SerializeField] private Sprite fullPostbox;
        [SerializeField] private PostboxData data;

        public void Bind(PostboxData data) {
            this.data = data;
            this.data.hasPost = data.hasPost;
        }

        private void Update() {
            if (data.hasPost == true) {
                var renderer = gameObject.GetComponent<SpriteRenderer>();
                renderer.sprite = fullPostbox;
            } else if (data.hasPost == false) {
                var renderer = gameObject.GetComponent<SpriteRenderer>();
                renderer.sprite = emptyPostbox;
            }
        }
        public void Interact() {
            data.hasPost = false;
            print("YO YOU GOT MAIL!");
        }
    }

    [Serializable]
    public class PostboxData : ISaveable {
        public string Id { get; set; }
        public bool hasPost = true;
    }
}

