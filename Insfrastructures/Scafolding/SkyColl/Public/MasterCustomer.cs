using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class MasterCustomer
    {
        public int Id { get; set; }
        public string CuCif { get; set; }
        public string CuName { get; set; }
        public DateTime? CuBorndate { get; set; }
        public string CuBornplace { get; set; }
        public int? CuIdtype { get; set; }
        public string CuIdnumber { get; set; }
        public int? CuGender { get; set; }
        public int? CuMaritalstatus { get; set; }
        public int? CuNationality { get; set; }
        public int? CuIncometype { get; set; }
        public string CuIncome { get; set; }
        public int? CuCusttype { get; set; }
        public string Pekerjaan { get; set; }
        public string Jabatan { get; set; }
        public int? CuOccupation { get; set; }
        public string CuCompany { get; set; }
        public string CuEmail { get; set; }
        public string CuAddress { get; set; }
        public string CuRt { get; set; }
        public string CuRw { get; set; }
        public int? CuKelurahan { get; set; }
        public string Kelurahan { get; set; }
        public string Kecamatan { get; set; }
        public int? CuKecamatan { get; set; }
        public string City { get; set; }
        public int? CuCity { get; set; }
        public string Provinsi { get; set; }
        public int? CuProvinsi { get; set; }
        public string CuZipcode { get; set; }
        public string CuHmphone { get; set; }
        public string CuMobilephone { get; set; }
        public int BranchId { get; set; }
        public DateTime? StgDate { get; set; }
    }
}
