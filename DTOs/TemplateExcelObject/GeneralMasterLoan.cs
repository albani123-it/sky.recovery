namespace sky.recovery.DTOs.TemplateExcelObject
{
    public class GeneralMasterLoan

    { 
        public int Id { get; set; }
        public string cell_id { get; set; }
        public string NamaNasabah { get; set; }
        public string cell_NamaNasabah { get; set; }

        public string KotaTinggal { get; set; }
        public string cell_KotaTinggal { get; set; }

        public string JumlahHutang { get; set; }
        public string cell_JumlahHutang { get; set; }

        public string Status { get; set; }
        public string cell_Status { get; set; }

        public int DPD { get; set; }
        public string cell_DPD { get; set; }

    }
}
