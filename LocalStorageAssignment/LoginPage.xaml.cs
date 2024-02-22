using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.IO;
using System.Runtime.CompilerServices;

namespace LocalStorageAssignment;

public partial class LoginPage : ContentPage
{
    private string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LocalStorageText.txt");

    public LoginPage()
	{
        InitializeComponent();
        BindingContext = this;
        LoadLoginInformation();
        ChooseImageButton.Clicked += OnChooseImageButtonClicked;
    }

    private string loadder;

    public string Loader
    {
        get { return loadder; }
        set 
        { 
            loadder = value;
            OnPropertyChanged();
        }
    }

    private string displayusername;

    public string DisplayUsername
    {
        get { return displayusername; }
        set { 
            displayusername = value; 
            OnPropertyChanged();
        }
    }

    private string displaypassword;

    public string DisplayPassword
    {
        get { return displaypassword; }
        set { 
            displaypassword = value;
            OnPropertyChanged();
        }
    }

    private string displayemail;

    public string DisplayEmail
    {
        get { return displayemail; }
        set { 
            displayemail = value;
            OnPropertyChanged();
        }
    }

    private string displayimage;

    public string DisplayImage
    {
        get { return displayimage; }
        set { 
            displayimage = value; 
            OnPropertyChanged();
        }
    }


    class LoginInformation
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public LoginInformation(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }
    }

    // Image Button
    private async void OnChooseImageButtonClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await Permissions.RequestAsync<Permissions.Media>();

            if (result == PermissionStatus.Granted)
            {
                var file = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Select Image"
                });

                if (file != null)
                {
                    var stream = await file.OpenReadAsync();
                    ChosenImage.Source = ImageSource.FromStream(() => stream);
                    // Tryy to save the picture and display it on load button
                }
            }
            else
            {
                // Permission denied
                await DisplayAlert("Permission Denied", "Permission Required.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }


    // Save Button
    private void Button_Clicked(object sender, EventArgs e)
    {
        savedMessage.Text = "Login Info Saved!";
        string usernameTest = DisplayUsername;
        string passwordTest = DisplayPassword;
        string emailTest = DisplayEmail;
        DisplayUsername = "";
        DisplayPassword = "";
        DisplayEmail = "";

        LoginInformation loginInfo = new LoginInformation(usernameTest, passwordTest, emailTest);

        string jsonString = JsonSerializer.Serialize(loginInfo);

        if (string.IsNullOrEmpty(DisplayUsername))
        {
            DisplayAlert("Information Required", "Fill in all required information", "Ok");
        }

        File.WriteAllText(filePath, jsonString);
        Console.WriteLine("Login information saved to local storage.");
    }


    // Load Method
    private void LoadLoginInformation()
    {
        savedMessage.Text = "";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

            LoginInformation loginInfo = JsonSerializer.Deserialize<LoginInformation>(jsonData);

            DisplayUsername = loginInfo.Username;
            DisplayPassword = loginInfo.Password;
            DisplayEmail = loginInfo.Email;
        }
        else
        {
            Error_message.Text = "Login information file not found.";
        }
    }
    // Load Button
    private void Button_Clicked_1(object sender, EventArgs e)
    {
        LoadLoginInformation();
    }
}