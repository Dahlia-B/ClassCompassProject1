namespace ClassCompassApp;

public partial class TestPage : ContentPage
{
    public TestPage()
    {
        InitializeComponent();
    }

    private void OnTestButtonClicked(object sender, EventArgs e)
    {
        TestLabel.Text = "Button clicked successfully!";
    }
}
