using System;
using System.Collections.Generic;
using System.Text;

namespace JsonParsing_WPF
{
    class JsonExample
    {
        public string Title { get; set; }
        public int CaseNo { get; set; }
        public string Date { get; set; }
        public string Desc { get; set; }
        public string Updated { get; set; }
        public List<DailyTemp> Temp { get; set; }

        public JsonExample(string title, int caseNo, string date, string desc, string updated)
        {
            this.Title = title;
            this.CaseNo = caseNo;
            this.Date = date;
            this.Desc = desc;
            this.Updated = updated;

        }

    }

    public class DailyTemp
    {
        public string Date { get; set; }
        public int High { get; set; }
        public int Low { get; set; }
        public int Average { get; set; }

        public DailyTemp(string date, int high, int low, int average)
        {
            this.Date = date;
            this.High = high;
            this.Low = low;
            this.Average = average;
        }

    }

}
