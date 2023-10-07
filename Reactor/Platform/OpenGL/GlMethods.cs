using System;
using System.Text;
using Reactor.Utilities;
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
    ///     Bindings to OpenGL 4.5 methods as well as some helper shortcuts.
    /// </summary>
    partial class GL
    {
        #region Preallocated Memory

        // pre-allocate the float[] for matrix and array data

        private static uint currentProgram;

        #endregion

        private static int version, versionMinor;

        /// <summary>
        ///     Returns the boolean value of a selected parameter.
        /// </summary>
        /// <param name="pname">A parameter that returns a single boolean.</param>
        public static bool GetBoolean(GetPName pname)
        {
            GetBooleanv(pname, Allocator.Bool_1);
            return Allocator.Bool_1[0];
        }

        /// <summary>
        ///     Returns the float value of a selected parameter.
        /// </summary>
        /// <param name="pname">A parameter that returns a single float.</param>
        public static float GetFloat(GetPName pname)
        {
            GetFloatv(pname, Allocator.Float_1);
            return Allocator.Float_1[0];
        }

        /// <summary>
        ///     Returns the double value of a selected parameter.
        /// </summary>
        /// <param name="pname">A parameter that returns a single double.</param>
        public static double GetDouble(GetPName pname)
        {
            GetDoublev(pname, Allocator.Double_1);
            return Allocator.Double_1[0];
        }

        /// <summary>
        ///     Returns the integer value of a selected parameter.
        /// </summary>
        /// <param name="name">A parameter that returns a single integer.</param>
        public static int GetInteger(GetPName name)
        {
            GetIntegerv(name, Allocator.Int32_1);
            return Allocator.Int32_1[0];
        }

        public static string GetProgramInfoLog(uint program)
        {
            GetProgramiv(program, ProgramParameter.InfoLogLength, Allocator.Int32_1);
            if (Allocator.Int32_1[0] == 0) return string.Empty;
            var sb = new StringBuilder(Allocator.Int32_1[0]);
            GetProgramInfoLog(program, sb.Capacity, Allocator.Int32_1, sb);
            return sb.ToString();
        }

        /// <summary>
        ///     Gets the program info from a shader program.
        /// </summary>
        /// <param name="shader">The ID of the shader program.</param>
        public static string GetShaderInfoLog(uint shader)
        {
            GetShaderiv(shader, ShaderParameter.InfoLogLength, Allocator.Int32_1);
            if (Allocator.Int32_1[0] == 0) return string.Empty;
            var sb = new StringBuilder(Allocator.Int32_1[0]);
            GetShaderInfoLog(shader, sb.Capacity, Allocator.Int32_1, sb);
            return sb.ToString();
        }

        public static string GetActiveAttrib(uint shader, int index)
        {
            int[] l = { 0 };
            int[] s = { 0 };
            ActiveAttribType[] type = { ActiveAttribType.Float };
            var sb = new StringBuilder(64);
            GetActiveAttrib(shader, index, 64, l, s, type, sb);
            return sb.ToString();
        }

        /// <summary>
        ///     Gets the current major OpenGL version (returns a cached result on subsequent calls).
        /// </summary>
        /// <returns>The current major OpenGL version, or 0 on an error.</returns>
        public static int Version()
        {
            if (version != 0) return version; // cache the version information

            try
            {
                var versionString = GetString(StringName.Version);

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
        ///     Gets the current minor OpenGL version (returns a cached result on subsequent calls).
        /// </summary>
        /// <returns>The current minor OpenGL version, or -1 on an error.</returns>
        public static int VersionMinor()
        {
            if (versionMinor != 0) return versionMinor; // cache the version information

            try
            {
                var versionString = GetString(StringName.Version);

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