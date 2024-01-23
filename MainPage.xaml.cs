namespace iOSModalLeak;

public partial class MainPage
{
    private WeakReference _pageRef;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OpenModal(object? sender, EventArgs e)
    {
        var page = new SimplePage();
        _pageRef = new WeakReference(page);
        Navigation.PushModalAsync(page);
        
        Label.Text = "Push the 'Force GC' button";
        OpenModalButton.IsEnabled = false;
        ForceGCButton.IsEnabled = true;
    }

    private async void ForceGC(object? sender, EventArgs e)
    {
        for (var i = 0; i < 20; i++)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (!_pageRef.IsAlive)
            {
                Label.Text = $"🗑️➡️🚚 {nameof(SimplePage)} was collected after {i + 1} collections.";
                break;
            }
            
            Label.Text = $"💦 {nameof(SimplePage)} is still alive after {i + 1} collections.";

            await Task.Delay(1000);
        }
        
        OpenModalButton.IsEnabled = true;
        ForceGCButton.IsEnabled = false;
    }
}