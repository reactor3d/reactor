using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Types.States
{
    public enum RBlend
    {
        One,				
        Zero,	 			
        SourceColor,		
        InverseSourceColor,	
        SourceAlpha,		
        InverseSourceAlpha,	
        DestinationColor,	
        InverseDestinationColor,	
        DestinationAlpha,	
        InverseDestinationAlpha,	
        BlendFactor,		
        InverseBlendFactor,	
        SourceAlphaSaturation,	
    }
}
