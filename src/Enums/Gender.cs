using System.Runtime.Serialization;

namespace DevsuTestApi.Enums;

public enum Gender
{
    [EnumMember(Value = "Masculino")]
    Male = 1,
    
    [EnumMember(Value = "Femenino")]
    Female,
    
    [EnumMember(Value = "No especificado")]
    NotSpecified
}