namespace Links.Admin.Services;

public static class LinksEnvironment // this will do for now
{
    private static Form mdi_container;

    public static void Initialise( Form mdi_container )
    {
        LinksEnvironment.mdi_container = mdi_container;
    }

    public static Form GetMdiContainer() => mdi_container!;
}
