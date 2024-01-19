using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroThemeSampleApp.Models;

public class User
{
    public AvatarColor UserAvatarColor { get; set; } = AvatarColor.White;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Name => FirstName + " " + LastName;

    // TextBox
    public string? Company { get; set; }

    // TextBox MutliLine
    public string? Address { get; set; }

    // Slider
    public int Age { get; set; } = 25;

    // DatePicker
    public DateTime DateOfJoining { get; set; } = DateTime.Now.Date;

    public bool IsNewGraduate { get; set; }

    public User(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public User()
    {
    }

    public User(User user)
    {
        UserAvatarColor = user.UserAvatarColor;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Company = user.Company;
        Address = user.Address;
        Age = user.Age;
        DateOfJoining = user.DateOfJoining;
        IsNewGraduate = user.IsNewGraduate;
    }

    public User(string firstName, string lastName,string company, string address) 
    {
        FirstName = firstName;
        LastName = lastName;
        Company = company;
        Address = address;
    }

    public User(AvatarColor avatarColor, string? firstName, string? lastName, string? company, string? address, bool isNewGraduate= false)
    {
        UserAvatarColor = avatarColor;
        FirstName = firstName;
        LastName = lastName;
        Company = company;
        Address = address;
        IsNewGraduate = isNewGraduate;
    }
}