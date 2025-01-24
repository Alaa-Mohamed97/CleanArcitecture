namespace CleanArcitecture.Core.Helpers
{
    public static class Localized
    {
        public static bool IsEn()
        {
            return Thread.CurrentThread.CurrentUICulture.Name == "en-US" ?
                true : false;
        }
    }
}
