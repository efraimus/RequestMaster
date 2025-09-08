namespace RequestMaster.Patterns
{
    class Snackbar
    {
        MaterialDesignThemes.Wpf.Snackbar snackBar;
        public Snackbar(MaterialDesignThemes.Wpf.Snackbar snackBar)
        {
            this.snackBar = snackBar;
        }
        public void show(string message)
        {
            snackBar.MessageQueue?.Enqueue
                ($"{message}", null, null, null, false, true, TimeSpan.FromSeconds(3));
        }
    }
}
