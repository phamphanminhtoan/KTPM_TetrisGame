using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class User
    {

        #region ATTRIBUTE

        private int id;
        private string username;
        private string password;
        private int score;
        private string fullname;

        #endregion

        #region METHOD

        public User() { }

        public User(User user)
        {
            id = user.id;
            username = user.username;
            password = user.password;
            score = user.score;
            fullname = user.fullname;
        }

        public int getId()
        {
            return id;
        }
        public string getUsername()
        {
            return username;
        }
        public string getPassword()
        {
            return password;
        }
        public int getScore()
        {
            return score;
        }
        public string getFullname()
        {
            return fullname;
        }
        public void setId(int id)
        {
            this.id = id;
        }
        public void setUsername(string username)
        {
            this.username = username;
        }
        public void setPassword(string password)
        {
            this.password = password;
        }
        public void setScore(int score)
        {
            this.score = score;
        }
        public void setFullname(string fullname)
        {
            this.fullname = fullname;
        }

        #endregion

    }
}
