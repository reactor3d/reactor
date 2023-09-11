//go:build windows

package assets

import "embed"

//go:generate .\_compile_shaders.bat
//go:embed shaders/*
var FS embed.FS
