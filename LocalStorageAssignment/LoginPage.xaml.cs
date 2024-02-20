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
        string usernameTest = userNAME.Text;
        string passwordTest = passWORD.Text;
        string emailTest = eMAIL.Text;

        LoginInformation loginInfo = new LoginInformation(usernameTest, passwordTest, emailTest);

        string jsonString = JsonSerializer.Serialize(loginInfo);

        // Breaks here
        File.WriteAllText(filePath, jsonString);
        Console.WriteLine("Login information saved to local storage.");
    }


    // Load Button
    private void Button_Clicked_1(object sender, EventArgs e)
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            //StreamReader jsonData = new StreamReader(filePath);

            LoginInformation loginInfo = JsonSerializer.Deserialize<LoginInformation>(jsonData);

            username2.Text = loginInfo.Username;
            password2.Text = loginInfo.Password;
            email2.Text = loginInfo.Email;
        }
        else
        {
            Error_message.Text = "Login information file not found.";
        }
    }
}