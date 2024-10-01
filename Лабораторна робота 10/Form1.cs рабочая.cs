using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;


namespace Лабораторна_робота_10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Test();
        }
        string firstName;
        string secondName;
        string fatherName;
        decimal baseSalary;
        int hireYear;
        decimal bonusPercentage;
        int workedDays;
        decimal accuredAmount;
        decimal withheldSalary;
        decimal netAmount;
        string nameID;

        List<SalaryCalculateLogic> temporaryMemory = new List<SalaryCalculateLogic>();

        #region Calculus
        private void NameIdentifierCheck()
        {
            firstName = FirstName_textBox.Text;
            secondName = SecondName_textBox.Text;
            fatherName = FatherName_textBox.Text;
            if (String.IsNullOrEmpty(FirstName_textBox.Text) && String.IsNullOrEmpty(SecondName_textBox.Text) && String.IsNullOrEmpty(FatherName_textBox.Text))
            {
                MessageBox.Show("Input full name!");
                return;
            }
            else
            {
                NameIdentifierGenerate();
            }
        }
        private void NameIdentifierGenerate()
        {
            nameID = $"{firstName[0]}. {fatherName[0]}. {secondName}";
            FullName_textBox.Text = nameID;
        }
        public void CalculateAccuredAmount() //Если в будущем я захочу закинуть это на гитхаб - перенести код проверок в отедльные методы
        {
            decimal baseSalaryParse = 0;
            decimal bonusPercentageParse = 0;
            decimal workedDaysParse = 0;
            int currentYear = DateTime.Now.Year;
            //
            //Check for salary
            //
            if (Salary_textBox.Text != string.Empty)
            {
                if (Decimal.TryParse(Salary_textBox.Text, out baseSalaryParse))
                {
                    if (Convert.ToInt32(Salary_textBox.Text) != 0)
                    {
                        baseSalary = Convert.ToDecimal(Salary_textBox.Text);
                    }
                }
                else
                {
                    MessageBox.Show("Salary is not correct!");
                    Salary_textBox.Text = "";
                }
            }
            //
            //Check for bonus amount
            //
            if (Bonus_textBox.Text != string.Empty)
            {
                if (Decimal.TryParse(Bonus_textBox.Text, out bonusPercentageParse))
                {
                    if (Convert.ToInt32(Bonus_textBox.Text) < 101)
                    {
                        bonusPercentage = Convert.ToDecimal(Bonus_textBox.Text);
                    }
                    else
                    {
                        MessageBox.Show("Bonus is not correct!");
                        Bonus_textBox.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Bonus is not correct!");
                    Bonus_textBox.Text = "";
                }
            }
            //
            //Check for worked days
            //
            if (WorkedDays_textBox.Text != string.Empty)
            {
                if (Decimal.TryParse(WorkedDays_textBox.Text, out workedDaysParse))
                {
                    if (Convert.ToInt32(WorkedDays_textBox.Text) < 32)
                    {
                        workedDays = Convert.ToInt32(WorkedDays_textBox.Text);
                    }
                    else
                    {
                        MessageBox.Show("Worked days is not correct!");
                        WorkedDays_textBox.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Worked days is not correct!");
                    WorkedDays_textBox.Text = "";
                }
            }

            decimal bonusAmount = baseSalary * (bonusPercentage / 100);

            if (Salary_textBox.Text != string.Empty && WorkedDays_textBox.Text != string.Empty)
            {
                accuredAmount = (baseSalary / 30) * workedDays + bonusAmount;
                AccuredAmount_textBox.Text = String.Format("{0:0.00}", accuredAmount);
            }
            else
            {
                AccuredAmount_textBox.Text = "";
            }



        }
        public void CalculateWithheldSalary()
        {
            if (AccuredAmount_textBox.Text != string.Empty)
            {
                withheldSalary = accuredAmount * 18 / 100;
                WithheldSumm_textBox.Text = String.Format("{0:0.00}", withheldSalary);
            }
            else
            {
                WithheldSumm_textBox.Text = "";
            }
        }
        public void CalculateNetAmount()
        {
            if (AccuredAmount_textBox.Text != string.Empty && WithheldSumm_textBox.Text != string.Empty)
            {
                netAmount = Math.Round(accuredAmount - withheldSalary, 2);
                NetAmount_textBox.Text = String.Format("{0:0.00}", netAmount);
            }
            else
            {
                NetAmount_textBox.Text = "";
            }
        }
        public void CalculateExperience()
        {
            int currentYear = DateTime.Now.Year;
            int experience = 0;
            if (HireYear_comboBox.Text != string.Empty)
            {
                if (Int32.TryParse(HireYear_comboBox.Text, out hireYear))
                {
                    if (Convert.ToInt32(hireYear) <= currentYear)
                    {
                        hireYear = Convert.ToInt32(HireYear_comboBox.Text);
                    }
                }
                else
                {
                    MessageBox.Show("Hire year is not correct!");
                    HireYear_comboBox.Text = "";
                }
            }

            if (HireYear_comboBox.Text != string.Empty)
            {
                if (Int32.TryParse(HireYear_comboBox.Text, out hireYear))
                {
                    experience = currentYear - Convert.ToInt32(HireYear_comboBox.Text);
                    Experience_textBox.Text = experience.ToString();
                }
                else
                {
                    Experience_textBox.Text = "";
                }
            }
        }
        public void CalculateFinalSalary()
        {
            if (Salary_textBox.Text != string.Empty && WithheldSumm_textBox.Text != string.Empty)
            {
                FinalSalary_textBox.Text = String.Format("{0:0.00}", baseSalary - withheldSalary);
            }
            else
            {
                FinalSalary_textBox.Text = "";
            }
        }
        private void Calculus_button1_Click(object sender, EventArgs e)
        {
            CalculateExperience();
            CalculateAccuredAmount();
            CalculateWithheldSalary();
            CalculateNetAmount();
            CalculateFinalSalary();
        }
        #endregion

        string filePath = string.Empty;
        private string GetFilePath()
        {
            if (filePath == string.Empty)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.InitialDirectory = "c:\\";
                    saveFileDialog.Filter = "json files (*.json)|*.json";
                    saveFileDialog.FilterIndex = 2;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.Title = "Save";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = saveFileDialog.FileName;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }

            return filePath;
        }
        private string OpenFilePath()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) //c:\\
            {
                openFileDialog.InitialDirectory = "c:\\Users\\kelz\\Desktop";
                openFileDialog.Filter = "json files (*.json)|*.json";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Open";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
                else
                {
                    return string.Empty;
                }
            }
            return filePath;
        }
        private void Serializer()
        {
            var options = new JsonSerializerOptions();

            options.WriteIndented = true;

            string jsonString = JsonSerializer.Serialize(temporaryMemory, options);
            File.WriteAllText(GetFilePath(), jsonString);
        }
        private List<SalaryCalculateLogic> Deserializer()
        {
            var jsonString = File.ReadAllText(filePath);

            List<SalaryCalculateLogic> temporaryMemory = JsonSerializer.Deserialize<List<SalaryCalculateLogic>>(jsonString);
            return temporaryMemory;
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePath != string.Empty)
            {
                Serializer();
            }
            if (filePath == string.Empty)
            {
                GetFilePath();
                if (filePath != string.Empty)
                {
                    Serializer();
                }
                return;
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filePath = string.Empty;
            GetFilePath();
            Serializer();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void SaveNote_button_Click(object sender, EventArgs e)
        {
            CalculateExperience();
            CalculateAccuredAmount();
            CalculateWithheldSalary();
            CalculateNetAmount();
            CalculateFinalSalary();
            NameIdentifierCheck();
            SaveNoteAction();
        }
        private void SaveNoteAction()
        {
            SalaryCalculateLogic salary = new SalaryCalculateLogic();
            {
                if (String.IsNullOrEmpty(FirstName_textBox.Text)) { return; }
                else { salary._firstName = FirstName_textBox.Text; }

                if (String.IsNullOrEmpty(SecondName_textBox.Text)) { return; }
                else { salary._secondName = SecondName_textBox.Text; }

                if (String.IsNullOrEmpty(FatherName_textBox.Text)) { return; }
                else { salary._fatherName = FatherName_textBox.Text; }

                if (String.IsNullOrEmpty(nameID)) { return; }
                else { salary._nameID = nameID; }

                if (String.IsNullOrEmpty(HireYear_comboBox.Text)) { return; }
                else { salary._hireYear = HireYear_comboBox.Text; }

                if (String.IsNullOrEmpty(Experience_textBox.Text)) { return; }
                else { salary.expirience = Experience_textBox.Text; }

                if (String.IsNullOrEmpty(AccuredAmount_textBox.Text)) { return; }
                else { salary._accuredAmount = AccuredAmount_textBox.Text; }

                if (String.IsNullOrEmpty(WithheldSumm_textBox.Text)) { return; }
                else { salary._withheldSalary = WithheldSumm_textBox.Text; }

                if (String.IsNullOrEmpty(NetAmount_textBox.Text)) { return; }
                else { salary.netAmount = NetAmount_textBox.Text; }

                if (String.IsNullOrEmpty(Salary_textBox.Text)) { return; }
                else { salary._baseSalary = Salary_textBox.Text; }

                if (String.IsNullOrEmpty(Bonus_textBox.Text)) { return; }
                else { salary._bonusPercentage = Bonus_textBox.Text; }

                if (String.IsNullOrEmpty(WorkedDays_textBox.Text)) { return; }
                else { salary._workedDays = WorkedDays_textBox.Text; }

                if (String.IsNullOrEmpty(FinalSalary_textBox.Text)) { return; }
                else { salary.finalSalary = FinalSalary_textBox.Text; }
            }
            temporaryMemory.Add(salary);
            Next_button.Visible = true;
        }
        private void loadFromBeginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Previous_button.Visible = false;
            OpenFilePath();
            if (filePath != string.Empty)
            {
                if (Deserializer().Count != 0)
                {
                    index = 0;
                    LoaderWithFlag(index);
                    temporaryMemory = Deserializer();
                    if (Deserializer().Count > 1)
                    {
                        Next_button.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("File is empty!");
                }
            }
        }
        private void loadFromNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFromFlagMethod();
        }
        private void LoadFromFlagMethod()
        {
            OpenFilePath();
            if (filePath != string.Empty)
            {
                Form2 form2 = new Form2();
                form2.ShowDialog();
                index = form2.GetNumberOfNote();

                if (Deserializer().Count != 0)
                {
                    if (index == 0)
                    {
                        LoaderWithFlag(index);
                        temporaryMemory = Deserializer();
                        Next_button.Visible = true;
                        PrevButtonCheck();
                    }
                    if (index != 0 && index <= Deserializer().Count)
                    {
                        LoaderWithFlag(index - 1);
                        temporaryMemory = Deserializer();
                        PrevButtonCheck();
                    }

                    if (index > Deserializer().Count)
                    {
                        index = Deserializer().Count - 1;
                        Previous_button.Visible = true;
                        LoaderWithFlag(index);
                        temporaryMemory = Deserializer();
                        PrevButtonCheck();
                    }
                }
                else
                {
                    MessageBox.Show("File is empty");
                }
            }
        }
        private void LoaderWithFlag(int index)
        {
            FullName_textBox.Text = Deserializer()[index]._nameID.ToString();
            FirstName_textBox.Text = Deserializer()[index]._firstName.ToString();
            SecondName_textBox.Text = Deserializer()[index]._secondName.ToString();
            FatherName_textBox.Text = Deserializer()[index]._fatherName.ToString();
            HireYear_comboBox.Text = Deserializer()[index]._hireYear.ToString();
            Salary_textBox.Text = Deserializer()[index]._baseSalary.ToString();
            Bonus_textBox.Text = Deserializer()[index]._bonusPercentage.ToString();
            WorkedDays_textBox.Text = Deserializer()[index]._workedDays.ToString();
            Experience_textBox.Text = Deserializer()[index].expirience.ToString();
            AccuredAmount_textBox.Text = Deserializer()[index]._accuredAmount.ToString();
            WithheldSumm_textBox.Text = Deserializer()[index]._withheldSalary.ToString();
            NetAmount_textBox.Text = Deserializer()[index].netAmount.ToString();
            FinalSalary_textBox.Text = Deserializer()[index].finalSalary.ToString();
        }
        private void Next_button_Click(object sender, EventArgs e)
        {
            NextButtonAction();
        }
        private void NextButtonAction()
        {
            index++;
            if (index < temporaryMemory.Count)
            {
                FullName_textBox.Text = temporaryMemory[index]._nameID.ToString();
                FirstName_textBox.Text = temporaryMemory[index]._firstName.ToString();
                SecondName_textBox.Text = temporaryMemory[index]._secondName.ToString();
                FatherName_textBox.Text = temporaryMemory[index]._fatherName.ToString();
                HireYear_comboBox.Text = temporaryMemory[index]._hireYear.ToString();
                Salary_textBox.Text = temporaryMemory[index]._baseSalary.ToString();
                Bonus_textBox.Text = temporaryMemory[index]._bonusPercentage.ToString();
                WorkedDays_textBox.Text = temporaryMemory[index]._workedDays.ToString();
                Experience_textBox.Text = temporaryMemory[index].expirience.ToString();
                AccuredAmount_textBox.Text = temporaryMemory[index]._accuredAmount.ToString();
                WithheldSumm_textBox.Text = temporaryMemory[index]._withheldSalary.ToString();
                NetAmount_textBox.Text = temporaryMemory[index].netAmount.ToString();
                FinalSalary_textBox.Text = temporaryMemory[index].finalSalary.ToString();
                Previous_button.Visible = true;
            }

            if (index == temporaryMemory.Count)
            {
                FullName_textBox.Text = "";
                FirstName_textBox.Text = "";
                SecondName_textBox.Text = "";
                FatherName_textBox.Text = "";
                HireYear_comboBox.Text = "";
                Salary_textBox.Text = "";
                Bonus_textBox.Text = "";
                WorkedDays_textBox.Text = "";
                Experience_textBox.Text = "";
                AccuredAmount_textBox.Text = "";
                WithheldSumm_textBox.Text = "";
                NetAmount_textBox.Text = "";
                FinalSalary_textBox.Text = "";
                Next_button.Visible = false;
                Previous_button.Visible = true;
            }

            PrevButtonCheck();
        }

        private void Previous_button_Click(object sender, EventArgs e)
        {
            PreviousButtonAction();
        }

        int index = 0;

        private void PreviousButtonAction()
        {
            index--;

            FullName_textBox.Text = temporaryMemory[index]._nameID.ToString();
            FirstName_textBox.Text = temporaryMemory[index]._firstName.ToString();
            SecondName_textBox.Text = temporaryMemory[index]._secondName.ToString();
            FatherName_textBox.Text = temporaryMemory[index]._fatherName.ToString();
            HireYear_comboBox.Text = temporaryMemory[index]._hireYear.ToString();
            Salary_textBox.Text = temporaryMemory[index]._baseSalary.ToString();
            Bonus_textBox.Text = temporaryMemory[index]._bonusPercentage.ToString();
            WorkedDays_textBox.Text = temporaryMemory[index]._workedDays.ToString();
            Experience_textBox.Text = temporaryMemory[index].expirience.ToString();
            AccuredAmount_textBox.Text = temporaryMemory[index]._accuredAmount.ToString();
            WithheldSumm_textBox.Text = temporaryMemory[index]._withheldSalary.ToString();
            NetAmount_textBox.Text = temporaryMemory[index].netAmount.ToString();
            FinalSalary_textBox.Text = temporaryMemory[index].finalSalary.ToString();
            Previous_button.Visible = true;
            Next_button.Visible = true;

            PrevButtonCheck();
        }
        private void PrevButtonCheck()
        {
            if (index == 0)
            {
                Previous_button.Visible = false;
                Next_button.Visible = true;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Next_button.Visible = false;
            Previous_button.Visible = false;
        }
        #region Tool strip menu
        private void ChangeFont(Font newFont)
        {
            FirstName_label.Font = newFont;
            SecondName_label.Font = newFont;
            FatherName_label.Font = newFont;
            HireYear_label.Font = newFont;
            Experience_label.Font = newFont;
            AccuredAmount_label.Font = newFont;
            WithheldSumm_label.Font = newFont;
            NetAmount_label.Font = newFont;
            Salary_label.Font = newFont;
            Bonus_label.Font = newFont;
            WorkedDays_label.Font = newFont;
            FinalSalary_label.Font = newFont;
        }

        private void ChangeForeColor(Color newColor)
        {
            FirstName_label.ForeColor = newColor;
            SecondName_label.ForeColor = newColor;
            FatherName_label.ForeColor = newColor;
            HireYear_label.ForeColor = newColor;
            Experience_label.ForeColor = newColor;
            AccuredAmount_label.ForeColor = newColor;
            WithheldSumm_label.ForeColor = newColor;
            NetAmount_label.ForeColor = newColor;
            Salary_label.ForeColor = newColor;
            Bonus_label.ForeColor = newColor;
            WorkedDays_label.ForeColor = newColor;
            FinalSalary_label.ForeColor = newColor;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ChangeFont(new Font("Microsoft Sans Serif", 12));
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ChangeFont(new Font("Impact", 12));
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ChangeFont(new Font("Comic Sans MS", 12));
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeForeColor(Color.Red);
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeForeColor(Color.Green);
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeForeColor(Color.Blue);
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeForeColor(Color.Black);
        }

        #endregion
    }
}
