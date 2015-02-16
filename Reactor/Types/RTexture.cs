using Reactor.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using Reactor;
using System.IO;
using System.Drawing;


namespace Reactor.Types
{
    public class RTexture : IDisposable
    {
        public uint Id;
        public string Name;
        public string Filename;
        public Reactor.Math.Rectangle Bounds;

        bool bound;
        protected TextureTarget textureTarget;
        protected RPixelFormat pixelFormat = RPixelFormat.Rgba;
        protected PixelType pixelType = PixelType.UnsignedByte;
        internal void LoadFromData(byte[] data, string name, bool isCompressed)
        {
            if(isCompressed)
            {
                try
                {
                    ImageDDS.LoadFromData( data, name, out Id, out textureTarget, out pixelFormat, out pixelType );

                }catch(Exception e){
                    RLog.Error("Error loading texture for: "+name);
                    RLog.Error(e.Message);
                    RLog.Error(e);
                }
            }
            else
            {
                try
                {
                    ImageGDI.LoadFromData( data, out Id, out textureTarget, out pixelFormat, out pixelType );
                }catch(Exception e){
                    RLog.Error("Error loading texture for: "+name);
                    RLog.Error(e.Message);
                    RLog.Error(e);
                }
            }
            if ( Id == 0 || textureTarget == 0)
            {
                RLog.Error("Error generating OpenGL texture for: "+name);

            }

            // load succeeded, Texture can be used.
            Bind();
            GL.TexParameter( textureTarget, TextureParameterName.TextureMagFilter, (int) RTextureMagFilter.Linear );
            int MipMapCount;
            GL.GetTexParameter( textureTarget, GetTextureParameter.TextureMaxLevel, out MipMapCount );
            if ( MipMapCount == 0 ) // if no MipMaps are present, use linear Filter
                GL.TexParameter( textureTarget, TextureParameterName.TextureMinFilter, (int) RTextureMinFilter.Linear );
            else // MipMaps are present, use trilinear Filter
                GL.TexParameter( textureTarget, TextureParameterName.TextureMinFilter, (int) RTextureMinFilter.LinearMipmapLinear );
            int height, width;
            GL.GetTexParameter(textureTarget, GetTextureParameter.TextureHeight, out height);
            GL.GetTexParameter(textureTarget, GetTextureParameter.TextureWidth, out width);

            Bounds = new Reactor.Math.Rectangle(0, 0, width, height);
            RLog.Info("Texture loaded for: "+name);
        }
        internal void LoadFromBitmap(Bitmap bitmap)
        {
            try
            {
                ImageGDI.LoadFromBitmap( ref bitmap, out Id, out textureTarget, out pixelFormat, out pixelType );
            }catch(Exception e){
                RLog.Error("Error loading texture from bitmap...");
                RLog.Error(e);
            }
            if ( Id == 0 || textureTarget == 0)
            {
                RLog.Error("Error generating OpenGL texture from bitmap");

            }
            // load succeeded, Texture can be used.
            Bind();
            GL.TexParameter( textureTarget, TextureParameterName.TextureMagFilter, (int) RTextureMagFilter.Linear );
            REngine.CheckGLError();
            int MipMapCount;
            GL.GetTexParameter( textureTarget, GetTextureParameter.TextureMaxLevel, out MipMapCount );
            REngine.CheckGLError();
            if ( MipMapCount == 0 ) // if no MipMaps are present, use linear Filter
                GL.TexParameter( textureTarget, TextureParameterName.TextureMinFilter, (int) RTextureMinFilter.Linear );
            else // MipMaps are present, use trilinear Filter
                GL.TexParameter( textureTarget, TextureParameterName.TextureMinFilter, (int) RTextureMinFilter.LinearMipmapLinear );

            REngine.CheckGLError();
            Bounds = new Reactor.Math.Rectangle(0, 0, bitmap.Width, bitmap.Height);
        }
        internal void LoadFromDisk(string filename)
        {
            if(Path.GetExtension(filename).ToLower() == "dds"){
                try
                {
                    ImageDDS.LoadFromDisk( RFileSystem.Instance.GetFilePath(filename), out Id, out textureTarget, out pixelFormat, out pixelType );

                }catch(Exception e){
                    RLog.Error("Error loading texture from: "+filename);
                    RLog.Error(e);
                }
            }
            else {
                try
                {
                    ImageGDI.LoadFromDisk( RFileSystem.Instance.GetFilePath(filename), out Id, out textureTarget, out pixelFormat, out pixelType );
                }catch(Exception e){
                    RLog.Error("Error loading texture from: "+filename);
                    RLog.Error(e);
                }
            }
            if ( Id == 0 || textureTarget == 0)
            {
                RLog.Error("Error generating OpenGL texture from: "+filename);

            }

                // load succeeded, Texture can be used.
                Bind();
                int max_level = 0;
                int min_level = 0;
                
                REngine.CheckGLError();
                GL.TexParameterI(textureTarget, TextureParameterName.TextureBaseLevel,ref min_level);
                REngine.CheckGLError();
                GL.TexParameterI(textureTarget, TextureParameterName.TextureMaxLevel,ref max_level);
                REngine.CheckGLError();
            GL.TexParameter( textureTarget, TextureParameterName.TextureMagFilter, (int) RTextureMagFilter.Linear );
            REngine.CheckGLError();
            int MipMapCount;
            GL.GetTexParameter( textureTarget, GetTextureParameter.TextureMaxLevel, out MipMapCount );
            REngine.CheckGLError();
            if ( MipMapCount == 0 ) // if no MipMaps are present, use linear Filter
                GL.TexParameter( textureTarget, TextureParameterName.TextureMinFilter, (int) RTextureMinFilter.Linear );
            else // MipMaps are present, use trilinear Filter
                GL.TexParameter( textureTarget, TextureParameterName.TextureMinFilter, (int) RTextureMinFilter.LinearMipmapLinear );
            REngine.CheckGLError();
            int height, width;
            GL.GetTexLevelParameter(textureTarget, 0, GetTextureParameter.TextureHeight, out height);
            REngine.CheckGLError();
            GL.GetTexLevelParameter(textureTarget, 0, GetTextureParameter.TextureWidth, out width);
            REngine.CheckGLError();
            Bounds = new Reactor.Math.Rectangle(0, 0, width, height);
            RLog.Info("Texture loaded from: "+filename);
        }

        internal void Bind(){
            GL.BindTexture( textureTarget, Id );
            REngine.CheckGLError();
            bound = true;
        }

        internal void SetActive()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            REngine.CheckGLError();

        }

        internal void Unbind(){
            GL.BindTexture( textureTarget, 0);
            bound = false;
        }

        public void SetTextureMagFilter(RTextureMagFilter value)
        {
            if(Id != 0)
            {
                GL.TexParameter(textureTarget, TextureParameterName.TextureMagFilter, (int)value);
                REngine.CheckGLError();
            }
        }

        public void SetTextureMinFilter(RTextureMinFilter value)
        {
            if(Id != 0)
            {
                GL.TexParameter(textureTarget, TextureParameterName.TextureMinFilter, (int)value);
                REngine.CheckGLError();
            }
        }

        public void SetTextureWrapMode(RTextureWrapMode modeS, RTextureWrapMode modeT)
        {
            if(Id != 0)
            {
                GL.TexParameter(textureTarget, TextureParameterName.TextureWrapS, (int) modeS);
                REngine.CheckGLError();
                GL.TexParameter(textureTarget, TextureParameterName.TextureWrapT, (int) modeT);
                REngine.CheckGLError();
            }
        }

        public RTextureMagFilter GetTextureMagFilter()
        {
            if(Id != 0)
            {
                int magFilter;
                GL.GetTexParameter(textureTarget, GetTextureParameter.TextureMagFilter, out magFilter);
                return (RTextureMagFilter)magFilter;
            }
            return 0;
        }
        public RTextureMinFilter GetTextureMinFilter()
        {
            if(Id != 0)
            {
                int minFilter;
                GL.GetTexParameter(textureTarget, GetTextureParameter.TextureMinFilter, out minFilter);
                return (RTextureMinFilter)minFilter;
            }
            return 0;
        }

        public RTextureWrapMode GetTextureWrapModeS()
        {
            if(Id!=0)
            {
                int modeS;
                GL.GetTexParameter(textureTarget, GetTextureParameter.TextureWrapS, out modeS);
                return (RTextureWrapMode)modeS;
            }
            return 0;
        }

        public RTextureWrapMode GetTextureWrapModeT()
        {
            if(Id!=0)
            {
                int modeT;
                GL.GetTexParameter(textureTarget, GetTextureParameter.TextureWrapT, out modeT);
                return (RTextureWrapMode)modeT;
            }
            return 0;
        }
        public RPixelFormat GetPixelFormat()
        {
            if(Id != 0)
            {
                return pixelFormat;
            }
            else return 0;
        }
        public void SetPixelFormat(RPixelFormat format)
        {
            if(Id != 0)
            {
                pixelFormat = format;
            }
            
        }
        protected bool isPowerOfTwo (uint x)
        {
            while (((x % 2) == 0) && x > 1) /* While x is even and > 1 */
                x /= 2;
            return (x == 1);
        }

        public T[] GetData<T>()where T : struct
        {
            Bind();
            T[] pixels = new T[Bounds.Width * Bounds.Height];
            GL.GetTexImage<T>(textureTarget, 0, (PixelFormat)pixelFormat, pixelType, pixels);
            REngine.CheckGLError();
            Unbind();
            REngine.CheckGLError();

            return pixels;
        }
        public void SetData<T>(T[] data, RPixelFormat format, int x, int y, int width, int height, bool packAlignment=true) where T : struct
        {
            if(!packAlignment)
                GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            Bind();
            GL.TexSubImage2D<T>(textureTarget, 0, x, y, width, height, (PixelFormat)format, PixelType.UnsignedByte, data);
            REngine.CheckGLError();
            Unbind();
            if(!packAlignment)
                GL.PixelStore(PixelStoreParameter.PackAlignment, 1);
        }
        public void SetData(RColor[] colors, RPixelFormat format, int x, int y, int width, int height)
        {
            Bind();
            GL.TexSubImage2D<RColor>(textureTarget, 0, x, y, width, height, (PixelFormat)format, PixelType.UnsignedByte, colors);
            REngine.CheckGLError();
            Unbind();
            REngine.CheckGLError();
        }
        #region IDisposable implementation

        public void Dispose()
        {
            if(bound){
                Unbind();
            }
            if(Id != 0)
                GL.DeleteTexture(Id);
        }

        #endregion

        protected void CreateProperties(TextureTarget target, bool mipmapped = false)
        {
            textureTarget = target;
            SetTextureMagFilter(RTextureMagFilter.Linear);
            SetTextureMinFilter(mipmapped ? RTextureMinFilter.LinearMipmapLinear : RTextureMinFilter.Linear);
            SetTextureWrapMode(RTextureWrapMode.Repeat, RTextureWrapMode.Repeat);
            REngine.CheckGLError();
        }

        internal static RTexture2D defaultWhite = new RTexture2D(true);
    }

    public enum RTextureMagFilter
    {
        Nearest = TextureMagFilter.Nearest,
        Linear = TextureMagFilter.Linear,
        LinearDetailSgis = TextureMagFilter.LinearDetailSgis,
        LinearDetailAlphaSgis = TextureMagFilter.LinearDetailAlphaSgis,
        LinearDetailColorSgis = TextureMagFilter.LinearDetailColorSgis,
        LinearSharpenSgis = TextureMagFilter.LinearSharpenSgis,
        LinearSharpenAlphaSgis = TextureMagFilter.LinearSharpenAlphaSgis,
        LinearSharpenColorSgis = TextureMagFilter.LinearSharpenColorSgis,
        Filter4Sgis = TextureMagFilter.Filter4Sgis,
        PixelTexGenQCeilingSgix = TextureMagFilter.PixelTexGenQCeilingSgix,
        PixelTexGenQRoundSgix = TextureMagFilter.PixelTexGenQRoundSgix,
        PixelTexGenQFloorSgix = TextureMagFilter.PixelTexGenQFloorSgix
    }
    public enum RTextureMinFilter
    {
        Nearest = TextureMinFilter.Nearest,
        Linear = TextureMinFilter.Linear,
        NearestMipmapNearest = TextureMinFilter.NearestMipmapNearest,
        LinearMipmapNearest = TextureMinFilter.LinearMipmapNearest,
        NearestMipmapLinear = TextureMinFilter.NearestMipmapLinear,
        LinearMipmapLinear = TextureMinFilter.LinearMipmapLinear,
        Filter4Sgis = TextureMinFilter.Filter4Sgis,
        LinearClipmapLinearSgix = TextureMinFilter.LinearClipmapLinearSgix,
        PixelTexGenQCeilingSgix = TextureMinFilter.PixelTexGenQCeilingSgix,
        PixelTexGenQRoundSgix = TextureMinFilter.PixelTexGenQRoundSgix,
        PixelTexGenQFloorSgix = TextureMinFilter.PixelTexGenQFloorSgix,
        NearestClipmapNearestSgix = TextureMinFilter.NearestClipmapNearestSgix,
        NearestClipmapLinearSgix = TextureMinFilter.NearestClipmapLinearSgix,
        LinearClipmapNearestSgix = TextureMinFilter.LinearClipmapNearestSgix
    }

    public enum RTextureWrapMode
    {
        Clamp = TextureWrapMode.Clamp,
        ClampToBorder = TextureWrapMode.ClampToBorder,
        ClampToBorderARB = TextureWrapMode.ClampToBorderArb,
        ClampToBorderNV = TextureWrapMode.ClampToBorderNv,
        Repeat = TextureWrapMode.Repeat,
        Mirrior = TextureWrapMode.MirroredRepeat
    }
}
