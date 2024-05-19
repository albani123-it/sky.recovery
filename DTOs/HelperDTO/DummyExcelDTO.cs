﻿namespace sky.recovery.DTOs.HelperDTO
{
    public class PositionCell
    {
        public string Cell { get; set; }
    }
    public class DummyExcelDTO
    {
        public int Id { get; set; }
        public string CellId { get; set; }
        public string NIP { get; set; }
        public string CellNIP { get; set; }

        public string Email { get; set; }
        public string CellEmail { get; set; }

        public string EmployeeName { get; set; }
        public string CellEmployeeName { get; set; }

        public string BranchCity { get; set; }
        public string CellBranchCity { get; set; }

    }
}
