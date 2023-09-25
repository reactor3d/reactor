using System;
using System.Runtime.InteropServices;
using System.Text;
using Reactor.Geometry;
using Reactor.Math;
using Reactor.Types;
#if USE_NUMERICS
using System.Numerics;
#endif

#if MEMORY_LOGGER
using System.Collections.Generic;
#endif

namespace Reactor.Platform.OpenGL
{
    #region Simple Memory Logging for Debugging
#if MEMORY_LOGGER
    public static class MemoryLogger
    {
        /// <summary>
        /// Configures the memory logger to log a stack trace with each allocated texture or VBO.
        /// </summary>
        public static bool LogStackTrace { get; set; }

        /// <summary>
        /// Gets the size in bytes of all of the textures that have been allocated.
        /// </summary>
        public static long TotalTextureMemory = 0;

        /// <summary>
        /// Gets the size in bytes of all of the VBOs that have been allocated.
        /// </summary>
        public static long TotalVBOMemory = 0;

        private static readonly Dictionary<uint, string> validTextures = new Dictionary<uint, string>();
        private static Dictionary<uint, int> validVBOs = new Dictionary<uint, int>();
        private static Dictionary<uint, string> validVBOStackTrace = new Dictionary<uint, string>();

        internal static void AllocateTexture(uint textureID, System.Drawing.Size size)
        {
            TotalTextureMemory += size.Height * size.Width * 4;
            if (LogStackTrace) validTextures.Add(textureID, Environment.StackTrace);
            else validTextures.Add(textureID, String.Empty);
        }

        internal static void DestroyTexture(uint textureID, System.Drawing.Size size)
        {
            if (validTextures.ContainsKey(textureID))
            {
                TotalTextureMemory -= size.Height * size.Width * 4;
                validTextures.Remove(textureID);
            }
        }

        internal static void AllocateVBO(uint id, int bytes)
        {
            if (validVBOs.ContainsKey(id))
            {
                TotalVBOMemory += bytes - validVBOs[id];
                validVBOs[id] = bytes;
                if (LogStackTrace) validVBOStackTrace[id] = Environment.StackTrace;
            }
            else
            {
                TotalVBOMemory += bytes;
                validVBOs.Add(id, bytes);
                if (LogStackTrace) validVBOStackTrace.Add(id, Environment.StackTrace);
            }
        }

        internal static void DestroyVBO(uint id)
        {
            if (!validVBOs.ContainsKey(id)) return;

            TotalVBOMemory -= validVBOs[id];

            validVBOs.Remove(id);
            if (validVBOStackTrace.ContainsKey(id)) validVBOStackTrace.Remove(id);
        }

        public static Dictionary<uint, string> TextureStackTraces
        {
            get { return validTextures; }
        }

        public static Dictionary<uint, string> VBOStackTraces
        {
            get { return validVBOStackTrace; }
        }
    }
#endif
    #endregion

    /// <summary>
    /// Bindings to OpenGL 4.5 methods as well as some helper shortcuts.
    /// </summary>
    partial class GL
    {
        #region Preallocated Memory
        // pre-allocate the float[] for matrix and array data
        private static float[] float1 = new float[1];
        private static double[] double1 = new double[1];
        private static int[] int1 = new int[1];
        private static bool[] bool1 = new bool[1];
        private static uint currentProgram;
        #endregion
        private static int version, versionMinor;

        /// <summary>
        /// Returns the boolean value of a selected parameter.
        /// </summary>
        /// <param name="pname">A parameter that returns a single boolean.</param>
        public static bool GetBoolean(GetPName pname)
        {
            GetBooleanv(pname, bool1);
            return bool1[0];
        }

        /// <summary>
        /// Returns the float value of a selected parameter.
        /// </summary>
        /// <param name="pname">A parameter that returns a single float.</param>
        public static float GetFloat(GetPName pname)
        {
            GetFloatv(pname, float1);
            return float1[0];
        }

        /// <summary>
        /// Returns the double value of a selected parameter.
        /// </summary>
        /// <param name="pname">A parameter that returns a single double.</param>
        public static double GetDouble(GetPName pname)
        {
            GetDoublev(pname, double1);
            return double1[0];
        }

        /// <summary>
        /// Returns the integer value of a selected parameter.
        /// </summary>
        /// <param name="name">A parameter that returns a single integer.</param>
        public static int GetInteger(GetPName name)
        {
            GetIntegerv(name, int1);
            return int1[0];
        }

        /// <summary>
        /// Gets the current major OpenGL version (returns a cached result on subsequent calls).
        /// </summary>
        /// <returns>The current major OpenGL version, or 0 on an error.</returns>
        public static int Version()
        {
            if (version != 0) return version; // cache the version information

            try
            {
                string versionString = GetString(StringName.Version);

                version = int.Parse(versionString.Substring(0, versionString.IndexOf('.')));
                return version;
            }
            catch (Exception)
            {
                //Console.WriteLine("Error while retrieving the OpenGL version.");
                return 0;
            }
        }

        /// <summary>
        /// Gets the current minor OpenGL version (returns a cached result on subsequent calls).
        /// </summary>
        /// <returns>The current minor OpenGL version, or -1 on an error.</returns>
        public static int VersionMinor()
        {
            if (versionMinor != 0) return versionMinor; // cache the version information

            try
            {
                string versionString = GetString(StringName.Version);

                versionMinor = int.Parse(versionString.Split('.')[1]);
                return versionMinor;
            }
            catch (Exception)
            {
                //Console.WriteLine("Error while retrieving the OpenGL version.");
                return -1;
            }
        }

    }
}
