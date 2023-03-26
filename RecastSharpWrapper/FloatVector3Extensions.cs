public static class FloatVector3Extensions
{
    public static System.Numerics.Vector3 ToNumericsVector3(this float[] p, int off = 0)
    {
        return new System.Numerics.Vector3(p[off + 0], p[off + 1], p[off + 2]);
    }

    public static float[] ToFloat(this System.Numerics.Vector3 p)
    {
        return new[] { p.X, p.Y, p.Z };
    }

    public static float SqrMagnitude(this System.Numerics.Vector3 p)
    {
        return p.X * p.X + p.Y * p.Y + p.Z * p.Z;
    }
}