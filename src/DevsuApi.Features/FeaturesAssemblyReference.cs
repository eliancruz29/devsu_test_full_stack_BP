using System;
using System.Reflection;

namespace DevsuApi.Features;

public class FeaturesAssemblyReference
{
    internal static readonly Assembly Assembly = typeof(FeaturesAssemblyReference).Assembly;
}
