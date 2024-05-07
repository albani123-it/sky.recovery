namespace sky.recovery.Helper.Enum
{
    public enum HelperEnumConfig
    {
       
    }
    public enum RestrukturRole
    {
        Operator,
        Supervisor
    }
    public enum ModuleName
    {
        Recovery
    }
    public enum RecoverySchema
    {
        RecoveryBusinessV2
    }
    public enum RecoveryFunctionName
    {
        getrestrukture,
        tasklistrestrukture,
        getloanmaster,
        getdetailfordraftingrestruktur,
        getlistfasilitas,
        searchingrestruktur,
        createdraftrestrukture,
        getpermasalahanrestrukture
    }
    public enum RecoverySubModule
    {
        Restruktur,
        Ayda,
        Insurance,
        WriteOff
    }
    public enum StatusWorkflow
    {
        DRAFT,
        REQUESTED,
        APPROVED,
        REJECT,
        REVISED
    }
}
