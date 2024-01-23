namespace iOSModalLeak;

public partial class SimplePage
{
    public SimplePage()
    {
        InitializeComponent();
    }

    private async void Close(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
        
        Handler?.DisconnectHandler();
    }
    
    ~SimplePage()
    {
        Console.WriteLine("SimplePage finalized");
    }
}