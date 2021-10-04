using UnityEngine;

public class InputController : MonoBehaviour
{
    private interface IAction
    {
        public void Launch();
    }

    private class Run : IAction
    {
        private Player m_target;
        public Run(Player target)
        {
            m_target = target;
        }

        public void Launch()
        {
            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            m_target.Run(direction);
        }
    }

    private class Hit : IAction
    {
        private Player m_target;

        public Hit(Player target)
        {
            m_target = target;
        }

        public void Launch()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                m_target.SwordAttack();
            }
        }
    }

    private class Look : IAction
    {
        private Player m_target;
        public Look(Player target)
        {
            m_target = target;
        }

        public void Launch()
        {
            m_target.RotateToDirection(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private Player player;
    private IAction[] actions;

    private void Start()
    {
        player = LevelManager.Instance.Player;
        actions = new IAction[] { new Hit(player), new Run(player), new Look(player) };
    }

    private void Update()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Launch();
        }
    }
}
