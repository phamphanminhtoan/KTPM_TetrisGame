using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisMVC.TetrisService;

namespace TetrisMVC.Controller
{
    class HighScoreController
    {
        #region ATTRIBUTE



        #endregion

        #region METHOD

        public void SetDataTable(highScore highScore)
        {
            DataTable dt = new DataTable("ThanhVien");
            UserServiceSoapClient reader = new UserServiceSoapClient();
            DataTable bxh = reader.SetDataTable(dt);

            highScore.dataGrid.ItemsSource = bxh.DefaultView;
        }

        public void ShowMainMenu(highScore highScore)
        {
            highScore.Equal += (object sender, EventArgs e) =>
            {
                MenuWindow mainmenu = new MenuWindow(highScore.id, highScore.fullname);
                highScore.Close();
                mainmenu.Show();
            };
        }

        #endregion

    }
}
