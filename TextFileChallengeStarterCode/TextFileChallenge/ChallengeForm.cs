using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextFileChallenge
{
    public partial class ChallengeForm : Form
    {
        BindingList<UserModel> users = new BindingList<UserModel>();
        public static string DataSet = @".\StandardDataSet.csv";

        public ChallengeForm()
        {
            InitializeComponent();

            OpenFile();

            WireUpDropDown();
        }

        private void OpenFile()
        {
            //FirstName,LastName,Age,IsAlive
            var file = new FileStream(DataSet, FileMode.Open);
            var reader = new StreamReader(file);
            List<string[]> Data = new List<string[]>();
            string[] ListOrder = reader.ReadLine().Split(',');

            while (!reader.EndOfStream)
            {
                Data.Add(reader.ReadLine().Split(','));
            }
            reader.Close();
            foreach (string[] data in Data)
            {
                /*
                 * bool check = true;
                    Console.WriteLine(check ? "Checked" : "Not checked");  // output: Checked

                    Console.WriteLine(false ? "Checked" : "Not checked");  // output: Not checked
                */
                UserModel m = new UserModel() { FirstName = data[0], LastName = data[1], Age = int.Parse(data[2]), IsAlive = (int.Parse(data[3]) == 1)};
                users.Add(m);
            }


        }

        private void WireUpDropDown()
        {
            usersListBox.DataSource = users;
            usersListBox.DisplayMember = nameof(UserModel.DisplayText);
        }
    }
}
