using System.Persistence;
using UnityEngine;

namespace System {
    public class Bed : MonoBehaviour, IInteractable, IBind<BedData> {
        private Animator animator;
        public string Id { get; set; }
        [SerializeField] BedData data;

        public void Bind(BedData data) {
            this.data = data;
            this.data.Id = Id;
            this.data.inUse = data.inUse;
        }

        private void Start() {
            animator = gameObject.GetComponent<Animator>();
            if (data.inUse == true) {
                animator.Play("GoToSleep", 0);
            }
        }

        public void Interact() {//TODO!
            var playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            if (data.inUse == false) {
                data.inUse = true;
                playerController.SwitchPlayerState(playerController.sleepingState);
                animator.Play("GoToSleep", 0);
            } else {
                data.inUse = false;
                animator.Play("WakeUp", 0);
            }
        }

        public void WakeUp() {
            var playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            playerController.SwitchPlayerState(playerController.awokenState);
        }
    }
    [Serializable] public class BedData : ISaveable {
        public string Id { get; set; }
        public bool inUse;
    }
}

