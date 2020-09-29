using System;

namespace WebApplication8
{
    public class History
    {
        public History()
        {
            this.Date = DateTime.Now.ToString("MM/dd/yyyy H:mm:ss");
        }
        public string? Id { get; set; }
        public string User { get; set; }
        public string? Date { get; set; }
        public string Screen { get; set; }


    }
}
