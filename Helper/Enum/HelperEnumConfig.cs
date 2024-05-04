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
        tasklistrestrukture
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
        WAITINGFORAPPROVAL,
        APPROVED,
        REJECT,
        REVISED
    }
}
