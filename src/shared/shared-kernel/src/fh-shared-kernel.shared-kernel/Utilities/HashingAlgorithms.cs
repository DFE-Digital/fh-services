using System.IO.Hashing;
using System.Text;

namespace FamilyHubs.SharedKernel.Utilities;

public static class HashingAlgorithms
{
    public static long ComputeXxHashToLong64(string value)
    {
        var data = Encoding.UTF8.GetBytes(value);
        return BitConverter.ToInt64(XxHash64.Hash(data));
    }
}