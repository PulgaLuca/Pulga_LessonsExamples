using System.Xml.Linq;

namespace BlaisePascal.LessonsExamples.Domain
{
    public class Enemy
    {
        private string _name;
        private string _description;
        private int _health;
        private bool _isAlive;

        // Overloading costruttore.
        public Enemy() { }

        public Enemy(string name)
        {
            SetName(name);
        }

        public Enemy(string name, int health)
        {
            SetName(name);
            SetHealth(health);
        }

        /// <summary>
        /// Set the enemy's name.
        /// </summary>
        /// <param name="newName">New assigned name</param>
        public void SetName(string newName)
        {
            if(!string.IsNullOrWhiteSpace(newName))
                _name = newName;
        }

        public void SetHealth(int newHealth)
        {
            if (int.IsPositive(newHealth) && newHealth <= 100)
                _health = newHealth;
        }

        public string GetName() => _name;
        public int GetHealth() => _health;

    }
}
