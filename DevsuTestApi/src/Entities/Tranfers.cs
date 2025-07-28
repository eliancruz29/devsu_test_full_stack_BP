using DevsuTestApi.Enums;
using DevsuTestApi.Primitives;

namespace DevsuTestApi.Entities;

public class Tranfers : BaseEntity
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public TranferTypes Type { get; set; }

    public int Amount { get; set; }

    public int Balance { get; set; }
}