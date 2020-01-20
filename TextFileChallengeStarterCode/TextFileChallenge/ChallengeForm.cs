using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextFileChallenge
{
    public partial class ChallengeForm : Form
    {
        BindingList<UserModel> users = new BindingList<UserModel>();
        public static string DataSet = @".\AdvancedDataSet.csv";
        List<string[]> Data;
        string[] ListOrder;

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
            Data = new List<string[]>();
            ListOrder = reader.ReadLine().Split(',');

            while (!reader.EndOfStream)
            {
                Data.Add(reader.ReadLine().Split(','));
            }
            reader.Close();
            foreach (string[] data in Data)
            {
                if (data[0] != "")
                {
                    users.Add(new UserModel() { FirstName = data[Array.IndexOf(ListOrder, "FirstName")], LastName = data[Array.IndexOf(ListOrder, "LastName")], Age = int.Parse(data[Array.IndexOf(ListOrder, "Age")]), IsAlive = int.Parse(data[Array.IndexOf(ListOrder, "IsAlive")]) == 1 });
                }
                
            }


        }

        private void WireUpDropDown()
        {
            usersListBox.DataSource = users;
            usersListBox.DisplayMember = nameof(UserModel.DisplayText);
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            UserModel m;
            bool finished = false;
            while(!finished)
            {
                try
                {
                    m = new UserModel() { FirstName = firstNameText.Text, LastName = lastNameText.Text, Age = (int)agePicker.Value, IsAlive = isAliveCheckbox.Checked };
                    finished = true;
                    users.Add(m);
                }
                catch
                {
                    MessageBox.Show("Please Enter Correct", "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void saveListButton_Click(object sender, EventArgs e)
        {
            var file = new FileStream(DataSet, FileMode.Create);
            var writer = new StreamWriter(file);

            writer.Write(ListOrder[0] + "," + ListOrder[1] + "," + ListOrder[2] + "," + ListOrder[3]);
            writer.WriteLine();
            
            foreach(UserModel user in users)
            {

                string sb = "";
                for (int i = 0; i < 4; i++)
                {
                    if (ListOrder[i] == "Age")
                    {
                        sb += Convert.ToString(user.Age);
                    }
                    else if (ListOrder[i] == "FirstName")
                    {
                        sb += user.FirstName;
                    }
                    else if (ListOrder[i] == "LastName")
                    {
                        sb += user.LastName;
                    }
                    else if (ListOrder[i] == "IsAlive")
                    {
                        sb += Convert.ToInt32(user.IsAlive);
                    }

                    if (i < 3)
                    {
                        sb += ",";
                    }
                }
                
                writer.WriteLine(sb);
            }
            writer.Close();
        }
    }
}
