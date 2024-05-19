using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace sky.recovery.DTOs.HelperDTO
{

    public class FormatDTO
    {
        public FormattingLayoutDTO Header { get; set; }
        public FormatKepadaDTO KepadaDTO { get; set; }
        public PerihalDTO Perihal { get; set; }
        public FormatNoSuratDTO NoSurat { get; set; }
    }

    public class FormatNoSuratDTO
    {
        public string nomor { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
    public class FormatKepadaDTO
    {
        public string Namakepada { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }

    public class PerihalDTO
    {
        public string Perihal { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
    public class FormattingLayoutDTO
    {
        public string AlamatPT { get; set; }
        public string NamaGedung { get; set; }
        public string AlamatJalanSurat { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }

        public string namakota { get; set; }

        public int x { get; set; }
        public int y { get; set; }
       
    }
}
