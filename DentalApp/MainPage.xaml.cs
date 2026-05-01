namespace DentalApp;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object? sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
    
    private async void OnExportClicked(object sender, EventArgs e)
    {
        var source = Path.Combine(FileSystem.AppDataDirectory, "app.db");
        if (!File.Exists(source))
            return;
        //var target = Path.Combine(FileSystem.CacheDirectory, "app_backup.db");
        var target = Path.Combine(
            FileSystem.CacheDirectory,
            $"app_backup_{DateTime.Now:yyyyMMdd_HHmm}.db");

        File.Copy(source, target, true);

        await Share.RequestAsync(new ShareFileRequest
        {
            Title = "Database backup",
            File = new ShareFile(target)
        });
    }
}