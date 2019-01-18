namespace SampleApp.Sources.democlient
{
    // If you are using HATEOAS links to navigate Deere's APIs, these are some of the link rel values to look for.
    // See https://developer.deere.com/#!documentation&doc=.%2Fmyjohndeere%2Flinks.htm
    public enum LinkRel
    {
        self,
        organizations,
        addresses,
        staff,
        operators,
        clients,
        farms,
        fields,
        addField,
        boundaries,
        fieldOperation,
        files,
        transferableFiles,
        uploadFile,
        sendFileToMachine,
        machines,
        addMachine,
        wdtCapableMachines,
        organizationMaintenancePlans,
        job,
        tasks,
        chemicals,
        addChemical,
        varieties,
        addVariety,
        assets,
        notifications,
        nextPage
    }
}
