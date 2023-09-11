package reactor

import (
	"github.com/go-gl/mathgl/mgl64"
)

type RRenderingBackend = int

const (
	RRENDERINGBACKEND_NONE   = 0
	RRENDERINGBACKEND_VULKAN = 1
	RRENDERINGBACKEND_OPENGL = 2
	RRENDERINGBACKEND_GLES   = 3
)

// RID represents an ID of any Reactor object. It's an int64 but holds signifigance so it is it's own type.
type RID = int64

// RBlendMode represents a blending mode for rendering.
// Compatible options are RBLENDMODE_OPAQUE, RBLENDMODE_ADD, RBLENDMODE_SUB, RBLENDMODE_ALPHA, and RBLENDMODE_MASK
type RBlendMode = int

const (
	RBLENDMODE_OPAQUE = 0
	RBLENDMODE_ADD    = 1
	RBLENDMODE_SUB    = 2
	RBLENDMODE_ALPHA  = 3
	RBLENDMODE_MASK   = 4
)

// RWindowMode represents a window mode, either
// RWINDOWMODE_WINDOW, RWINDOWMODE_BORDERLESS, RWINDOWMODE_FULLSCREEN, RWINDOWMODE_NATIVE, or RWINDOWMODE_NONE for dedicated servers.
type RWindowMode = int

const (
	RWINDOWMODE_NONE = iota
	RWINDOWMODE_WINDOW
	RWINDOWMODE_BORDERLESS
	RWINDOWMODE_FULLSCREEN
	RWINDOWMODE_NATIVE
)

// RDestroyable is an interface for any resource that requires cleanup or destruction.
// For native resources, a finalizer is used to clean up, but for game objects and resources
// you, the developer, create will have to be destroyed. You can also use a finalizer to call Destroy. It's up to you.
type RDestroyable interface {
	Destroy() error
}

// RClonable is an interface for any resource that can be cloned.
type RClonable interface {
	// Clone the object and return it, otherwise return an error. The boolean parameter is to make a deep-copy.
	Clone(bool) (interface{}, error)
}

// RPoint represents a x and y coordinate, in that order.
type RPoint struct{ X, Y int }

// Point returns a RPoint of a given x, y coordinate.
func Point(x, y int) RPoint {
	return RPoint{x, y}
}

// RSize represents a width and a height, in that order.
type RSize struct{ Width, Height int }

// Size returns a RSize of a given width and height.
func Size(width, height int) RSize {
	return RSize{width, height}
}

// RRect represents a rectangle as left, top, width, height, in that order.
type RRect struct{ Left, Top, Width, Height int }

// Rect returns a RRect of left, top, width, and height.
func Rect(left, top, width, height int) RRect {
	return RRect{left, top, width, height}
}

type RVector2 = mgl64.Vec2
type RVector3 = mgl64.Vec3
type RVector4 = mgl64.Vec4
type RMatrix2 = mgl64.Mat2
type RMatrix3 = mgl64.Mat3
type RMatrix4 = mgl64.Mat4
type RQuaternion = mgl64.Quat
type RPlane struct {
	D      float64
	Normal RVector3
}
type RRay2 struct {
	Position RVector2
	Normal   RVector2
}
type RRay3 struct {
	Position RVector3
	Normal   RVector3
}
type RBoundingBox struct {
	Min RVector3
	Max RVector3
}
