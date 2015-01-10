using Reactor.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using Reactor;
using System.IO;
namespace Reactor.Types
{
    public class RTexture : IDisposable
    {
        public uint Id;
        public string Name;
        public string Filename;
        public Rectangle Bounds;

        bool bound;
        TextureTarget textureTarget;

        internal void LoadFromData(byte[] data, string name, bool isCompressed)
        {
            if(isCompressed)
            {
                try
                {
                    ImageDDS.LoadFromData( data, name, out Id, out textureTarget );

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
                    ImageGDI.LoadFromData( data, out Id, out textureTarget );
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
            Bounds = new Rectangle(0, 0, width, height);
            RLog.Info("Texture loaded for: "+name);
        }
        internal void LoadFromDisk(string filename)
        {
            if(Path.GetExtension(filename).ToLower() == "dds"){
                try
                {
                    ImageDDS.LoadFromDisk( filename, out Id, out textureTarget );

                }catch(Exception e){
                    RLog.Error("Error loading texture from: "+filename);
                    RLog.Error(e.Message);
                    RLog.Error(e);
                }
            }
            else {
                try
                {
                    ImageGDI.LoadFromDisk( filename, out Id, out textureTarget );
                }catch(Exception e){
                    RLog.Error("Error loading texture from: "+filename);
                    RLog.Error(e.Message);
                    RLog.Error(e);
                }
            }
            if ( Id == 0 || textureTarget == 0)
            {
                RLog.Error("Error generating OpenGL texture from: "+filename);

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
            Bounds = new Rectangle(0, 0, width, height);
            RLog.Info("Texture loaded from: "+filename);
        }

        internal void Bind(){
            GL.BindTexture( textureTarget, Id );
            bound = true;
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
            }
        }

        public void SetTextureMinFilter(RTextureMinFilter value)
        {
            if(Id != 0)
            {
                GL.TexParameter(textureTarget, TextureParameterName.TextureMinFilter, (int)value);
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
}
