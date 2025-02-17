using System;

namespace Crud_test.Models;

public class Users
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
