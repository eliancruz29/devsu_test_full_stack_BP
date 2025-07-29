using DevsuApi.Domain.Enums;
using DevsuApi.SharedKernel;

namespace DevsuApi.Domain.Entities;

public abstract class Person : BaseEntity
{
    protected Person(
        Guid id,
        string name,
        Gender gender,
        DateTime dateOfBirth,
        string identification,
        string address,
        string phoneNumber)
        : base(id)
    {
        Name = name;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Identification = identification;
        Address = address;
        PhoneNumber = phoneNumber;
    }

    public string Name { get; private set; } = string.Empty;

    public Gender Gender { get; private set; }

    public DateTime DateOfBirth { get; private set; }

    public string Identification { get; private set; } = string.Empty;

    public string Address { get; private set; } = string.Empty;

    public string PhoneNumber { get; private set; } = string.Empty;

    protected void Update(
        string name,
        Gender gender,
        DateTime dateOfBirth,
        string identification,
        string address,
        string phoneNumber)
    {
        Name = name;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Identification = identification;
        Address = address;
        PhoneNumber = phoneNumber;
    }
}