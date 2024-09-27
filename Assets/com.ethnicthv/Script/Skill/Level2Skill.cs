using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace com.ethnicthv.Script.Skill
{
    [RequireComponent(typeof(AntibodyController))]
    public class Level2Skill : MonoBehaviour
    {
        public Image skillCooldown;
        public float cooldown = 8f;
        public float effectDuration = 2f;
        public float activeSpeed = 500f;

        private float _cooldownTimer;
        private AntibodyController _antibodyController;

        private float _originalSpeed;

        private void Awake()
        {
            _antibodyController = GetComponent<AntibodyController>();
            _originalSpeed = _antibodyController.speed;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space) && _cooldownTimer <= 0)
            {
                UseSkill();
            }
        }

        private IEnumerator StartCooldown()
        {
            _cooldownTimer = cooldown;
            while (_cooldownTimer > 0)
            {
                _cooldownTimer -= Time.deltaTime;
                skillCooldown.fillAmount = _cooldownTimer / cooldown;
                yield return null;
            }
        }

        public void UseSkill()
        {
            StartCoroutine(StartCooldown());
            ActiveSkillEffect();
        }

        private void ActiveSkillEffect()
        {
            // do something
            _antibodyController.speed = activeSpeed;
            StartCoroutine(DeactiveSkillEffect());
        }

        private IEnumerator DeactiveSkillEffect()
        {
            yield return new WaitForSeconds(effectDuration);
            _antibodyController.speed = _originalSpeed;
        }
    }
}