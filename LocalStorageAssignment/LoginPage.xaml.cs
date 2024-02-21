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

    // Save Button
    private void Button_Clicked(object sender, EventArgs e)
    {
        // some text data first
        savedMessage.Text = "Login Info Saved!";
        string usernameTest = DisplayUsername;
        string passwordTest = passWORD.Text;
        string emailTest = eMAIL.Text;
        DisplayUsername = "";
        DisplayPassword = "";
        DisplayEmail = "";

        LoginInformation loginInfo = new LoginInformation(usernameTest, passwordTest, emailTest);

        string jsonString = JsonSerializer.Serialize(loginInfo);

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