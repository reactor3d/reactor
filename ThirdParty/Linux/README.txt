On linux, you'll need to add 'freetype' and 'assimp' to your .deb or .rpm packages.  The libraries will pick them up here:
/usr/lib/libfreetype.so
/usr/lib/libassimp.so

and on 64-bit
/usr/lib64/libfreetype.so
/usr/lib64/libassimp.so


Let me know if this doesn't work out.
-Gabriel