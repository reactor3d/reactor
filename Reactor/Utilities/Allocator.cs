namespace Reactor.Utilities
{
    /// <summary>
    ///     Allocator has some pre-defined type arrays useful for Interop with underlying platform memory.
    ///     Instead of creating new types, we reuse memory and thus increase speed by reducing allocations.
    /// </summary>
    public static class Allocator
    {
        public static bool[] Bool_1 = { false };
        public static short[] Short_1 = { 0 };
        public static uint[] UInt32_1 = { 0 };
        public static uint[] UInt32_2 = { 0, 0 };
        public static int[] Int32_1 = { 0 };
        public static int[] Int32_2 = { 0, 0 };
        public static int[] Int32_3 = { 0, 0, 0 };
        public static int[] Int32_4 = { 0, 0, 0, 0 };
        public static float[] Float_1 = { 0.0f };
        public static float[] Float_2 = { 0.0f, 0.0f };
        public static float[] Float_3 = { 0.0f, 0.0f, 0.0f };
        public static float[] Float_4 = { 0.0f, 0.0f, 0.0f, 0.0f };
        public static double[] Double_1 = { 0.0 };
        public static double[] Double_2 = { 0.0 };
        public static double[] Double_3 = { 0.0 };
        public static double[] Double_4 = { 0.0 };
    }
}