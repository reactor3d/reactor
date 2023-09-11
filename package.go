package reactor

import (
	"io"
)

type RPackage interface {
	io.ReadSeekCloser
	Verify(string) bool
	Get(path string) []byte
}
