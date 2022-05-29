using System;
using System.Globalization;
using System.Text;

namespace struct_worker
{
    [Serializable]
    public struct Worker : IComparable<Worker>
    {
        public string surnameAndInitials;
        public string jobTitle;
        public int year;
        public Worker(string surnameAndInitials, string jobTitle, int year)
        {
            this.surnameAndInitials = surnameAndInitials;
            this.jobTitle = jobTitle;
            this.year = year;
        }
        public int CompareTo(Worker that)
        {
            string thisName = this.surnameAndInitials;
            string thatName = that.surnameAndInitials;
            return string.Compare(thisName,thatName);
        }
        public override string ToString()
        {
            return $"{this.surnameAndInitials} {this.jobTitle} {this.year}";
        }

    }
}

