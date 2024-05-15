namespace sky.recovery.Helper.Enum
{
    public enum HelperEnumConfig
    {
       
    }
    public enum ConfigSPVNumber
    {
        SPVC=32,
        SPVG=38
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
    public enum CoreSchema
    {
        param
    }
    public enum CoreFunctionName
    {
        getallbranchactived
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
        getpermasalahanrestrukture,
        getmastercollateral,
        removepermasalahanrestrukture,
        createpermasalahanrestrukture,
        updatepermasalahanrestrukture,
        getmasterdocrules,
        getdocrestruktrue,
        checkingavailabledoc,
        insertdocrestrukture,
        removedraftrestrukture,
        checkingexistingrestrukture,
        checkinganalisaexistingrestrukture,
        updateanalisarestrukture,
        configanalisarestruktur,
        updateddocrestrukture
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
