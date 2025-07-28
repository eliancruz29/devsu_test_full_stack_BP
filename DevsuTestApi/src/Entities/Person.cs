using DevsuTestApi.Enums;
using DevsuTestApi.Primitives;

namespace DevsuTestApi.Entities;

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

    public string Name { get; private init; } = string.Empty;

    public Gender Gender { get; private init; }

    public DateTime DateOfBirth { get; private init; }

    public string Identification { get; private init; } = string.Empty;

    public string Address { get; private init; } = string.Empty;

    public string PhoneNumber { get; private init; } = string.Empty;
}