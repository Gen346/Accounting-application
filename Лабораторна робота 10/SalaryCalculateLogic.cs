using System;
using System.Text.Json.Serialization;

namespace Лабораторна_робота_10
{
    [Serializable]
    internal class SalaryCalculateLogic
    {
        [JsonPropertyName("NameID")]
        public string _nameID { get; set; }
        [JsonPropertyName("FirstName")]
        public string _firstName { get; set; }
        [JsonPropertyName("SecondName")]
        public string _secondName { get; set; }
        [JsonPropertyName("FatherName")]
        public string _fatherName { get; set; }
        [JsonPropertyName("BaseSalary")]
        public string _baseSalary { get; set; }
        [JsonPropertyName("HireYear")]
        public string _hireYear { get; set; }
        [JsonPropertyName("BonusPercantage")]
        public string _bonusPercentage { get; set; }
        [JsonPropertyName("WorkedDays")]
        public string _workedDays { get; set; }
        [JsonPropertyName("AccuredAmount")]
        public string _accuredAmount { get; set; }
        [JsonPropertyName("WithheldSalary")]
        public string _withheldSalary { get; set; }
        [JsonPropertyName("NetAmount")]
        public string netAmount { get; set; }

        [JsonPropertyName("Expirience")]
        public string expirience { get; set; }

        [JsonPropertyName("FinalSalary")]
        public string finalSalary { get; set; }
        public SalaryCalculateLogic() { }
        protected SalaryCalculateLogic(string firstName, string secondName, string fatherName, string baseSalary, string hireYear, string bonusPercentage, string workedDays, string accuredAmount, string withheldSalary, string nameID)
        {
            _firstName = firstName;
            _baseSalary = baseSalary;
            _hireYear = hireYear;
            _bonusPercentage = bonusPercentage;
            _workedDays = workedDays;
            _accuredAmount = accuredAmount;
            _withheldSalary = withheldSalary;
            this._nameID = nameID;
        }
    }
}
