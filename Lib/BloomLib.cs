using System.Collections;

namespace bloom;
public class Bloom
{
    public Bloom(ushort hashes, ushort numberOfBits)
    {
        Hashes = hashes;
        NumberOfBits = numberOfBits;

        bitVector = new BitArray(numberOfBits);
    }

    public ushort Hashes { get; }
    public ushort NumberOfBits { get; }
    public ushort NumberOfHashesAdded = 0;

    public BitArray bitVector;

    public void Insert(object element) {
        var indices = FindIndices(element);
        PopulateBitVector(indices);
        NumberOfHashesAdded += 1;
    }

    public bool Search(object element) {
        var indices = FindIndices(element);
        return indices.All(i=>bitVector[i]);
    }
    public double FalsePositiveLikelihood() => (double) Math.Pow(1.0d -Math.Pow(Math.E,(double)Hashes* NumberOfHashesAdded / NumberOfBits), Hashes);


    private IEnumerable<ushort> FindIndices(object element) {
        for (int i = 0; i < Hashes; i++)
        {
            var hash = (i,element).GetHashCode();
            var remainder = Modulo(hash);
            yield return remainder;
        }
    }
    private ushort Modulo (int a) {
        var b = (int)NumberOfBits;
        var mod = a - b*Math.Floor(a / (double)b);
        return (ushort) mod;
    }
    private void PopulateBitVector(IEnumerable<ushort> indices) {
        foreach (var index in indices)
        {
            bitVector[index] = true;
        }
    }
}
